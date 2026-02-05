using UnityEngine;
using UnityEngine.AI;
public enum NPCState { Idle, Moving }

public class NPCMovementState : MonoBehaviour
{
    [Header("IDLE SETTINGS")]
    [SerializeField] private float idleTimeMin = 1.5f;
    [SerializeField] private float idleTimeMax = 4.0f;
    private float idleTimer;

    [Header("MOVING SETTINGS")]
    [SerializeField] private Spot currentSpot;
    [SerializeField] private float moveSpeedMin = 0.5f;
    [SerializeField] private float moveSpeedMax = 2f;

    [Header("GENERAL SETTINGS")]
    [SerializeField] private NavMeshAgent agent;
    public NPCState currentState = NPCState.Idle;

    private void Start()
    {
        // IMPORTANTE para top-down 2D
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    private void Update()
    {
        switch (currentState)
        {
            case NPCState.Idle:
                HandleIdle();
                break;
            case NPCState.Moving:
                HandleMoving();
                break;
        }
    }
    private void HandleIdle() //Lógica para cuando el NPC está inactivo
    {
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f)
        {
            SetRandomDestination();
            currentState = NPCState.Moving;
        }
    }
    private void HandleMoving() //Lógica para cuando el NPC está en movimiento
    {
        agent.isStopped = false;
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("Reached destination");
            EnterIdle();
        }
    }


    private void SetRandomDestination()
    {
        TryMoveToNewSpot();
        float randomSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        agent.speed = randomSpeed;
        agent.SetDestination(currentSpot.transform.localPosition);
        Debug.Log("Moving to new spot at position: " + currentSpot.transform.localPosition);
    }

    private void TryMoveToNewSpot()
    {
        Spot newSpot = SpotManager.Instance.RequestFreeSpot(this);
        if(newSpot == null)
        {
            EnterIdle();
            return;
        }
        SpotManager.Instance.ReleaseSpot(currentSpot, this);

        currentSpot = newSpot;
    }

    private void EnterIdle()
    {
        agent.ResetPath();
        idleTimer = Random.Range(idleTimeMin, idleTimeMax);
        currentState = NPCState.Idle;
        agent.isStopped = true;
    }
}

