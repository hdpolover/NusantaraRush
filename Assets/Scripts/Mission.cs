using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public GameObject missionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        hideMission();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMission()
    {
        missionsPanel.SetActive(true);
    }

    public void hideMission()
    {
        missionsPanel.SetActive(false);
    }
}
