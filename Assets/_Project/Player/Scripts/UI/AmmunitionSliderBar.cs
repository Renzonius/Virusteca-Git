using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmunitionSliderBar : MonoBehaviour
{
    [SerializeField] private float waitToFillDose; // buen valor 0.0001f
    [SerializeField] private Dose[] ammunitionDoseSliders;
    private int ammo;


    private void OnEnable()
    {
        PlayerEvents.OnPlayerAmmoChanged += HandlePlayerAmmoChanged;
    }
    private void OnDisable()
    {
        PlayerEvents.OnPlayerAmmoChanged += HandlePlayerAmmoChanged;

    }


    private void Start()
    {
        InitializedPlayerAmmoHUDData();
        StartCoroutine(nameof(InitializeAmmoBarCoroutine));
    }

    private void InitializedPlayerAmmoHUDData()
    {
        ammo = GameManager.Instance.playerAmmunitionData.initialAmmunition;
    }

    private IEnumerator InitializeAmmoBarCoroutine()
    {
        for (int i = 0; i < ammo; i++)
        {
            if (ammunitionDoseSliders[i].isFull == false)
            {
                ammunitionDoseSliders[i].AddLiquid(1f);
                yield return new WaitForSeconds(waitToFillDose);
            }
        }
    }


    private void HandlePlayerAmmoChanged(int currentAmmo)
    {
        if (currentAmmo > ammo)
        {
            int ammoToAdd = currentAmmo - ammo;
            AddLiquid(ammoToAdd);
        }
        else if (currentAmmo < ammo)
        {
            float ammoToLess = ammo - currentAmmo;
            LessLiquid(currentAmmo);
        }
        ammo = currentAmmo;

    }

    private void AddLiquid(int ammoToAdd)
    {
        int doseIndex = 0;
        int dosesToAdd = ammoToAdd;
        foreach (Dose dose in ammunitionDoseSliders)
        {
            if (dose.isFull == true)
            {
                doseIndex++;
            }
        }

        for (int i = doseIndex; i < ammunitionDoseSliders.Length; i++)
        {
            if(dosesToAdd > 0)
            {
                ammunitionDoseSliders[i].AddLiquid(1f);
                dosesToAdd--;
            }
            else
            {
                break;
            }
        }
    }
    private void LessLiquid(int newAmmo)
    {
        for(int i = ammunitionDoseSliders.Length - 1; i >= 0; i--)
        {
            if (ammunitionDoseSliders[i].isFull == true)
            {
                ammunitionDoseSliders[i].LessLiquid(0f);
                ammo = newAmmo;
                break;
            }
        }
    }

}
