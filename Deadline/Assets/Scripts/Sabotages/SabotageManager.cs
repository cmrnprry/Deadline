using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageManager : MonoBehaviour
{
    private List<Sabotage> sabotages = new List<Sabotage>();

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


    //So maybe make a fake chance of everyhting happening depending on the current spooky meter?
    // For the sake of ease, sppok meter will go from 0 - 100 and there are 4 stages?
    // So stage 1 is from 0-25, Stage 2 is 26 - 50 etc.
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
        Debug.Log("random: " + random);
        Debug.Log("tvFit: " + tvFit);
        Debug.Log("doorFit: " + doorFit);
        Debug.Log("internetFit: " + internetFit);
        Debug.Log("breakerFit: " + breakerFit);

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] >= random)
            {
                sabotages[i].ActivateSabotage();
            }
        }

        yield return new WaitForSecondsRealtime(5f);
        StartCoroutine(DecideSabotage());
    }

    private float GetPercentChance()
    {
        float chance = Random.Range(0f, 1f);

        return chance;
    }
}
