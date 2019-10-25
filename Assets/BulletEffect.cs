using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    public ParticleSystem waterSplash;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            waterSplash.Play();
            Destroy(gameObject);
        }
    }
}
