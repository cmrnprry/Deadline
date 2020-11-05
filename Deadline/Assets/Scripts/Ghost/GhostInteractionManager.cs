using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteractionManager : MonoBehaviour
{

    //The chance is backwards I'm sorry
    // SO the lower the chance var is, the more likely it will happen
    // this is bc the sppoky meter is more intense, the higher it is
    public bool DecideGhostEncounter(float chance)
    {
        Debug.Log("Deciding");
        float curr = GameManager.Instance.currSpooky;

        if (chance <= curr)
        {
            return true;
        }

        return false;
    }

}
