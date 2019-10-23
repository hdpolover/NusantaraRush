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

    public int movePoints = 2;
    private int lastMovePoints = 2;

    [Header("Enemy turn")]
    public bool enemyTurn = false;
    public int enemyIdTurn = 0;

    public Text movePointsText;

    [Header("Info UI")]
    public Button menyerahButton;
    public Button batalButton;
    public Button selesaiButton;

    [Header("End Game UI")]
    public Text goldBonus;
    public Text partBonus;
    public Text ammoBonus;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject confirmWindow;

    public Transform deployNode;
    public GameObject ship;
    public GameObject[] enemy;
    public GameObject[] enemyFirstnode;
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
                // for player instantiate
                GameObject instance = Instantiate(ship, nodes[reader.GetInt32(1)].transform.position, nodes[reader.GetInt32(1)].transform.rotation);
                Destroy(ship);
                StrategyPlayer playerShip = instance.GetComponent<StrategyPlayer>();
                playerShip.nodeBefore = nodes[reader.GetInt32(1)];
                playerShip.scripts = scripts;
                playerShip.shipType = "Ship Type";
                playerShip.shipId = reader.GetInt32(0);
                playerShip.nodePostion = reader.GetInt32(1);
                movePoints++;
                lastMovePoints++;
            }
            else
            {
                // for enemy instantiate
                for (int i = 0; i < enemy.Length; i++)
                {
                    StrategyEnemy strategyEnemy = enemy[i].GetComponent<StrategyEnemy>();
                    if (reader.GetInt32(0) == strategyEnemy.enemyShipId)
                    {
                        GameObject instance = Instantiate(enemy[i], nodes[reader.GetInt32(1)].transform.position, nodes[reader.GetInt32(1)].transform.rotation);
                        enemyFirstnode[i].GetComponent<Nodes>().isBattle = false;
                        enemyFirstnode[i].GetComponent<Nodes>().isEnemy = false;
                        nodes[reader.GetInt32(1)].GetComponent<Nodes>().isBattle = true;
                        nodes[reader.GetInt32(1)].GetComponent<Nodes>().isEnemy = true;
                        Destroy(enemy[i]);
                        StrategyEnemy strategyEnemy2 = instance.GetComponent<StrategyEnemy>();
                        strategyEnemy2.nodeBefore = nodes[reader.GetInt32(1)];
                        strategyEnemy2.enemyShipId = reader.GetInt32(0);
                        strategyEnemy2.positionOnNode = reader.GetInt32(1);
                    }
                }
            }
            selesaiButton.interactable = true;
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
        movePoints = lastMovePoints;
        menyerahButton.interactable = true;
        batalButton.interactable = true;
        selesaiButton.interactable = false;

        if (targetNodeIsEnemyBase)
        {
            int goldBonus_ = Random.Range(150, 350);
            int partBonus_ = Random.Range(80, 280);
            int ammoBonus_ = Random.Range(100, 300);
            goldBonus.text = ""+goldBonus_;
            partBonus.text = ""+partBonus_;
            ammoBonus.text = ""+ammoBonus_;

            string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
            IDbConnection myConnection = new SqliteConnection(path_sqlite);
            myConnection.Open();
            IDbCommand myCommand = myConnection.CreateCommand();

            myCommand.CommandText = "SELECT poin, part, ammo, mission_progress, on_mission FROM player_stat";
            IDataReader reader = myCommand.ExecuteReader();

            int playerGold = 0;
            int playerPart = 0;
            int playerAmmo = 0;
            int playerMissionProgress = 0;
            int playerOnMission = 0;

            if (reader.Read())
            {
                playerGold = reader.GetInt32(0);
                playerPart = reader.GetInt32(1);
                playerAmmo = reader.GetInt32(2);
                playerMissionProgress = reader.GetInt32(3);
                playerOnMission = reader.GetInt32(4);
            }
            reader.Dispose();
            myCommand.Dispose();

            myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UPDATE player_stat SET poin = "+(playerGold + goldBonus_)+", part = " + (playerPart + partBonus_) + ", ammo = " + (playerAmmo + ammoBonus_);
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();

            if (playerMissionProgress < playerOnMission)
            {
                myCommand = myConnection.CreateCommand();
                myCommand.CommandText = "UPDATE player_stat SET mission_progress = "+playerOnMission+" WHERE id = 1";
                myCommand.ExecuteNonQuery();
                PlayerManager.instance.missionProgress = playerMissionProgress;
            }

            myCommand.Dispose();
            myConnection.Close();

            sqlScript.DoneStrategyState();
            winScreen.SetActive(true);
        }
        else
        {
            enemyTurn = true;
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
