using System;
using System.Linq;
using Interfaces;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<float> OnProgressChanged;
    public event EventHandler<State> OnStateChanged;

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burning,
        Burned,
    }
    
    [SerializeField] private FryingRecipeSO[] _fryingRecipeSos;
    [SerializeField] private BurningRecipeSO[] _burningRecipeSos;

    private float _fryingTimer;
    private FryingRecipeSO _curFryingRecipeSo;
    private BurningRecipeSO _curBurningRecipeSo;
    private State _state = State.Idle;

    private void Update()
    {
        if (!(this as IKitchenObjectHolder).HasKitchenObject)
        {
            _state = State.Idle;
            OnStateChanged?.Invoke(this, _state);
            OnProgressChanged?.Invoke(this, 0f);
            _fryingTimer = 0f;
            return;
        }
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Frying:
                _fryingTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this, _fryingTimer / _curFryingRecipeSo.TimeToFry);
                
                if (!(_fryingTimer > _curFryingRecipeSo.TimeToFry)) 
                    return;
            
                KitchenObject.DestroySelf();
                KitchenObject.SpawnKitchenObject(_curFryingRecipeSo.Output, this);
                _state = State.Fried;
                OnStateChanged?.Invoke(this, _state);
                break;
            case State.Fried:
                _fryingTimer = 0f;
                _state = State.Burning;
                OnStateChanged?.Invoke(this, _state);
                _curBurningRecipeSo = GetBurningRecipeSoFromInput(KitchenObject.KitchenObjectSo);
                break;
            case State.Burning:
                _fryingTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this, _fryingTimer / _curBurningRecipeSo.TimeToBurn);
                
                if (!(_fryingTimer > _curBurningRecipeSo.TimeToBurn)) 
                    return;

                _state = State.Burned;
                OnStateChanged?.Invoke(this, _state);
                break;
            case State.Burned:
                OnProgressChanged?.Invoke(this, 0f);
                KitchenObject.DestroySelf();
                KitchenObject.SpawnKitchenObject(_curBurningRecipeSo.Output, this);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void Interact(Player player)
    {
        if (!(this as IKitchenObjectHolder).HasKitchenObject)
        {
            // Counter is empty
            if (!(player as IKitchenObjectHolder).HasKitchenObject) 
                return;
            // Player has KitchenObject
            player.KitchenObject.KitchenObjectHolder = this;
            
        }
        else
        {
            // There is something on counter
            if (!(player as IKitchenObjectHolder).HasKitchenObject)
            {
                KitchenObject.KitchenObjectHolder = player;
            }
            else
            {
                if (player.KitchenObject is not PlateKitchenObject plateKitchenObject) 
                    return;

                if (plateKitchenObject.TryAddIngredient(KitchenObject.KitchenObjectSo)) 
                    KitchenObject.DestroySelf();
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (!(this as IKitchenObjectHolder).HasKitchenObject || _state == State.Frying) 
            return;

        _curFryingRecipeSo = GetFryingRecipeSoFromInput(KitchenObject.KitchenObjectSo);
        if (_curFryingRecipeSo is null)
            return;

        _state = State.Frying;
        OnStateChanged?.Invoke(this, _state);
    }

    private FryingRecipeSO GetFryingRecipeSoFromInput(KitchenObjectSO inputKitchenObjectSo) 
        => _fryingRecipeSos.FirstOrDefault(fryingRecipeSo => fryingRecipeSo.Input == inputKitchenObjectSo);
    
    private BurningRecipeSO GetBurningRecipeSoFromInput(KitchenObjectSO inputKitchenObjectSo) 
        => _burningRecipeSos.FirstOrDefault(burningRecipeSo => burningRecipeSo.Input == inputKitchenObjectSo);
}