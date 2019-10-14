using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOver : MonoBehaviour
{
    public GameObject panelOver;
    public GameObject panelSelesai;

    void Start()
    {
        panelOver = GameObject.Find("OverPanel");
        panelSelesai = GameObject.Find("OtherPanels");

        panelOver.SetActive(false);
    }
    
    public void OpenPanel()
    {
        panelOver.SetActive(true);
        panelSelesai.SetActive(false);
    }

    public void ClosePanel()
    {
        panelOver.SetActive(false);
        Time.timeScale = 1f;
    }
}
