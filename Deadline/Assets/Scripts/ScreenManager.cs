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

    public void ChangeCameraView(bool isScreenOne)
    {
        one.enabled = isScreenOne;
        two.enabled = !isScreenOne;
    }

    public void StandingCamera()
    {
        player.SetActive(true);
    }

    //Waits until the button to stand up
    IEnumerator WaitingForStanding()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Standing"));
        Debug.Log("Standing");

        GameManager.Instance.state = GameState.Walking;
        GameManager.Instance.UpdateGameState();

        yield return new WaitForEndOfFrame();
        
        StartCoroutine(WaitingForSitting());
    }

    //Back to Sitting
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
