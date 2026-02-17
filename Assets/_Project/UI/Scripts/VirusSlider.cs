using UnityEngine;
using UnityEngine.UI;

public class VirusSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float maxVirusValue;
    [SerializeField] private float currentVirusValue;

    private void Start()
    {
        InitializedVirusData();
        GameEvents.OnVirusUpdate += UpdateVirusSlider;
    }
    private void OnDisable()
    {
        GameEvents.OnVirusUpdate -= UpdateVirusSlider;
    }

    private void InitializedVirusData()
    {
        maxVirusValue = GameManager.Instance.virusSliderData.maxVirusLevel;
        slider.maxValue = maxVirusValue;
        currentVirusValue = GameManager.Instance.virusSliderData.currentVirusLevel;
    }

    private void UpdateVirusSlider(float currentVirus)
    {
        currentVirusValue = Mathf.Clamp(currentVirusValue + currentVirus, 0, maxVirusValue);
        slider.value = currentVirusValue;
        if (currentVirusValue >= maxVirusValue)
            GameEvents.VirusOutOfControl();
    }
}
