using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    ScreenOne, ScreenTwo, Walking, Sitting
}

public class GameManager : MonoBehaviour
{
    [Header("State")]
    // Keeps track of the player game state
    public GameState state = GameState.ScreenOne;

    [Header("Flags")]
    //If true, the player was last looking at screenOne
    //If false, the player was last looking at screenTwo
    public bool isScreenOne = true;
    public bool canSit = false;

    [Header("Spooky Meter")]
    public float currSpooky; // current number the meter is at
    public float delta; //how much it will go up over time
    public float activeDelta; //how much it will go up when the player is ignoring a sabotage
    public float maxSpooky; // the max amount it can be at
    public bool isSabotageActive; //true if there is an active sabotage

    [Header("References to other scripts :(")]
    public ScreenManager screenManager;
    public UIManager uiManager;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    private void Start()
    {
        StartCoroutine(SpookyMeter());
    }

    //Loop to check the meter every frame?
    //I'm sorry I just refuse to use the update function,
    //it has hurt me too many times in the past :(
    IEnumerator SpookyMeter()
    {
        currSpooky = (currSpooky < maxSpooky) ? currSpooky + delta : currSpooky;
        currSpooky = (currSpooky < maxSpooky && isSabotageActive) ? currSpooky + activeDelta : currSpooky;

        yield return new WaitForFixedUpdate();

        StartCoroutine(SpookyMeter());
    }
    
    // Is called everytime the game state is updated
    public void UpdateGameState()
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
                screenManager.StandingCamera();
                uiManager.StopUI();
                break;

            case GameState.Sitting:
                uiManager.StartUI();
                break;
            default:
                break;
        }
    }

    //Setter method to set the canSit bool
    public void SetCanSit(bool b)
    {
        canSit = b;
    }
}