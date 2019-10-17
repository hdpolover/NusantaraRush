using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClick1;
    public AudioClip buttonClick2;

    public void ButtonClick1()
    {
        audioSource.PlayOneShot(buttonClick1);
    }

    public void ButtonClick2()
    {
        audioSource.PlayOneShot(buttonClick2);
    }
}
