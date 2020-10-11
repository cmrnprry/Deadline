using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerBoxSabotage : Sabotage
{
    public override void FakeStart(GameObject go)
    {
        Debug.Log("Breaker Box Start");

    }


    public override void ActivateSabotage()
    {
        isSabotaged = true;
        Debug.Log("Breaker Box Sabotage");
    }

    public override void FixSabotage()
    {
        isSabotaged = false;
        Debug.Log("Fix Breaker Box");
        GameManager.Instance.SetIsSabotageActive();
    }
}
