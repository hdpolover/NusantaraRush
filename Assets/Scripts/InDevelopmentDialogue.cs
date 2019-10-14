using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InDevelopmentDialogue : MonoBehaviour
{
    public GameObject infoDialogue;

    public void CloseDialogue()
    {
        infoDialogue.SetActive(false);
    }

}
