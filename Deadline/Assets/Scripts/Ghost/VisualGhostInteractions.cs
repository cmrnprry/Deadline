using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Just using placeholders for now
public enum VisualEncounters
{
    NULL, One, Two, Three
}

public class VisualGhostInteractions : MonoBehaviour
{
    [Header("Audio")]
    //If any audio plays on the Interaction
    public AudioClip SFX;
    public AudioSource source;

    [Header("Interaction Manager")]
    public GhostInteractionManager interactionManager;

    [Header("Interactions")]
    //Interactions may not be enums like this, but it was the easiest thing I could think of
    public List<VisualEncounters> encounters = new List<VisualEncounters>();
    private VisualEncounters lastEncounter = VisualEncounters.NULL;

    [Header("Chance but the LOWER it is the MORE LIKEYLE it is")]
    //Chance of this visual scare happening
    public float chance;
    
    [Header("Delay in Seconds")]
    //Min / Max amount of time to keep the trigger turned off
    public float minDelay;
    public float maxDelay;

    private BoxCollider bc;

    void Start()
    {
        bc = this.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Trigger ghost?");

            if (interactionManager.DecideGhostEncounter(chance))
            {
                ShowGhost();
            }
        }
    }

    //If we are going to have a ghost encounter, show it here
    void ShowGhost()
    {
        VisualEncounters e = encounters[RandomElement()];

        if (lastEncounter == e || e == VisualEncounters.NULL)
        {
            Debug.Log("Trying again");
            ShowGhost();
            return;
        }

        lastEncounter = e;
        Debug.Log("Ghost Triggered: " + e.ToString());

        //This is not GREAT I think
        //But with enums deligating this way is the easiest way I could think of?
        //I figure when triggered, check which interaction it is, and then send them to their own methods that will then make teh scary thing appear
        switch (e)
        {
            case VisualEncounters.One:
                break;
            case VisualEncounters.Two:
                break;
            case VisualEncounters.Three:
                break;
            default:
                Debug.LogError("This Encounter does not exist");
                break;
        }

        StartCoroutine(DelayScare());
    }

    //Turns off trigger for a random amount of time so the player can't continuously activate it
    IEnumerator DelayScare()
    {
        bc.enabled = false;

        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSecondsRealtime(delay);

        bc.enabled = true;
    }


    //Gets a random number that is between 0 and the length of the list of encounters
    int RandomElement()
    {
        int index = Random.Range(0, encounters.Count);

        return index;
    }
}
