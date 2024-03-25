using Interfaces;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [field: SerializeField]
    public KitchenObjectSO KitchenObjectSo { get; set; }
    
    private IKitchenObjectHolder _kitchenObjectHolder;

    public IKitchenObjectHolder KitchenObjectHolder
    {
        get => _kitchenObjectHolder;
        set
        {
            if (_kitchenObjectHolder is not null)
                _kitchenObjectHolder.ClearKitchenObject();
            
            _kitchenObjectHolder = value;
            
            if (_kitchenObjectHolder.HasKitchenObject) 
                Debug.Log("KitchenObjectHolder already has KitchenObject");
            
            _kitchenObjectHolder.KitchenObject = this;
            var transformCashed = transform;
            transformCashed.parent = _kitchenObjectHolder.CounterTopPoint;
            transformCashed.localPosition = Vector3.zero;
            transformCashed.rotation = Quaternion.identity;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo,
        IKitchenObjectHolder kitchenObjectHolder)
    {
        var kitchenObject = Instantiate(kitchenObjectSo.Prefab).GetComponent<KitchenObject>();
        kitchenObject.KitchenObjectHolder = kitchenObjectHolder;
        return kitchenObject;
    }

    public void DestroySelf()
    {
        _kitchenObjectHolder.ClearKitchenObject();
        
        Destroy(gameObject);
    }
}