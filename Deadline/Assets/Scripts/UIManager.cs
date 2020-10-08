using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Canvases")]
    public Canvas screenOne;
    public Canvas screenTwo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Turns off the coreect Canvas
    private void TurningoffOther(bool isScreenOne)
    {
        screenOne.gameObject.SetActive(!isScreenOne);
        screenTwo.gameObject.SetActive(isScreenOne);
    }

    //Conntected to the "Switch Screen" Button on the canvases
    public void ChangeCamera(bool isCameraOne)
    {
        _ = isCameraOne == true ? GameManager.Instance.state = GameState.ScreenTwo : GameManager.Instance.state = GameState.ScreenOne;
        GameManager.Instance.UpdateGameState();
        TurningoffOther(isCameraOne);
    }
}
