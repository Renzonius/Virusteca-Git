using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("PLAYER DATA")]
    public PlayerHealthSO playerHealthData;
    public PlayerAmmunitionSO playerAmmunitionData;


    public static GameManager Instance { get; private set; }
}
