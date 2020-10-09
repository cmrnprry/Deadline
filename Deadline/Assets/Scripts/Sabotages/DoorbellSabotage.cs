using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorbellSabotage : Sabotage
{
    public override void ActivateSabotage()
    {
        Debug.Log("Doorbell Sabotage");
    }

    public override void FixSabotage()
    {
        Debug.Log("Fix Doorbell");
    }
}
