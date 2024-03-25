using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private ContainerCounter _containerCounter;
    private Animator _animator;
    private static readonly int OpenClose = Animator.StringToHash("OpenClose");

    private void Awake()
    {
        _containerCounter = GetComponentInParent<ContainerCounter>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _containerCounter.OnPlayerGrabbedObject += (_, _) => _animator.SetTrigger(OpenClose);
    }
}