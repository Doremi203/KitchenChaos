using System;
using System.Linq;
using Interfaces;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<float> OnProgressChanged;
    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;
    
    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSos;
    private int _cuttingProgress;
    
    public override void Interact(Player player)
    {
        if (!(this as IKitchenObjectHolder).HasKitchenObject)
        {
            // Counter is empty
            if (!(player as IKitchenObjectHolder).HasKitchenObject) 
                return;
            // Player has KitchenObject
            player.KitchenObject.KitchenObjectHolder = this;
            _cuttingProgress = 0;
            OnProgressChanged?.Invoke(this, 0);
        }
        else
        {
            // There is something on counter
            if (_cuttingProgress != 0)
                return;
            
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
        if (!(this as IKitchenObjectHolder).HasKitchenObject) 
            return;

        var cuttingRecipeSo = GetCuttingRecipeSoFromInput(KitchenObject.KitchenObjectSo);
        if (cuttingRecipeSo is null)
            return;
        
        ++_cuttingProgress;
        var cuttingProgressNormalized = (float)_cuttingProgress / cuttingRecipeSo.CutsToDo;
        OnCut?.Invoke(this, EventArgs.Empty);
        OnAnyCut?.Invoke(this, EventArgs.Empty);
        OnProgressChanged?.Invoke(this, cuttingProgressNormalized);
        
        if (_cuttingProgress < cuttingRecipeSo.CutsToDo)
            return;

        _cuttingProgress = 0;
        KitchenObject.DestroySelf();
        KitchenObject.SpawnKitchenObject(cuttingRecipeSo.Output, this);
    }

    private CuttingRecipeSO GetCuttingRecipeSoFromInput(KitchenObjectSO inputKitchenObjectSo) 
        => _cuttingRecipeSos.FirstOrDefault(cuttingRecipeSo => cuttingRecipeSo.Input == inputKitchenObjectSo);
}