using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnCountDownToStartStarted;
    public event EventHandler OnGamePlayingStarted;
    public event EventHandler OnGameOver;
    
    public static GameManager Instance { get; private set; }
    public bool IsGamePlaying => _state == State.GamePlaying;
    
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
        Pause
    }
    
    [SerializeField] private float _gamePlayingTimerMax = 10f;
    
    private State _state;
    private State _stateBeforePause;
    private float _waitingToStartTimer = 1f;
    public float CountdownToStartTimer { get; private set; } = 3f;
    private float _gamePlayingTimer;

    public float GamePlayingTimerNormalized => _gamePlayingTimer / _gamePlayingTimerMax;

    private void Awake()
    {
        _state = State.WaitingToStart;
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnPause += () =>
        {
            if (_state == State.Pause)
            {
                _state = _stateBeforePause;
                Time.timeScale = 1f;
            }
            else
            {
                _stateBeforePause = _state;
                _state = State.Pause;
                Time.timeScale = 0f;
            }
        };
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                _waitingToStartTimer -= Time.deltaTime;
                if (_waitingToStartTimer < 0f)
                {
                    _state = State.CountdownToStart;
                    OnCountDownToStartStarted?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                CountdownToStartTimer -= Time.deltaTime;
                if (CountdownToStartTimer < 0f)
                {
                    _state = State.GamePlaying;
                    OnGamePlayingStarted?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                _gamePlayingTimer += Time.deltaTime;
                if (_gamePlayingTimer > _gamePlayingTimerMax)
                {
                    _state = State.GameOver;
                    OnGameOver?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
            case State.Pause:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}