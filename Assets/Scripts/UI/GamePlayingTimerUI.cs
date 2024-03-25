using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingTimerUI : MonoBehaviour
{
    [SerializeField] private Image _clockImage;
    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;

        _clockImage.fillAmount = GameManager.Instance.GamePlayingTimerNormalized;
    }
}