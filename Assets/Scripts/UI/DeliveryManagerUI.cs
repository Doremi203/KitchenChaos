using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _recipeTemplate;

    private void Awake()
    {
        _recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeStateChanged += (_, _) => UpdateVisual();
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in _container)
        {
            if (child == _recipeTemplate)
                continue;
            Destroy(child.gameObject);
        }

        foreach (var waitingRecipe in DeliveryManager.Instance.WaitingRecipeSos)
        {
            var recipe = Instantiate(_recipeTemplate, _container);
            recipe.gameObject.SetActive(true);
            recipe.TryGetComponent<DeliveryManagerSingleUI>(out var singleUI);
            singleUI.SetRecipeSO(waitingRecipe);
        }
    }
}