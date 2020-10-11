using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetSabotage : Sabotage
{
    public override void FakeStart(GameObject go)
    {
        Debug.Log("Internet Start");

    }

    public override void ActivateSabotage()
    {
        isSabotaged = true;
        Debug.Log("Internet Sabotage");
    }

    public override void FixSabotage()
    {
        isSabotaged = false;
        Debug.Log("Fix Internet");
        GameManager.Instance.SetIsSabotageActive();
    }
}
