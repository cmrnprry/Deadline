using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sabotage
{
    public abstract void ActivateSabotage();
    public abstract void FixSabotage();
    public abstract void DecideSabotage();

    public int GetPercentChance()
    {
        int chance = Random.Range(1, 100);
        return chance;
    }
}
