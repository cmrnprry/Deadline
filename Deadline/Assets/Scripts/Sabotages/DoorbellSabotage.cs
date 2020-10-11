using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorbellSabotage : Sabotage
{
    private AudioSource audio;

    public override void FakeStart(GameObject go)
    {
        Debug.Log("DoorBell Start");
        audio = go.GetComponent<AudioSource>();
    }


    public override void ActivateSabotage()
    {
        isSabotaged = true;
        audio.Play();
        Debug.Log("Doorbell is ringing");
    }

    public override void FixSabotage()
    {
        isSabotaged = false;
        audio.Stop();
        Debug.Log("Fixed Doorbell");
        GameManager.Instance.SetIsSabotageActive();
    }
}
