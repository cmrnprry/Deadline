using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EssayManager : MonoBehaviour
{
    public TextMeshProUGUI essayTMPro;
    public int charactersPerKeystroke;

    private GameManager _gm;
    private TextFileParser _tfp;

    private string _currentSection;
    private List<string> _subsections = new List<string>();

    private int _essaySectionIndex = 0;
    private int _intraSectionProgressIndex = 0;

    private bool _canYouWriteMore = true;

    // Start is called before the first frame update
    void Start()
    {
        //_gm = FindObjectOfType<GameManager>();
        _tfp = FindObjectOfType<TextFileParser>();

        _currentSection = _tfp.GetEssaySection(0);

        if(_currentSection != null)
        {
            TruncateSection(_currentSection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*  if (_gm.isScreenOne && _canYouWriteMore)
          {
              if (CheckForInput())
              {
                  Type();
              }
          }*/

        if (_canYouWriteMore)
        {
            if (CheckForInput())
            {
                Type();
            }
        }
    }

    /*Clears the subsections list. Checks to see if the given section is evenly divided by the desired number of characters per keystroke and stores
     the total amount of subsections that can be made from this section accordingly. Then, it loops through the section, adding the number of subsections to
    the list.*/
    private void TruncateSection(string section)
    {
        _subsections.Clear();

        int length = section.Length;
        int subsections;

        if(length % charactersPerKeystroke == 0)
        {
            subsections = length / charactersPerKeystroke;
        }
        else
        {
            subsections = (length / charactersPerKeystroke) + 1;
        }
        
        for(int i = 0; i < subsections; i++)
        {
            if(i != subsections - 1)
            {
                _subsections.Add(section.Substring(0, charactersPerKeystroke));
                section = section.Remove(0, charactersPerKeystroke);
            }
            else
            {
                _subsections.Add(section);
            }
        }
    }

    //Checks to see if the player inputs a key input that isn't hotkeyed elsewhere.
    private bool CheckForInput()
    {
        if(Input.anyKeyDown && (!Input.GetButtonDown("Standing") ||
                                !Input.GetButtonDown("Switch Screens") ||
                                !Input.GetButtonDown("Pause") ||
                                !Input.GetMouseButtonDown(0) ||
                                !Input.GetMouseButtonDown(1)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Adds the next subsection to the essay TMPro. If that's the last subsection, it sets _canYouWriteMore to false
    //and resets the _intraSectionProgressIndex to zero.
    private void Type()
    {
        essayTMPro.text += _subsections[_intraSectionProgressIndex];
        _intraSectionProgressIndex++;
        Debug.Log(_intraSectionProgressIndex);

        if(_intraSectionProgressIndex >= _subsections.Count)
        {
            _canYouWriteMore = false;
            _intraSectionProgressIndex = 0;
        }
    }
}
