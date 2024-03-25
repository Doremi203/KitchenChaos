using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;
    private bool _isUpdated = true;

    private void Start()
    {
        Hide();
        GameManager.Instance.OnCountDownToStartStarted += (_, _) => Show();
        GameManager.Instance.OnGamePlayingStarted += (_, _) =>
        {
            Hide();
            _isUpdated = false;
        };
    }

    private void Update()
    {
        if (!_isUpdated)
            return;
        
        _countdownText.text = Mathf.Ceil(GameManager.Instance.CountdownToStartTimer).ToString();
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