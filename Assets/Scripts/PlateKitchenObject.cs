using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<KitchenObjectSO> OnIngredientAdded;
    
    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSos;
    public List<KitchenObjectSO> KitchenObjectsOnThePlate { get; private set; }

    private void Awake()
    {
        KitchenObjectsOnThePlate = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
    {
        if (!_validKitchenObjectSos.Contains(kitchenObjectSo))
            return false;
        if (KitchenObjectsOnThePlate.Contains(kitchenObjectSo))
            return false;
        KitchenObjectsOnThePlate.Add(kitchenObjectSo);
        OnIngredientAdded?.Invoke(this, kitchenObjectSo);
        return true;
    }
}