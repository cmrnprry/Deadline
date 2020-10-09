using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSabotage : Sabotage
{
    public override void ActivateSabotage()
    {
        Debug.Log("TV Sabotage");
    }

    public override void FixSabotage()
    {
        Debug.Log("Fix TV");
    }
}
