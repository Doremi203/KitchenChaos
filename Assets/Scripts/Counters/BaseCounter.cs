using System;
using Interfaces;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectHolder
{
    private KitchenObject _kitchenObject;
    public static event EventHandler OnObjectPlaced;
    
    [field: SerializeField] 
    public Transform CounterTopPoint { get; set; }

    public KitchenObject KitchenObject
    {
        get => _kitchenObject;
        set
        {
            _kitchenObject = value;
            if (_kitchenObject is not null) 
                OnObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }

    public virtual void Interact(Player player) { }

    public virtual void InteractAlternate(Player player) { }
}