using UnityEngine;
using UnityEngine.AI;
public enum NPCState { Idle, Moving }

public class NPCMovementState : MonoBehaviour
{
    [Header("IDLE SETTINGS")]
    [SerializeField] private float idleTimeMin = 1.5f;
    [SerializeField] private float idleTimeMax = 4.0f;
    [SerializeField] private float idleTimer;

    [Header("MOVING SETTINGS")]
    [SerializeField] private Spot currentSpot;
    [SerializeField] private Spot oldSpot;
    [SerializeField] private float moveSpeedMin = 0.5f;
    [SerializeField] private float moveSpeedMax = 2f;
    [SerializeField] float rotationSpeed = 720f; // grados por segundo

    [Header("GENERAL SETTINGS")]
    [SerializeField] private NavMeshAgent agent;
    public NPCState currentState = NPCState.Idle;

    [Header("ANIMATION SETTINGS")]
    [SerializeField] private Animator animator;
    private float animationSpeed = 1f;
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
        }
    }
    private void HandleMoving() //Lógica para cuando el NPC está en movimiento
    {
        agent.isStopped = false;
        RotateTowardsMovement();

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            EnterIdle();
        }
    }

    private void RotateTowardsMovement() //Rota el NPC en la dirección del movimiento
    {
        Vector3 velocity = agent.velocity;

        if (velocity.sqrMagnitude < 0.01f)
            return;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg; //Atan2 devuelve el ángulo en radianes, se convierte a grados.
        angle += 90f;                                                      //Para que el sprite mire en la dirección del movimiento.

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, 
            targetRotation, 
            rotationSpeed * Time.deltaTime
        );
    }

    private void SetRandomDestination()
    {
        TryMoveToNewSpot();

        if (currentSpot == oldSpot)
            return;

        currentState = NPCState.Moving;
        oldSpot = currentSpot;

        float randomSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        agent.speed = randomSpeed;

        animator.speed = randomSpeed;
        animator.SetBool("isWalking", true);

        agent.SetDestination(currentSpot.transform.position);
    }

    private void TryMoveToNewSpot()
    {
        Spot newSpot = SpotManager.Instance.RequestFreeSpot(this);
        if (newSpot == null || currentSpot == newSpot)
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
        if(currentSpot != null)
            transform.rotation = currentSpot.transform.rotation;

        idleTimer = Random.Range(idleTimeMin, idleTimeMax);

        animator.speed = animationSpeed;
        if(animator.GetBool("isWalking") == true)
            animator.SetBool("isWalking", false);

        agent.isStopped = true;
        currentState = NPCState.Idle;
    }
}

