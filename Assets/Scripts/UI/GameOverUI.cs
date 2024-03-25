using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipesDeliveredText;
    
    private void Start()
    {
        Hide();
        GameManager.Instance.OnGameOver += (_, _) =>
        {
            Show();
            _recipesDeliveredText.text = DeliveryManager.Instance.SuccessfulRecipes.ToString();
        };
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}