using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class InkManager : MonoBehaviour
{
    [Header("Ink Variables")]
    public TextAsset inkJSON;
    public Story story;
    private List<string> tags = new List<string>();

    [Header("Chat UI")]
    public int speakerTagSize;
    public int textSize;
    public Scrollbar scrollbar;

    [Header("UI Variables")]
    public List<Button> buttons;
    public TextMeshProUGUI text;

    [Header("Keeping Track of Convos")]
    private bool waitingForNext; //bool to check if the game is waiting to show the next convo
    private float time; // time in seconds until the next convo is triggered

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);
        StartCoroutine(InkLoop());
    }

    IEnumerator InkLoop()
    {
        if (story.canContinue)
        {
            string line = story.Continue();
            line.Trim();
            tags = story.currentTags;

            //basically if the friends are typing
            if (tags.Contains("Speaker_One") || tags.Contains("Speaker_Two") || tags.Contains("Speaker_Three"))
            {
                text.text += "<align=left><size=" + speakerTagSize + ">" + tags[0] + "\n";
                text.text += "<align=left><size=" + textSize + ">" + line + "\n";

                Canvas.ForceUpdateCanvases();
                scrollbar.value = 0;
            }
            //Otherwise show the player response
            else
            {
                text.text += "<align=right><size=" + speakerTagSize + "> Player\n";
                text.text += "<align=right><size=" + textSize + ">" + line + "\n";

                Canvas.ForceUpdateCanvases();
                scrollbar.value = 0;
            }

            //If the Tag "End" is seen, the next tag (WHICH SHOULD BE THE LAST ONE) will be how long to wait before starting a new conversation
            //So then we'll wait and then restart the ink loop
            if (tags.Contains("End"))
            {
                waitingForNext = true;
                time = float.Parse(tags[tags.Count - 1]);

                yield return new WaitForSecondsRealtime(time);
                WaitForNextConvo();

                yield break;
            }
        }
        else
        {
            Debug.Log("End of Text");
            yield break;
        }

        //If there are choices
        if (story.currentChoices.Count > 0)
        {
            yield return new WaitForSecondsRealtime(1f);

            DisplayChoices();
            yield break;
        }

        //Show a typing anim? Or something?
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(InkLoop());
    }

    void WaitForNextConvo()
    {
        Debug.Log("Next Convo");
        waitingForNext = false;
        StartCoroutine(InkLoop());
    }

    public void StopCoroutine()
    {
        StopAllCoroutines();
    }

    public void RestartCoroutine()
    {
        if (waitingForNext)
        {
            Invoke("WaitForNextConvo", time);
        }
        else
        {
            StartCoroutine(InkLoop());
        }

    }

    // Display all the choices, if there are any!
    void DisplayChoices()
    {

        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            Choice choice = story.currentChoices[i];
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = choice.text.Trim();
            buttons[i].gameObject.SetActive(true);

            // Tell the button what to do when we press it
            buttons[i].onClick.AddListener(delegate
            {
                OnClickChoiceButton(choice);
            });
        }

    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        TurnOffButtons();
        StartCoroutine(InkLoop());
    }

    //Set all teh choice buttons to be false
    void TurnOffButtons()
    {
        foreach (Button b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }
}
