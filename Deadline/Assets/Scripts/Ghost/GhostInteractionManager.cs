using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteractionManager : MonoBehaviour
{
    [Header("List of Visual Ghost INteractions")]
    public List<VisualGhostInteractions> visual;

    //These are the "fitness values" in the roulette wheel algorythm  
    [Header("Chance")]
    public int ghostChanceLow;
    public int ghostChanceNormal;
    public int ghostChanceMedium;
    public int ghostChanceHigh;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DecideGhostEncounter());
    }

    //SO IM NOT REALLY SURE HOW THIS WORKS BUT IT'S MORE OR LESS THE ROULETTE ALGORITM
    //It works the same / similar way that the Sabotage Manager decides stuff
    public bool DecideVisualGhostEncounter(float chance)
    {
        Debug.Log("Deciding");
        float curr = GameManager.Instance.currSpooky;

        if (chance >= curr)
        {
            return true;
        }

        return false;
    }

    //Will decide when to trigger auditory ghosts
    //Will be purely auditory
    void AuditoryGhostTriggers()
    {

    }
}
