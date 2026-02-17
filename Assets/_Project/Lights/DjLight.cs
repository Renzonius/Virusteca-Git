using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DjLight : MonoBehaviour
{
    public bool isMoving;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 randomPosition;
    private Coroutine moveLight;

    private void OnEnable()
    {
        SelectRandomPosition();
        moveLight = StartCoroutine(MoveLightCoroutine());
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(moveLight));
    }


    private IEnumerator MoveLightCoroutine()
    {
        while (isMoving)
        {
            if (Vector3.Distance(transform.position, randomPosition) < 0.25f)
            {
                SelectRandomPosition();
                yield return new WaitForSeconds(3f);
            }
            MoveLight(randomPosition);
            yield return null;
        }
    }
    private Vector3 SelectRandomPosition()
    {
        randomPosition = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-3.5f, 3.5f), transform.position.z);
        return randomPosition;
    }

    //Con este metodo el objeto se mueve hasta la posicion aleatoria.
    private void MoveLight(Vector2 RandomDiretion)
    {
        transform.position = Vector3.MoveTowards(transform.position, randomPosition, moveSpeed * Time.deltaTime);
    }
}
