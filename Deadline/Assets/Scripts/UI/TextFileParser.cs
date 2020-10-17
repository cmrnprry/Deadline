using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TextFileParser : MonoBehaviour
{
    public TextAsset fullEssayTextFile;
    public string signalBreakCharacter = ">";

    private string _essayText;
    private List<string> _essaySections = new List<string>();


    private void Awake()
    {
        _essayText = fullEssayTextFile.text;

        if (signalBreakCharacter == null)
        {
            signalBreakCharacter = ">";
        }

        ParseText(_essayText, signalBreakCharacter);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*Loops through a text  looking for a specific break key. If found, it adds everything up to the first index of that break key to a list of strings
     * called _essaySections and then updates the string by removing that substring. If the break key is not found, the while loop breaks and it checks if
     * the text still contains a character, as we can assume that this is the last part of the essay and thus there is no lasting break key afterwards,
     * so all of this string is added as the last section in _essaySections.*/

    private void ParseText (string text, string breakKey)
    {
        while(text.Contains(breakKey))
        {
            int index = text.IndexOf(breakKey);
            _essaySections.Add(text.Substring(0, index));
            text = text.Remove(0, index + 1);
        }

        if(text.Length > 0)
        {
            _essaySections.Add(text);
        }
    }

    //Returns the string of the essay section at the given index.
    public string GetEssaySection (int index)
    {
        if(_essaySections[index] != null)
        {
            return _essaySections[index];
        }
        else
        {
            Debug.LogWarning("There is no essay section that exists at index " + index);
            return null;
        }
    }
}
