using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSabotage : Sabotage
{
    public GameObject go;

    public override void FakeStart(GameObject go)
    {
        Debug.Log("TV Start");

    }


    public override void ActivateSabotage()
    {
        isSabotaged = true;
        Debug.Log("TV Sabotage");
    }

    public override void FixSabotage()
    {
        isSabotaged = false;
        Debug.Log("Fix TV");
        GameManager.Instance.SetIsSabotageActive();
    }
}
