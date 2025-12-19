using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerAmmunitionData", menuName = "Player/Create Ammunition Data", order = 1)]
public class PlayerAmmunitionSO : ScriptableObject
{
    public GameObject projectile;
    [Range(6, 12)]
    public int initialAmmunition = 12;
}
