using Interfaces;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;

    public override void Interact(Player player)
    {
        if (!(this as IKitchenObjectHolder).HasKitchenObject)
        {
            // Counter is empty
            if ((player as IKitchenObjectHolder).HasKitchenObject)
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
                if (player.KitchenObject is PlateKitchenObject plateKitchenObject)
                {
                    if (plateKitchenObject.TryAddIngredient(KitchenObject.KitchenObjectSo)) 
                        KitchenObject.DestroySelf();
                }
                else if (KitchenObject is PlateKitchenObject plateOnCounter)
                {
                    if (plateOnCounter.TryAddIngredient(player.KitchenObject.KitchenObjectSo))
                    {
                        player.KitchenObject.DestroySelf();
                    }
                }
            }
        }
    }
}
