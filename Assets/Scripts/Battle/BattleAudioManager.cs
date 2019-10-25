using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAudioManager : MonoBehaviour
{ 
    public AudioSource mgSound;
    public AudioSource cannonSound;
    public AudioSource rocketSound;

    public void MgPlay()
    {
        mgSound.Play();
    }

    public void CannonPlay()
    {
        cannonSound.Play();
    }

    public void RocketPlay()
    {
        rocketSound.Play();
    }
    
}
