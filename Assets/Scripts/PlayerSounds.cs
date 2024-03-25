using System;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public static event EventHandler OnStep;
    [SerializeField] private Player _player;
    [SerializeField] private float _footStepsInterval = 1f;

    private float _footStepsTimer;

    private void Update()
    {
        if (!_player.IsWalking)
            return;

        _footStepsTimer += Time.deltaTime;
        if (_footStepsTimer < _footStepsInterval)
            return;

        _footStepsTimer = 0;
        OnStep?.Invoke(this, EventArgs.Empty);
    }
}