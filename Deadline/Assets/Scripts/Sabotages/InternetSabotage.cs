﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetSabotage : Sabotage
{
    public override void ActivateSabotage()
    {
        Debug.Log("Internet Sabotage");
    }

    public override void DecideSabotage()
    {
        Debug.Log("Internet Sabotage");

        int chance = GetPercentChance();

    }

    public override void FixSabotage()
    {
        throw new System.NotImplementedException();
    }
}
