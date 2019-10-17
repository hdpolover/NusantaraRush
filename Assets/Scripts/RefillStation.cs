using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RefillStation : MonoBehaviour
{
    BulletHandler bh;
    public GameObject panelIsi;
    public GameObject panelHabis;
    public TextMeshProUGUI isian;

    private Button cannonBtn;

    private void Start()
    {
        bh = GameObject.FindGameObjectWithTag("Player").GetComponent<BulletHandler>();
        cannonBtn = GameObject.Find("Cannon").GetComponent<Button>();

        panelIsi = GameObject.Find("PanelIsi");
        panelIsi.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        panelIsi.SetActive(true);
        panelHabis.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        cannonBtn.interactable = true;

        /*
        if (bh.bulletCount < 100)
        {
            bh.bulletCount += 0.05f;
        } else
        {
            isian.SetText("Amunisi penuh.");
        }
        */
    }
    
    private void OnTriggerExit(Collider other)
    {
        panelIsi.SetActive(false);
    }
}
