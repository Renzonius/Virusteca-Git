using UnityEngine;

[CreateAssetMenu(fileName = "NewRageSliderData", menuName = "UI/Create Rage Data")]
public class RageSliderSO : ScriptableObject
{
    [Range(0f, 100f)]
    public float maxRageLevel = 100f;
    [Range(0f, 100f)]
    public float currentRageLevel = 0f;
}
