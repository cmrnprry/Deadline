using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public SabotageManager sabotageManager;

    //int to check what has been sabotaged / what you are fixing
    private int sabotage = -1;

    [Header("DoorBell Sabotage")]
    public TextMeshProUGUI doorText;
    //public Animator door;

    [Header("TV Sabotage")]
    public TextMeshProUGUI TVText;
    //public Animator door;

    // Triggers to check if the player is closeenough to an object to interact with it
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Computer")
        {
            Debug.Log("Computer");
            GameManager.Instance.SetCanSit(true);
        }
        else if (other.tag == "TV")
        {
            Debug.Log("TV");
            sabotage = 0;
            //Stop Sabotage but allow player to open the door
            TVText.gameObject.SetActive(true);
            StartCoroutine(WaitingForInput());
        }
        else if (other.tag == "DoorBell")
        {
            Debug.Log("DoorBell");
            sabotage = 1;
            //Stop Sabotage but allow player to open the door
            sabotageManager.StopSabatage(SabotageType.DoorBell);
            doorText.gameObject.SetActive(true);
            StartCoroutine(WaitingForInput());
        }

    }

    // Trigger to check if the player left the computer
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Computer")
        {
            Debug.Log("no Computer");

            GameManager.Instance.SetCanSit(false);
        }
        else if (other.tag == "TV")
        {
            sabotage = -1;
            Debug.Log("no TV");
        }
        else if (other.tag == "DoorBell")
        {
            sabotage = -1;
            Debug.Log("no DoorBell");
            Debug.Log("Door Closed");
            //door.SetTrigger("Close");
            //Play Door creak SFX?
            doorText.gameObject.SetActive(false);
        }
    }


    //Waits for specific input
    private IEnumerator WaitingForInput()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("Open Door"));

        Debug.Log("Pressed");

        if (Input.GetButtonDown("Open Door"))
        {
            if (sabotage == 0)
            {
                Debug.Log("TV Off");
                sabotageManager.StopSabatage(SabotageType.TV);
                TVText.gameObject.SetActive(false);
            }
            else if (sabotage == 1)
            {
                Debug.Log("Door Open");
                //door.SetTrigger("Open");
                //Play Door creak SFX?
            }

        }
    }
}
