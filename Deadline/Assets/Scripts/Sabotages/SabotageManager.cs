using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageManager : MonoBehaviour
{
    private List<Sabotage> sabotages = new List<Sabotage>();

    //How long between sabotages there are
    public float delay = 5f;

    //These are the "fitness values" in the roulette wheel algorythm  
    [Header("Chance")]
    public int sabotageChanceLow;
    public int sabotageChanceNormal;
    public int sabotageChanceMedium;
    public int sabotageChanceHigh;

    [Header("Weight Values TV")]
    public float weightTVLow;
    public float weightTVNormal;
    public float weightTVMedium;
    public float weightTVHigh;

    [Header("Weight Values Doorbell")]
    public float weightDoorLow;
    public float weightDoorNormal;
    public float weightDoorMedium;
    public float weightDoorHigh;

    [Header("Weight Values Internet")]
    public float weightInternetLow;
    public float weightInternetNormal;
    public float weightInternetMedium;
    public int weightInternetHigh;

    [Header("Weight Values Breaker Box")]
    public float weightBreakerLow;
    public float weightBreakerNormal;
    public float weightBreakerMedium;
    public float weightBreakerHigh;

    private void Start()
    {
        sabotages.Add(new TVSabotage());
        sabotages.Add(new DoorbellSabotage());
        sabotages.Add(new InternetSabotage());
        sabotages.Add(new BreakerBoxSabotage());

        StartCoroutine(DecideSabotage());
    }


    //SO IM NOT REALLY SURE HOW THIS WORKS BUT IT'S MORE OR LESS THE ROULETTE ALGORITM
    //BASICALLY everything in the inspector is a weight. The higher the number, the more likely it is to happen
    //What this code does is takt the sum of them, find the value that each weight lies between 0 & 1, then picks a random number between 0 & 1
    //If that random number is less than or equal to the wieght, it triggers the sabotage.
    //This will call itself every X number of seconds that we decide. We can also ver easily change this to a range.
    IEnumerator DecideSabotage()
    {
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

        //Debug.Log("random: " + random);
        //Debug.Log("tvFit: " + tvFit);
        //Debug.Log("doorFit: " + doorFit);
        //Debug.Log("internetFit: " + internetFit);
        //Debug.Log("breakerFit: " + breakerFit);

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] >= random)
            {
                sabotages[i].ActivateSabotage();
            }
        }

        yield return new WaitForSecondsRealtime(delay);
        StartCoroutine(DecideSabotage());
    }

    //Gets a random number between 0 and 1 inclusive
    private float GetPercentChance()
    {
        float chance = Random.Range(0f, 1f);

        return chance;
    }
}
