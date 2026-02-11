using System;

public static class GameEvents
{
    public static Action<float> OnVirusUpdate;
    public static Action<float> OnVirusDown;

    public static Action<float> OnRageUp;
    public static Action<float> OnRageDown;
    public static void VirusUpdate(float currentVirus) 
        => OnVirusUpdate?.Invoke(currentVirus);
    public static void VirusDown(float currentVirus)
        => OnVirusDown?.Invoke(currentVirus);

    public static void RageUp(float currentRage)
        => OnRageUp?.Invoke(currentRage);
    public static void RageDown(float currentRage)
        => OnRageDown?.Invoke(currentRage);
}
