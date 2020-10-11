using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVSabotage : Sabotage
{
    public GameObject obj;

    public override void FakeStart(GameObject go)
    {
        Debug.Log("TV Start");
        obj = go;

    }


    public override void ActivateSabotage()
    {
        isSabotaged = true;
        obj.GetComponent<Renderer>().material.color = Color.blue;
        Debug.Log("TV Sabotage");
    }

    public override void FixSabotage()
    {
        isSabotaged = false;
        obj.GetComponent<Renderer>().material.color = Color.black;
        Debug.Log("Fix TV");
        GameManager.Instance.SetIsSabotageActive();
    }
}
