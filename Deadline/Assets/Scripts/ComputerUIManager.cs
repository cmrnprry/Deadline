using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerUIManager : MonoBehaviour
{
    const int tabCount = 3;
    int[] allTabs = new int[tabCount]; //int array of 0s and 1s that saves if a tabs has been unlocked

    //public int currentTab;
   public GameObject[] computerTabs; //the gameobject array of tabs to be turned on and off

   public GameObject[] tabButtons;
   public GameObject chatWindow;
   public bool chatting;

   private void Start() {
       SelectTab(0);
   }

   ///------LOGIC-------

    //turns all tabs off , turn on target tab
   void SetTabs(int targetTab)
   {
        if(targetTab >= tabCount) return; //if target tab is out of bounds, return

       for(int i = 0; i < tabCount; i++)
       {
           computerTabs[i].SetActive(false); //turn off all the tabs
       }

       computerTabs[targetTab].SetActive(true); //turn on the target tab

   }

   ///------BUTTONS-------
   
   //Task Bar Chat Button
   public void ToggleChatWindow()
   {
       if(chatting)
       {
           chatWindow.SetActive(false);
           chatting = false;
       } 
       else
       {
            chatWindow.SetActive(true);
            chatting = true;
        }
   }

    //Chat Box X button
   public void CloseChatWindow()
   {
       if(chatting)
       {
           chatting = false;
           chatWindow.SetActive(false);
       }
   }

   //Tab Buttons
   public void SelectTab(int targetTab)
   {
      SetTabs(targetTab);
   }
}
