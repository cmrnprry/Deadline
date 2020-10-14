using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteractionManager : MonoBehaviour
{
    //IMPORTANT: I AM BEING SO LAZY AND THESE ALL NEED TO BE IN THE SAME ORDER AS THE ABOVE LIST
    [Header("Objects to be Sabotaged")]
    public List<BoxCollider> objects;

    //How long between sabotages there are
    public float delay = 5f;

    //These are the "fitness values" in the roulette wheel algorythm  
    [Header("Chance")]
    public int sabotageChanceLow;
    public int sabotageChanceNormal;
    public int sabotageChanceMedium;
    public int sabotageChanceHigh;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DecideGhostEncounter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //SO IM NOT REALLY SURE HOW THIS WORKS BUT IT'S MORE OR LESS THE ROULETTE ALGORITM
    //It works the same / similar way that the Sabotage Manager decides stuff
    IEnumerator DecideGhostEncounter()
    {
        Debug.Log("Deciding");
        float sum = 0, random = 0, curr = GameManager.Instance.currSpooky;
        float tvFit = 0, doorFit = 0, internetFit = 0, breakerFit = 0;

        if (curr <= sabotageChanceLow)
        {
            sum = weightTVLow + weightDoorLow + weightInternetLow + weightBreakerLow;

            tvFit = weightTVLow / sum;
            doorFit = weightDoorLow / sum;
            internetFit = weightInternetLow / sum;
            breakerFit = weightBreakerLow / sum;
        }
        else if (curr <= sabotageChanceNormal)
        {
            sum = weightTVNormal + weightDoorNormal + weightInternetNormal + weightBreakerNormal;

            tvFit = weightTVNormal / sum;
            doorFit = weightDoorNormal / sum;
            internetFit = weightInternetNormal / sum;
            breakerFit = weightBreakerNormal / sum;
        }
        else if (curr <= sabotageChanceMedium)
        {
            sum = weightTVMedium + weightDoorMedium + weightInternetMedium + weightBreakerMedium;

            tvFit = weightTVMedium / sum;
            doorFit = weightDoorMedium / sum;
            internetFit = weightInternetMedium / sum;
            breakerFit = weightBreakerMedium / sum;
        }
        else if (curr <= sabotageChanceHigh)
        {
            sum = weightTVHigh + weightDoorHigh + weightInternetHigh + weightBreakerHigh;

            tvFit = weightTVHigh / sum;
            doorFit = weightDoorHigh / sum;
            internetFit = weightInternetHigh / sum;
            breakerFit = weightBreakerHigh / sum;
        }

        float[] values = { tvFit, doorFit, internetFit, breakerFit };
        random = GetPercentChance();

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] >= random && !sabotages[i].isSabotaged)
            {
                sabotages[i].ActivateSabotage();
                GameManager.Instance.SetIsSabotageActive();
            }
        }

        yield return new WaitForSecondsRealtime(delay);
        StartCoroutine(DecideGhostEncounter());
    }
}
