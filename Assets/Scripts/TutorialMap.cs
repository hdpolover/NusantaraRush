using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMap : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject scripts;
    public GameObject firstTutorial;

    Dialogue dialogueScript;
    Tutorial tutorialScript;

    int lastDialogue = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogueScript = dialogue.GetComponent<Dialogue>();
        tutorialScript = scripts.GetComponent<Tutorial>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkTutorial()
    {
        lastDialogue++;

        if (lastDialogue == 0)
        {
            tutorialScript.CheckTutorial();
        }
        else if (lastDialogue == dialogueScript.dialogueTurn.Length)
        {
            tutorialScript.NextTutorial();
            tutorialScript.CheckTutorial();
        }
        else
        {
            firstTutorial.SetActive(false);
        }
    }
}
