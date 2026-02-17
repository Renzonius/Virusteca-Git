using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControls : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private List<GameObject> lights;

    private int currentLightIndex = 0;
    private Coroutine lightSequence;
    private void Start()
    {
        lightSequence = StartCoroutine(LightSequenceCoroutine());
    }

    private void OnDisable()
    {
        if (lightSequence != null)
            StopCoroutine(lightSequence);
    }

    private void TurnOffGlobalLight()
    {
        globalLight.intensity = 0f;
    }
    private void TurnOnGlobalLight()
    {
        globalLight.intensity = 0.05f;
    }

    private IEnumerator LightSequenceCoroutine()
    {
        yield return new WaitForSeconds(5f);
        globalLight.intensity = 0.003f;
        yield return new WaitForSeconds(2f);
        globalLight.intensity = 0.05f;

        while (GameState.Playing == GameManager.Instance.CurrentState)
        {
            ActivateLightSequence();
            yield return new WaitForSeconds(5f);
            DeactivateLightSequence();
        }
    }

    private void ActivateLightSequence()
    {
        currentLightIndex = Random.Range(0, lights.Count);
        lights[currentLightIndex].SetActive(true);
    }
    private void DeactivateLightSequence()
    {
        lights[currentLightIndex].SetActive(false);
    }
}
