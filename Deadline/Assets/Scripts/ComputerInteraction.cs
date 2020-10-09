using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    // Trigger to check if the player is close enough to the computer
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("yes");
            GameManager.Instance.SetCanSit(true);
        }
    }

    // Trigger to check if the player left the computer
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("no");

            GameManager.Instance.SetCanSit(false);
        }
    }
}
