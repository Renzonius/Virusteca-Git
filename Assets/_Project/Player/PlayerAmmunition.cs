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
        //maxAmmunition = GameManager.Instance.playerAmmunitionSO.maxAmmunition;
        //ammunition = GameManager.Instance.playerAmmunitionSO.currentAmmunition;
    }
    public void AddAmmo(int Amount)
    {
        ammunition = Mathf.Clamp(ammunition + Amount, 0, maxAmmunition);
        //GameManager.Instance.playerAmmunitionSO.currentAmmunition = ammunition;

        //UIEvents.AmmoRestoreUI();
    }
    public void RemoveAmmo(int Amount)
    {
        ammunition = Mathf.Clamp(ammunition - Amount, 0, maxAmmunition);
        //GameManager.Instance.playerAmmunitionSO.currentAmmunition = ammunition;

        //UIEvents.AmmoReductionUI();
    }
}
