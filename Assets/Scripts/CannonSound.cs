using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CannonSound : MonoBehaviour
{
    public AudioSource cannonSound;

    public void Play()
    {
        cannonSound.Play();
    }
}
