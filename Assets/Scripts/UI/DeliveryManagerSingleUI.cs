using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeNameText;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSo)
    {
        _recipeNameText.text = recipeSo.RecipeName;

        foreach (Transform child in _iconContainer)
        {
            if (child == _iconTemplate)
                continue;
            
            Destroy(child.gameObject);
        }

        foreach (var kitchenObjectSo in recipeSo.KitchenObjectSos)
        {
            var icon = Instantiate(_iconTemplate, _iconContainer);
            icon.gameObject.SetActive(true);
            icon.TryGetComponent(out Image image);
            image.sprite = kitchenObjectSo.Sprite;
        }
    }
}