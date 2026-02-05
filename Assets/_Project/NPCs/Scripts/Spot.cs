using UnityEngine;

public class Spot : MonoBehaviour
{
    public Vector3 position;
    public NPCMovementState occupant; // null = libre

    private void Start()
    {
        position = transform.localPosition;
    }
}
