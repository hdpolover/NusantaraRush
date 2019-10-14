using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployNode : MonoBehaviour
{
    public GameObject chooseShipPanel;
    public GameObject informationPanel;
    StrategyMove strategyMove;
    // Start is called before the first frame update
    void Start()
    {
        chooseShipPanel.SetActive(false);
        strategyMove = GameObject.Find("Scripts").GetComponent<StrategyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTouchDown()
    {
        if (strategyMove.movePoints != 0)
        {
            if (strategyMove.shipLeft != 0)
            {
                chooseShipPanel.SetActive(true);
                informationPanel.SetActive(false);
            }
        }
    }

    void OnMouseDown()
    {
        if (strategyMove.movePoints != 0)
        {
            if (strategyMove.shipLeft != 0)
            {
                chooseShipPanel.SetActive(true);
                informationPanel.SetActive(false);
            }
        }
    }
}
