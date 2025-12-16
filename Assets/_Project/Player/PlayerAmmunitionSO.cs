using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerAmmunitionData", menuName = "Player/Create Ammunition Data", order = 1)]
public class PlayerAmmunitionSO : ScriptableObject
{
    public GameObject projectile;
    [Range(6, 12)]
    public int maxAmmunition = 12;
    [Range(0, 12)]
    public int currentAmmunition = 12;

}
