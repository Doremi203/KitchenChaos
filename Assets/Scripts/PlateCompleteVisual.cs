using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObjectSo;
        public GameObject GameObject;
    }

    [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSoGameObjects;
    
    private PlateKitchenObject _plateKitchenObject;
    private Dictionary<KitchenObjectSO, GameObject> _gameObjects;

    private void Awake()
    {
        
        _plateKitchenObject = GetComponentInParent<PlateKitchenObject>();
    }

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += (_, kitchenObjectSo) =>
        {
            foreach (var kitchenObjectSoGameObject in _kitchenObjectSoGameObjects)
            {
                if (kitchenObjectSoGameObject.KitchenObjectSo == kitchenObjectSo)
                    kitchenObjectSoGameObject.GameObject.SetActive(true);
            }
        };
    }
}