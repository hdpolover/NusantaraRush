using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;

    public int[] dialogueTurn;
    // 0 - monologue
    // 1 - just lussie
    // 2 - just player
    // 3 - lussie turn
    // 4 - player turn

    public string[] texts;
    public GameObject lusie;
    public GameObject player;
    public GameObject nameTurn;
    public Text dialogueText;
    int currentDialogue = 0;

    void Start()
    {
        showDialogue();
    }

    void Update(){}

    public void nextDialogue()
    {
        showDialogue();
    }

    void showDialogue()
    {
        if (dialogueTurn.Length == currentDialogue)
        {
            dialoguePanel.SetActive(false);
        }
        else
        {
            if (dialogueTurn[currentDialogue] == 0)
            {
                setProperties(false, false, new Color32(255, 255, 225, 255), new Color32(255, 255, 225, 255), "", texts[currentDialogue]);
            }
            else if (dialogueTurn[currentDialogue] == 1)
            {
                setProperties(true, false, new Color32(255, 255, 225, 255), new Color32(255, 255, 225, 255), "Lussie", texts[currentDialogue]);
            }
            else if (dialogueTurn[currentDialogue] == 2)
            {
                setProperties(false, true, new Color32(255, 255, 225, 255), new Color32(255, 255, 225, 255), "Yanna", texts[currentDialogue]);
            }
            else if (dialogueTurn[currentDialogue] == 3)
            {
                setProperties(true, true, new Color32(255, 255, 225, 255), new Color32(75, 75, 75, 255), "Lussie", texts[currentDialogue]);
            }
            else if (dialogueTurn[currentDialogue] == 4)
            {
                setProperties(true, true, new Color32(75, 75, 75, 255), new Color32(255, 255, 225, 255), "Yanna", texts[currentDialogue]);
            }
        }
    }

    void setProperties(bool lusieActive, bool playerActive, Color32 lusieColor, Color32 playerColor, string name, string text)
    {
        lusie.SetActive(lusieActive);
        player.SetActive(playerActive);
        lusie.GetComponent<Image>().color = lusieColor;
        player.GetComponent<Image>().color = playerColor;
        nameTurn.GetComponent<TMPro.TextMeshProUGUI>().text = name;
        dialogueText.text = text;
        currentDialogue++;
    }
}
