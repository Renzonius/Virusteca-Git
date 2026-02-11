using UnityEngine;

[CreateAssetMenu(fileName = "NewVirusSliderData", menuName = "UI/Create Virus Data")]
public class VirusSliderSO : ScriptableObject
{
    [Range(0f, 100f)]
    [SerializeField] public float maxVirusLevel = 100f;
    [Range(0f, 100f)]
    [SerializeField] public float currentVirusLevel = 0f;

}
