using System;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private StoveCounter _stoveCounter;

    private void Start()
    {
        _stoveCounter.OnStateChanged += (sender, state) =>
        {
            var needToPlaySound = state is StoveCounter.State.Frying or StoveCounter.State.Burning;
            if (needToPlaySound)
                _audioSource.Play();
            else
                _audioSource.Pause();
        };
    }
}