using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetSabotage : Sabotage
{
    public override void ActivateSabotage()
    {
        Debug.Log("Internet Sabotage");
    }

    public override void FixSabotage()
    {
        Debug.Log("Fix Internet");
    }
}
