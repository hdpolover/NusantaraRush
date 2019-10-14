using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyShipInfo : MonoBehaviour
{
    public GameObject shipInfoPanel;
    public Button selesaiButton;
    public Text shipName;
    public Text shipHp;

    public GameObject ship;

    StrategyMove strategyMove;
    TestTouchInput testTouchInput;
    public bool isCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        strategyMove = GetComponent<StrategyMove>();
        shipInfoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ship != null && isCheck)
        {
            testTouchInput = ship.GetComponent<TestTouchInput>();
            isCheck = false;
        }
    }

    public void setInfo(string name, int hp)
    {
        shipName.text = name;
        shipHp.text = hp + "%";
        shipInfoPanel.SetActive(true);
    }

    public void cancelChoose()
    {
        shipInfoPanel.SetActive(false);
        strategyMove.shipTerpilih = false;
        testTouchInput.terpilih = false;
        ship = null;
        isCheck = false;
        selesaiButton.interactable = true;
    }
}
