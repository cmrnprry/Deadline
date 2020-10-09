using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenManager : MonoBehaviour
{
    [Header("Cameras")]
    public Camera one;
    public Camera two;
    public GameObject player;

    void Start()
    {
        StartCoroutine(WaitingForStanding());
    }

    //Changes which camera is enabled
    public void ChangeCameraView(bool isScreenOne)
    {
        one.enabled = isScreenOne;
        two.enabled = !isScreenOne;
    }

    //Turns on the player when switching to free roam
    public void StandingCamera()
    {
        player.SetActive(true);
    }

    //Waits until the button to stand up is pressed
    //Changes to free roam state when it does
    IEnumerator WaitingForStanding()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Standing"));
        Debug.Log("Standing");

        GameManager.Instance.state = GameState.Walking;
        GameManager.Instance.UpdateGameState();

        yield return new WaitForEndOfFrame();
        
        StartCoroutine(WaitingForSitting());
    }

    //Waits until the button to sit is pressed
    //Changes to computer state when it does
    IEnumerator WaitingForSitting()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Sitting") && GameManager.Instance.canSit);
        Debug.Log("Sitting");

        player.SetActive(false);

        GameManager.Instance.state = GameState.Sitting;
        GameManager.Instance.UpdateGameState();

        yield return null;

        _ = (GameManager.Instance.isScreenOne == false) ? GameManager.Instance.state = GameState.ScreenTwo : GameManager.Instance.state = GameState.ScreenOne;
        GameManager.Instance.UpdateGameState();

        yield return new WaitForEndOfFrame();

        StartCoroutine(WaitingForStanding());
    }
}
