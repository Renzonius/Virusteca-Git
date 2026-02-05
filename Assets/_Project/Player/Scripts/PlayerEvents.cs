using System;
using UnityEngine;


public static class PlayerEvents
{
    public static Action<int> OnPlayerHealthChanged;
    public static Action<int> OnPlayerAmmoChanged;
    public static Action OnPlayerDeath;
    public static void PlayerHealthChanged(int currentHealth) 
        => OnPlayerHealthChanged?.Invoke(currentHealth);

    public static void PlayerAmmoChanged(int currentAmmo) 
        => OnPlayerAmmoChanged?.Invoke(currentAmmo);

    //public static void PlayerDeath() 
    //    => OnPlayerDeath?.Invoke();
}
