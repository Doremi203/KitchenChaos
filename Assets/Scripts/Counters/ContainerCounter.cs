using System;
using Interfaces;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;

    public override void Interact(Player player)
    {
        if ((player as IKitchenObjectHolder).HasKitchenObject)
        {
            if (player.KitchenObject is not PlateKitchenObject plateKitchenObject) 
                return;
            
            KitchenObject.SpawnKitchenObject(_kitchenObjectSo, this);
            if(plateKitchenObject.TryAddIngredient(KitchenObject.KitchenObjectSo))
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            KitchenObject.DestroySelf();
            return;
            
        }
        
        KitchenObject.SpawnKitchenObject(_kitchenObjectSo, player);
        
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}