using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightHorizontalMovement : MonoBehaviour
{
    [SerializeField] private Light2D light2D;
    [SerializeField] private float delayBetweenPositions;
    [SerializeField] private bool isMoving;
    [SerializeField] private List<Color> lightColors;
    [SerializeField] private List<GameObject> listPositions;
    private int currentColorIndex = 0;
    private Coroutine lightSequence;

    private void OnEnable()
    {
        lightSequence = StartCoroutine(LightCoroutine());
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(lightSequence));
    }

    private IEnumerator MoveRightCoroutine()
    {
        foreach (GameObject position in listPositions)
        {
            transform.position = position.transform.position;
            SetLightColor(light2D);
            yield return new WaitForSeconds(delayBetweenPositions);
        }
    }

    private IEnumerator MoveLeftCoroutine()
    {
        for (int i = listPositions.Count - 1; i >= 0; i--)
        {
            transform.position = listPositions[i].transform.position;
            SetLightColor(light2D);
            yield return new WaitForSeconds(delayBetweenPositions);
        }
    }

    private IEnumerator LightCoroutine()
    {
        light2D.enabled = true;
        while (isMoving)
        {
            yield return StartCoroutine(MoveRightCoroutine());
            yield return StartCoroutine(MoveLeftCoroutine());
        }
    }

    private void SetLightColor(Light2D CurrentLight)
    {
        CurrentLight.color = lightColors[currentColorIndex];
        if (currentColorIndex < lightColors.Count - 1)
            currentColorIndex++;
        else
            currentColorIndex = 0;
    }

}
