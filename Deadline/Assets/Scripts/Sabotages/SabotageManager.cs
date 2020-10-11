using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SabotageType
{
    TV = 0, DoorBell = 1, Internet = 2, BreakerBox = 4
}

public class SabotageManager : MonoBehaviour
{
    private List<Sabotage> sabotages = new List<Sabotage>();

    //IMPORTANT: I AM BEING SO LAZY AND THESE ALL NEED TO BE IN THE SAME ORDER AS THE ABOVE LIST
    [Header("Objects to be Sabotaged")]
    public List<GameObject> objects;

    //How long between sabotages there are
    public float delay = 5f;

    [Header("Internet MiniGame")]
    //bool to check if the Internet button is being held down
    private bool isHeld = false;
    //bool to check if you've waited long enough
    private bool isFinished = false;

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
        //IMPORTANT: I AM BEING SO LAZY AND THESE ALL NEED TO BE IN THIS ORDER
        sabotages.Add(new TVSabotage());
        sabotages.Add(new DoorbellSabotage());
        sabotages.Add(new InternetSabotage());
        sabotages.Add(new BreakerBoxSabotage());

        //I just need them to have a Start method :(
        for (int i = 0; i < sabotages.Count; i++)
        {
            sabotages[i].FakeStart(objects[i]);
        }

        sabotages[2].ActivateSabotage();
        //StartCoroutine(DecideSabotage());
    }


    //SO IM NOT REALLY SURE HOW THIS WORKS BUT IT'S MORE OR LESS THE ROULETTE ALGORITM
    //BASICALLY everything in the inspector is a weight. The higher the number, the more likely it is to happen
    //What this code does is takt the sum of them, find the value that each weight lies between 0 & 1, then picks a random number between 0 & 1
    //If that random number is less than or equal to the wieght, it triggers the sabotage.
    //This will call itself every X number of seconds that we decide. We can also ver easily change this to a range.
    IEnumerator DecideSabotage()
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
        StartCoroutine(DecideSabotage());
    }

    //Tells the game that the sabotage has been fixed
    public void StopSabatage(SabotageType st)
    {
        sabotages[(int)st].FixSabotage();
    }

    //Checks if there are any active sabotages
    public bool CheckActiveSabotages()
    {
        for (int i = 0; i < sabotages.Count; i++)
        {
            if (sabotages[i].isSabotaged)
            {
                return true;
            }
        }

        return false;
    }


    //Gets a random number between 0 and 1 inclusive
    private float GetPercentChance()
    {
        float chance = Random.Range(0f, 1f);

        return chance;
    }

    ////////////////////////   INTERNET MINI GAME CODE     /////////////////////////////////////////////////////

    //Is called when the player clicks on the button
    public void StartRestartInternet()
    {
        Debug.Log("start");

        isHeld = true;
        StartCoroutine(RestartInternet());
    }

    //This is called when the player releaces the mouse on the "resetart" button
    public void InternetGameWon()
    {
        if (isFinished && isHeld)
        {
            sabotages[2].FixSabotage();
            Debug.Log("won");
        }
        else
        {
            Debug.Log("not won");
        }
    }

    //While the player is holding down the button
    IEnumerator RestartInternet()
    {
        yield return new WaitForSecondsRealtime(15f);
        isFinished = true;
        Debug.Log("Finished!");
    }


}
