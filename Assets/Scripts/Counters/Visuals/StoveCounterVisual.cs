using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject _stove;
    [SerializeField] private GameObject _particles;
    private StoveCounter _stoveCounter;

    private void Start()
    {
        _stoveCounter = GetComponentInParent<StoveCounter>();
        _stoveCounter.OnStateChanged += (_, state) =>
        {
            var showVisuals = state is StoveCounter.State.Frying or StoveCounter.State.Fried or StoveCounter.State.Burning;
            _stove.SetActive(showVisuals);
            _particles.SetActive(showVisuals);
        };
    }
}