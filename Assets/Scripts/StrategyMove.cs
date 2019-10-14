using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyMove : MonoBehaviour
{
    public bool shipTerpilih;
    public bool shipMove;

    public int shipNodePos;
    public GameObject firstNode;
    public int targetNodePos;
    public GameObject nodeTarget;
    public bool targetNodeIsBattle;

    public int movePoints = 1;
    private int lastMovePoints = 1;
    public bool enemyTurn = false;

    public Text movePointsText;

    public Button menyerahButton;
    public Button batalButton;
    public Button selesaiButton;

    public Transform deployNode;
    public GameObject ship;
    public GameObject chooseShipPanel;
    public GameObject informationPanel;
    public Button ship1;
    public Button ship2;
    public Button ship3;
    public Button ship4;
    public int shipLeft = 4;

    public GameObject scripts;
    void Start() {
        selesaiButton.interactable = false;
    }

    void Update()
    {
        movePointsText.text = "Poin Aksi : "+movePoints;
        if (shipTerpilih)
        {
            selesaiButton.interactable = false;
        }

        // - banyak proses, lag
        // todo : buat bool check
        if (shipMove)
        {
            menyerahButton.interactable = false;
            batalButton.interactable = false;
        }
        else
        {
            menyerahButton.interactable = true;
            batalButton.interactable = true;
        }
        //shipNodePos = targetNodePos;
    }

    public void UpdateMovePoints(int quantity)
    {
        movePoints += quantity;
    }

    public void EndTurn()
    {
        enemyTurn = true;
        movePoints = lastMovePoints;
        menyerahButton.interactable = true;
        batalButton.interactable = true;
        selesaiButton.interactable = false;
    }

    public void DeployShip(int shipButtonId)
    {
        GameObject instance = Instantiate(ship, deployNode.position, deployNode.rotation);
        TestTouchInput playerShip = instance.GetComponent<TestTouchInput>();
        playerShip.nodeBefore = firstNode;
        playerShip.scripts = scripts;
        chooseShipPanel.SetActive(false);
        informationPanel.SetActive(true);
        selesaiButton.interactable = true;
        if (shipButtonId == 1)
        {
            playerShip.shipType = "Main Boat 1";
            ship1.interactable = false;
            shipLeft--;
            movePoints--;
            lastMovePoints++;
        }
        else if (shipButtonId == 2)
        {
            playerShip.shipType = "Main Boat 2";
            ship2.interactable = false;
            shipLeft--;
            movePoints--;
            lastMovePoints++;
        }
        else if (shipButtonId == 3)
        {
            playerShip.shipType = "Warship 1";
            ship3.interactable = false;
            shipLeft--;
            movePoints--;
            lastMovePoints++;
        }
        else if (shipButtonId == 4)
        {
            playerShip.shipType = "Warship 2";
            ship4.interactable = false;
            shipLeft--;
            movePoints--;
            lastMovePoints++;
        }
    }
}
