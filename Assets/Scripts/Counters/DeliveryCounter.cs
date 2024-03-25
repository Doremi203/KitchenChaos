using System;
using Interfaces;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (!(player as IKitchenObjectHolder).HasKitchenObject)
            return;
        if (player.KitchenObject is not PlateKitchenObject plateKitchenObject)
            return;
        
        DeliveryManager.Instance.DeliverOrder(plateKitchenObject);
        plateKitchenObject.DestroySelf();
    }
}