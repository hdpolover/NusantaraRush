using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public GameObject creditScreen;

    // Start is called before the first frame update
    void Start()
    {
        creditScreen.SetActive(false);    
    }

    public void EnableCreditScreen()
    {
        creditScreen.SetActive(true);    
    }

    public void DisableCreditScreen()
    {
        creditScreen.SetActive(false);
    }
}
