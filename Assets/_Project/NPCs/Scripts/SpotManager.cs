using System.Collections.Generic;
using UnityEngine;

public class SpotManager : MonoBehaviour
{
    public static SpotManager Instance;

    public List<Spot> spots = new List<Spot>();

    private void Awake()
    {
        Instance = this;
    }

    public Spot RequestFreeSpot(NPCMovementState npc) //Retorna un Spot libre para el NPC.
    {
        List<Spot> freeSpots = spots.FindAll(spot => spot.occupant == null);

        if (freeSpots.Count == 0)
            return null;

        Spot chosen = freeSpots[Random.Range(0, freeSpots.Count)];
        chosen.occupant = npc;
        return chosen;
    }

    public void ReleaseSpot(Spot spot, NPCMovementState npc) //Libera el Spot ocupado por el NPC.
    {
        if (spot != null && spot.occupant == npc) 
            spot.occupant = null;
    }

}
