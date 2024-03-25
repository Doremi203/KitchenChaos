using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;

    public event EventHandler OnRecipeStateChanged;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailure;
    
    [SerializeField] private float _timeToSpawnRecipe = 3f;
    [SerializeField] private int _maxWaitingRecipes = 5;
    [SerializeField] private RecipeListSO _recipeListSo;
    
    private float _spawnRecipeTimer;

    public List<RecipeSO> WaitingRecipeSos { get; private set; }
    public int SuccessfulRecipes { get; private set; }
    private void Awake()
    {
        WaitingRecipeSos = new List<RecipeSO>();
        Instance = this;
    }

    private void Update()
    {
        if (WaitingRecipeSos.Count >= _maxWaitingRecipes)
            return;
        
        _spawnRecipeTimer += Time.deltaTime;

        if (_spawnRecipeTimer < _timeToSpawnRecipe)
            return;

        _spawnRecipeTimer = 0f;
        var waitingRecipeSo = _recipeListSo.RecipeSos[Random.Range(0, _recipeListSo.RecipeSos.Count)];
        WaitingRecipeSos.Add(waitingRecipeSo);
        OnRecipeStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverOrder(PlateKitchenObject plateKitchenObject)
    {
        for (var i = 0; i < WaitingRecipeSos.Count; i++)
        {
            var waitingRecipeSo = WaitingRecipeSos[i];
            if (waitingRecipeSo.KitchenObjectSos.Count != plateKitchenObject.KitchenObjectsOnThePlate.Count)
                continue;

            var flag = true;
           
            foreach (var kitchenObjectSo in waitingRecipeSo.KitchenObjectSos)
            {
                if (!plateKitchenObject.KitchenObjectsOnThePlate.Contains(kitchenObjectSo))
                    flag = false;
            }

            if (!flag)
                continue;

            WaitingRecipeSos.RemoveAt(i);
            OnRecipeStateChanged?.Invoke(this, EventArgs.Empty);
            OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
            SuccessfulRecipes++;
            return;
        }
        
        OnDeliveryFailure?.Invoke(this, EventArgs.Empty);
    }
}