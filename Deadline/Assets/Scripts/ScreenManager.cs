using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenManager : MonoBehaviour
{
    [Header("Cameras")]
    public Camera one;
    public Camera two;

    public void ChangeCameraView(bool isScreenOne)
    {
        one.enabled = isScreenOne;
        two.enabled = !isScreenOne;
    }
}
