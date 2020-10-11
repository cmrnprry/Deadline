using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sabotage
{
    //bool to check if the object is already sabotaged
    public bool isSabotaged;

    //Fake Start for each sabotage
    public abstract void FakeStart(GameObject go);

    //Activates a given Sbotage
    public abstract void ActivateSabotage();

    //Fixes a sabotage
    public abstract void FixSabotage();
}
