using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightRandomPosition : MonoBehaviour
{
    [SerializeField] private float delayBetweenPositions;
    [SerializeField] private bool isMoving;
    [SerializeField] private List<GameObject> listPositions;

    private Coroutine lightSequence;

    private void OnEnable()
    {
        lightSequence = StartCoroutine(LightCoroutine());
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(lightSequence));
    }

    private IEnumerator LightCoroutine()
    {
        while (isMoving)
        {
            int randomIndex = Random.Range(0, listPositions.Count);
            transform.position = listPositions[randomIndex].transform.position;
            yield return new WaitForSeconds(delayBetweenPositions);
        }
    }
}
