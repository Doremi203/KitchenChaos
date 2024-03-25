using System;
using Interfaces;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectHolder
{
    public static Player Instance { get; private set; }

    public event EventHandler<BaseCounter> OnSelectedCounterChanged;
    public event EventHandler OnPickedUpSomething;

    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _rotationSpeed = 12f;
    [SerializeField] private LayerMask _countersLayerMask;
    private CharacterController _characterController;
    
    private Vector3 _moveDir = Vector3.zero;
    private Transform _transformCashed;
    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;

    public bool IsWalking { get; private set; }

    public KitchenObject KitchenObject
    {
        get => _kitchenObject;
        set
        {
            _kitchenObject = value;
            if (_kitchenObject is not null) 
                OnPickedUpSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    [field: SerializeField]
    public Transform CounterTopPoint { get; set; }

    private void Awake()
    {
        if (Instance != null) 
            Debug.Log("Error");

        Instance = this;
    }

    private void Start()
    {
        var gameInput = GameInput.Instance;
        _characterController = GetComponent<CharacterController>();
        
        gameInput.OnInteract += HandleInteraction;
        gameInput.OnInteractAlternative += HandleAlternativeInteraction;
    }

    private void Update()
    {
        _transformCashed = transform;
        HandleMovement();
        HandleCounterSelection();
    }
    
    private void HandleAlternativeInteraction()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;
        if (_selectedCounter is null)
            return;
        
        _selectedCounter.InteractAlternate(this);
    }

    private void HandleInteraction()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;
        if (_selectedCounter is null)
            return;
        
        _selectedCounter.Interact(this);
    }

    private void HandleCounterSelection()
    {
        const float selectDistance = 2f;
        var prevSelectedCounter = _selectedCounter;

        if (!Physics.Raycast(_transformCashed.position, _transformCashed.forward, out var raycastHit,
                selectDistance, _countersLayerMask))
        {
            _selectedCounter = null;
        }
        else
        {
            raycastHit.transform.TryGetComponent(out _selectedCounter);
        }
        
        if (prevSelectedCounter != _selectedCounter) 
            OnSelectedCounterChanged?.Invoke(this, _selectedCounter);
    }

    private void HandleMovement()
    {
        var moveVec = GameInput.Instance.GetMovementVector2Normalized();
        _moveDir.x = moveVec.x;
        _moveDir.z = moveVec.y;
        IsWalking = _moveDir != Vector3.zero;

        if (IsWalking)
            _characterController.Move(_moveSpeed * Time.deltaTime * _moveDir);
        transform.forward = Vector3.Slerp(_transformCashed.forward, _moveDir, _rotationSpeed * Time.deltaTime);
    }
}