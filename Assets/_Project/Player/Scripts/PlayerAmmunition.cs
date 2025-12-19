using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAmmunition: MonoBehaviour, ICumulative
{
    private int maxAmmunition;
    private int ammunition;


    private void Start()
    {
        InitializedAmmunitionData();
    }
    private void InitializedAmmunitionData()
    {
        maxAmmunition = GameManager.Instance.playerAmmunitionData.initialAmmunition;
        ammunition = maxAmmunition;
    }
    public bool HasAmmo()
    {
        return ammunition > 0;
    }

    public void AddAmmo(int Amount)
    {
        ammunition = Mathf.Clamp(ammunition + Amount, 0, maxAmmunition);
        PlayerEvents.OnPlayerAmmoChanged?.Invoke(ammunition);
    }
    public void RemoveAmmo(int Amount)
    {
        ammunition = Mathf.Clamp(ammunition - Amount, 0, maxAmmunition);
        PlayerEvents.OnPlayerAmmoChanged?.Invoke(ammunition);
    }
}
