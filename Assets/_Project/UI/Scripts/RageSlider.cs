using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RageSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float maxRageValue;
    [SerializeField] private float currentRageValue;
    private Coroutine rageDownCoroutine; 
    void Start()
    {
        InitializedRageData();
        GameEvents.OnRageUp += UpdateRageSlider;
    }
    private void OnDisable()
    {
        GameEvents.OnRageUp -= UpdateRageSlider;
    }
    private void InitializedRageData()
    {
        maxRageValue = GameManager.Instance.rageSliderData.maxRageLevel;
        currentRageValue = GameManager.Instance.rageSliderData.currentRageLevel;
    }

    private void UpdateRageSlider(float currentRage)
    {
        currentRageValue = Mathf.Clamp(currentRageValue + currentRage, 0, maxRageValue);
        slider.value = currentRageValue;

        if (currentRageValue >= maxRageValue)
            GameEvents.RageOutOfControl();

        if (rageDownCoroutine != null) 
            StopCoroutine(rageDownCoroutine);

        rageDownCoroutine = StartCoroutine(RageDownCoroutine());
    }

    private IEnumerator RageDownCoroutine()
    {
        yield return new WaitForSeconds(3f); 
        while (currentRageValue > 0f)
        {
            currentRageValue -= 1f; // Ajusta la velocidad de disminución aquí
            slider.value = currentRageValue;
            yield return new WaitForSeconds(0.1f); // Ajusta el intervalo de tiempo aquí
        }
        currentRageValue = 0;
        slider.value = currentRageValue;

        rageDownCoroutine = null;
    }

}
