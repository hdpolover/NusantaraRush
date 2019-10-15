using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Mono.Data.Sqlite;
using System.Data;

public class StrategyMove : MonoBehaviour
{
    public bool shipTerpilih;
    public bool shipMove;

    public int shipNodePos;
    public GameObject firstNode;
    public int targetNodePos;
    public GameObject[] nodes;
    public GameObject nodeTarget;
    public bool targetNodeIsBattle;
    public bool targetNodeIsEnemyBase;
    public bool targetNodeIsPlayerBase;

    public int movePoints = 1;
    private int lastMovePoints = 1;
    public bool enemyTurn = false;

    public Text movePointsText;

    public Button menyerahButton;
    public Button batalButton;
    public Button selesaiButton;

    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject confirmWindow;

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
    Sql sqlScript;
    void Start() {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        selesaiButton.interactable = false;
        sqlScript = scripts.GetComponent<Sql>();

        confirmWindow.SetActive(false);
        sqlScript.CheckStrategyState(confirmWindow);
    }

    void Update()
    {
        movePointsText.text = "Poin Aksi : "+movePoints;
        if (shipTerpilih)
        {
            selesaiButton.interactable = false;
        }

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

    public void LoadStrategyState()
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        myCommand.CommandText = "SELECT * FROM mission_ships";
        IDataReader reader = myCommand.ExecuteReader();

        while (reader.Read())
        {
            if (reader.GetInt32(0) < 50)
            {
                GameObject instance = Instantiate(ship, nodes[reader.GetInt32(1)].transform.position, nodes[reader.GetInt32(1)].transform.rotation);
                TestTouchInput playerShip = instance.GetComponent<TestTouchInput>();
                playerShip.nodeBefore = nodes[reader.GetInt32(1)];
                playerShip.scripts = scripts;
                playerShip.shipType = "Main Boat 1";
                playerShip.shipId = reader.GetInt32(0);
                playerShip.nodePostion = reader.GetInt32(1);
                movePoints++;
                lastMovePoints++;
            }
            else
            {
                // for enemy instantiate
            }
        }

        reader.Close();
        myCommand.Dispose();
        myConnection.Close();

        confirmWindow.SetActive(false);
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

        if (targetNodeIsEnemyBase)
        {
            sqlScript.DoneStrategyState();
            winScreen.SetActive(true);
        }
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
            playerShip.shipId = shipButtonId;
            ship1.interactable = false;
            shipLeft--;
            movePoints--;
            lastMovePoints++;
            sqlScript.SaveStrategyState(0, shipButtonId);
        }
        else if (shipButtonId == 2)
        {
            playerShip.shipType = "Main Boat 2";
            playerShip.shipId = shipButtonId;
            ship2.interactable = false;
            shipLeft--;
            movePoints--;
            lastMovePoints++;
            sqlScript.SaveStrategyState(0, shipButtonId);
        }
        else if (shipButtonId == 3)
        {
            playerShip.shipType = "Warship 1";
            playerShip.shipId = shipButtonId;
            ship3.interactable = false;
            shipLeft--;
            movePoints--;
            lastMovePoints++;
            sqlScript.SaveStrategyState(0, shipButtonId);
        }
        else if (shipButtonId == 4)
        {
            playerShip.shipType = "Warship 2";
            playerShip.shipId = shipButtonId;
            ship4.interactable = false;
            shipLeft--;
            movePoints--;
            lastMovePoints++;
            sqlScript.SaveStrategyState(0, shipButtonId);
        }
    }
}
