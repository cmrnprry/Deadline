using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Location
{ 
    Desk, Stairs, LivingRoom
}

public class AudioGhostInteraction : MonoBehaviour
{
    [Header("Audio")]
    //If any audio plays on the Interaction
    public List<AudioClip> clips;
    public AudioSource source;
    private AudioClip lastClip;

    [Header("Interaction Manager")]
    public GhostInteractionManager interactionManager;

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
            Debug.Log("Play Sound?");

            if (interactionManager.DecideGhostEncounter(chance))
            {
                PlayRandomSFX();
            }
        }
    }

    void PlayRandomSFX()
    {
        AudioClip clip = clips[RandomElement()];
        
        if (lastClip == clip)
        {
            PlayRandomSFX();
            return;
        }

        lastClip = clip;

        source.PlayOneShot(clip);
    }

    int RandomElement()
    {
        int index = Random.Range(0, clips.Count);

        return index;
    }

    //Turns off trigger for a random amount of time so the player can't continuously activate it
    IEnumerator DelayScare()
    {
        bc.enabled = false;

        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSecondsRealtime(delay);

        bc.enabled = true;
    }
}
