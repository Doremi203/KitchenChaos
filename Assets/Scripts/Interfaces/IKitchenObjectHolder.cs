using UnityEngine;

namespace Interfaces
{
    public interface IKitchenObjectHolder
    {
        public KitchenObject KitchenObject { get; set; }
        public Transform CounterTopPoint { get; set; }
        public bool HasKitchenObject => KitchenObject != null;
        
        public void ClearKitchenObject()
        {
            KitchenObject = null;
        }
    }
}