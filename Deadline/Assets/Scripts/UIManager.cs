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
        StartCoroutine(WaitingForScreenChange());
    }

    //Turns off the correct Canvas
    private void TurningoffOther(bool isScreenOne)
    {
        screenOne.gameObject.SetActive(!isScreenOne);
        screenTwo.gameObject.SetActive(isScreenOne);
    }

    //Conntected to the "Switch Screen" Button on the canvases
    //Checks which scree is currently on and switches the state
    public void ChangeCamera(bool isCameraOne)
    {
        _ = (isCameraOne == true) ? GameManager.Instance.state = GameState.ScreenTwo : GameManager.Instance.state = GameState.ScreenOne;
        GameManager.Instance.UpdateGameState();
        TurningoffOther(isCameraOne);
    }

    //Waits until the button to change the screens appears then changes it
    IEnumerator WaitingForScreenChange()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Switch Screens"));

        bool isCameraOne = (GameManager.Instance.state == GameState.ScreenOne) ? true : false;
        ChangeCamera(isCameraOne);

        yield return new WaitForEndOfFrame();

        StartCoroutine(WaitingForScreenChange());
    }

    //Turns off all the COmputer UI and hides the mouse
    public void StopUI()
    {
        screenOne.gameObject.SetActive(false);
        screenTwo.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;

        StopAllCoroutines();
    }


    //Turns the mouse cursor and correct screen back on, and restarts the waiting for screen change coroutine
    public void StartUI()
    {
        Cursor.lockState = CursorLockMode.None;
        ChangeCamera(GameManager.Instance.isScreenOne);
        StartCoroutine(WaitingForScreenChange());
    }
}
