using System;
using Interfaces;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrashed;
    public override void Interact(Player player)
    {
        if (!(player as IKitchenObjectHolder).HasKitchenObject) 
            return;
        
        OnTrashed?.Invoke(this, EventArgs.Empty);
        player.KitchenObject.DestroySelf();
    }
}
