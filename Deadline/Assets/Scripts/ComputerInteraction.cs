using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("yes");
            GameManager.Instance.SetCanSit(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("no");

            GameManager.Instance.SetCanSit(false);
        }
    }
}
