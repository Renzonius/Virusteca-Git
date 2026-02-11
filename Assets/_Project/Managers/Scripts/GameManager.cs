using System;
using UnityEngine;


public enum GameState { MainMenu, Playing, PlayerInfected, RagedPeople, VirusNeutralized, UncontrolledVirus, Paused }
public class GameManager : MonoBehaviour
{
    public GameState CurrentState;
    public event Action<GameState> OnGameStateChanged;

    [Header("PLAYER DATA")]
    public PlayerHealthSO playerHealthData;
    public PlayerAmmunitionSO playerAmmunitionData;
    public event Action OnPlayerLose;

    [Header("UI DATA")]
    public VirusSliderSO virusSliderData;
    public RageSliderSO rageSliderData;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState);

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.RagedPeople:
                OnPlayerLose?.Invoke();
                Time.timeScale = 0f;
                break;
            case GameState.PlayerInfected:
                OnPlayerLose?.Invoke();
                Time.timeScale = 0f;
                break;
            case GameState.UncontrolledVirus:
                OnPlayerLose?.Invoke();
                Time.timeScale = 0f;
                break;
        }
    }
}
