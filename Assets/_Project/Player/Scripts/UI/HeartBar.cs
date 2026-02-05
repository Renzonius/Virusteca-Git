using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;

    private void OnEnable()
    {
        PlayerEvents.OnPlayerHealthChanged += UpdatePlayerHealthHUD;
    }
    private void OnDisable()
    {
        PlayerEvents.OnPlayerHealthChanged -= UpdatePlayerHealthHUD;
    }


    private void Start()
    {
        InitiatePlayerHealthHUD();
    }

    private void InitiatePlayerHealthHUD()
    {
        int health = GameManager.Instance.playerHealthData.currentHealth;
        for (int i = 0; i < health; i++)
        {
            hearts[i].SetActive(true);
        }
    }
    private void UpdatePlayerHealthHUD(int currentHeart)
    {
        hearts[currentHeart].SetActive(false);
    }
}
