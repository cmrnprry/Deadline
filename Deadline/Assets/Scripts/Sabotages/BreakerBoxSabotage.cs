using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerBoxSabotage : Sabotage
{
    public override void ActivateSabotage()
    {
        Debug.Log("Breaker Box Sabotage");
    }

    public override void FixSabotage()
    {
        Debug.Log("Fix Breaker Box");
    }
}
