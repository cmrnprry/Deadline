using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    ScreenOne, ScreenTwo, Walking
}


public class GameManager : MonoBehaviour
{
    // Keeps track of the player game state
    public static GameState state = GameState.ScreenOne;

    //If true, the player was last looking at screenOne
    //If false, the player was last looking at screenTwo
    public bool isScreenOne;
    public ScreenManager screenManager;

    private void Start()
    {
        StartCoroutine(WaitingForScreenChange());
    }

    // Update is called once the state is changed
    void UpdateGameState()
    {
        switch (state)
        {
            case GameState.ScreenOne:
                isScreenOne = true;
                screenManager.ChangeCameraView(isScreenOne);
                break;

            case GameState.ScreenTwo:
                isScreenOne = false;
                screenManager.ChangeCameraView(isScreenOne);
                break;

            case GameState.Walking:
                Debug.Log("The player is now standing");
                break;
            default:
                break;
        }
    }


    //Waits until the button to change the screens appears then changes it
    IEnumerator WaitingForScreenChange()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Switch Screens"));

        _ = state == GameState.ScreenOne ? state = GameState.ScreenTwo : state = GameState.ScreenOne;
        UpdateGameState();

        yield return new WaitForEndOfFrame();

        StartCoroutine(WaitingForScreenChange());
    }
}
