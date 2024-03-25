using UnityEngine;
using UnityEngine.Serialization;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += (_, _) => UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == _iconTemplate)
                continue;
            Destroy(child.gameObject);
        }
        
        foreach (var kitchenObjectSo in _plateKitchenObject.KitchenObjectsOnThePlate)
        {
            var icon = Instantiate(_iconTemplate, transform);
            icon.gameObject.SetActive(true);
            icon.GetComponent<PlateIconUI>().SetKitchenObjectSO(kitchenObjectSo);
        }
    }
}
