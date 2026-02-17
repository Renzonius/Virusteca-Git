using System;

public static class GameEvents
{
    public static Action<float> OnVirusUpdate;
    public static Action OnVirusOutOfControl;

    public static Action<float> OnRageUp;
    public static Action OnRageOutOfControl;

    public static Action OnPlayerInfected;

    public static Action OnPlayerLose;
    public static Action OnPlayerWin;


    public static void VirusUpdate(float currentVirus) 
        => OnVirusUpdate?.Invoke(currentVirus);
    public static void VirusOutOfControl() 
        => OnVirusOutOfControl?.Invoke();
    public static void RageUp(float currentRage)
        => OnRageUp?.Invoke(currentRage);
    public static void RageOutOfControl()
        => OnRageOutOfControl?.Invoke();
    public static void PlayerInfected()
        => OnPlayerInfected?.Invoke();
    public static void PlayerLose()
        => OnPlayerLose?.Invoke();
    public static void PlayerWin()
        => OnPlayerWin?.Invoke();
}
