using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private CuttingCounter _cuttingCounter;
    private Animator _animator;
    private static readonly int Cut = Animator.StringToHash("Cut");

    private void Awake()
    {
        _cuttingCounter = GetComponentInParent<CuttingCounter>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _cuttingCounter.OnCut += (_, _) => _animator.SetTrigger(Cut);
    }
}