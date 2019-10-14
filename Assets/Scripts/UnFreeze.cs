using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnFreeze : MonoBehaviour
{
    public void Awake()
    {
        Time.timeScale = 1f;
    }
}
