using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    
    public event Action OnInteractAlternative;
    public event Action OnInteract;
    public event Action OnPause;

    public static GameInput Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _playerInputActions.Player.Interact.performed += _ => OnInteract?.Invoke();
        _playerInputActions.Player.InteractAlternative.performed += _ => OnInteractAlternative?.Invoke();
        _playerInputActions.Player.Pause.performed += _ => OnPause?.Invoke();
    }

    public Vector2 GetMovementVector2Normalized() 
        => _playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
}