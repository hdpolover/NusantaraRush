using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TestTouchInput : MonoBehaviour
{
    public int shipId = 0;
    public string shipType;
    public int hp = 100;
    //public TextMeshProUGUI test;

    public bool terpilih = false;
    public bool isMove = false;

    public GameObject nodeBefore;
    Transform moveTarget;
    public int nodePostion = 0;
    public float speed = 5;
    float step;

    public GameObject scripts;
    public int battleSceneIndex;

    StrategyMove strategyMove;
    StrategyShipInfo strategyShipInfo;
    Nodes nodeScript;
    Sql sqlScript;
    //ShipsPosition shipsPosition;

    void Start(){
        step =  speed * Time.deltaTime;
        strategyMove = GameObject.Find("Scripts").GetComponent<StrategyMove>();
        strategyShipInfo = GameObject.Find("Scripts").GetComponent<StrategyShipInfo>();
        sqlScript = scripts.GetComponent<Sql>();
        //shipsPosition = scripts.GetComponent<ShipsPosition>();
    }

    void Update()
    {
        if (strategyMove.nodeTarget)
        {
            moveTarget = strategyMove.nodeTarget.transform;
        }
        isMove = strategyMove.shipMove;

        MoveShip();
    }

    void OnTouchDown(){
        if (strategyMove.movePoints > 0 && !strategyMove.shipTerpilih)
        {
            if (terpilih)
            {
                terpilih = false;
                strategyMove.shipTerpilih = false;
                strategyMove.shipNodePos = 0;
            }
            else
            {
                terpilih = true;
                strategyMove.shipTerpilih = true;
                strategyMove.shipNodePos = nodePostion;
                strategyShipInfo.setInfo(shipType, hp);
                strategyShipInfo.ship = gameObject;
                strategyShipInfo.isCheck = true;
            }
        }
    }

    void OnMouseDown(){
        if (strategyMove.movePoints > 0 && !strategyMove.shipTerpilih)
        {
            if (terpilih)
            {
                terpilih = false;
                strategyMove.shipTerpilih = false;
                strategyMove.shipNodePos = nodePostion;
            }
            else
            {
                terpilih = true;
                strategyMove.shipTerpilih = true;
                strategyMove.shipNodePos = nodePostion;
                strategyShipInfo.setInfo(shipType, hp);
                strategyShipInfo.ship = gameObject;
                strategyShipInfo.isCheck = true;
            }
        }
    }

    private void MoveShip()
    {
        if (strategyMove.movePoints > 0)
        {
            if (terpilih && isMove)
            {
                if (transform.position != moveTarget.position)
                {
                    //move the ship
                    transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, step);
                }
                else
                {
                    //set no battle value for last node
                    if (strategyMove.nodeTarget)
                    {
                        nodeScript = nodeBefore.GetComponent<Nodes>();
                        nodeScript.isBattle = false;
                    }
                    //set nodeBefore object variable with current node position
                    nodeBefore = strategyMove.nodeTarget;
                    Nodes currentNodeScript = strategyMove.nodeTarget.GetComponent<Nodes>();
                    currentNodeScript.isBattle = true;

                    isMove = false;
                    terpilih = false;
                    strategyMove.shipTerpilih = false;
                    strategyMove.shipMove = false;
                    strategyMove.nodeTarget = null;

                    //update the move points
                    strategyMove.UpdateMovePoints(-1);
                    nodePostion = strategyMove.targetNodePos;
                    strategyShipInfo.cancelChoose();

                    //Set ships position to array
                    //shipsPosition.SaveShipsPosition(nodePostion, shipId);
                    //shipsPosition.Debug();

                    //load battle scene
                    if (strategyMove.targetNodeIsBattle)
                    {
                        //test.SetText("Success");
                        SceneManager.LoadScene(battleSceneIndex);
                    }
                    //save strategy state to sql
                    else
                    {
                        sqlScript.SaveStrategyState(nodePostion, shipId);
                    }
                }
            }
        }
    }
}
