using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerHealthData", menuName = "Player/Create Health Data", order = 1)]

public class PlayerHealthSO : ScriptableObject
{
    public float timeToInvincibility;
    [Range(0,3)]
    public int maxHealth;
    [Range(0, 3)]
    public int currentHealth;
}
