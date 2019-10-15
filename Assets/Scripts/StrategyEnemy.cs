using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StrategyEnemy : MonoBehaviour
{
    public GameObject[] nodes;
    public int[] AllowedMoveTo = { 0 };
    public float speed = 10;
    float step;

    public GameObject nodeBefore;
    public int positionOnNode = 4;
    public int enemyShipId;

    public Button selesaiButton;

    public GameObject loseScreen;

    public bool moveState = false;
    public bool checkPositionOnNode_State = false;

    public GameObject node;
    public GameObject scripts;
    public int battleSceneIndex;
    //public TextMeshProUGUI test;

    StrategyMove strategyMove;

    Nodes nodeScript;
    Sql sqlScript;

    // Start is called before the first frame update
    void Start()
    {
        step = speed * Time.deltaTime;
        strategyMove = scripts.GetComponent<StrategyMove>();
        sqlScript = scripts.GetComponent<Sql>();
    }

    // Update is called once per frame
    void Update()
    {
        Check();
        Move();
    }

    private void Check()
    {
        if (strategyMove.enemyTurn && !moveState)
        {
            node = null;
            if (node == null)
            {
                node = nodes[Random.Range(0, nodes.Length)];
                nodeScript = node.GetComponent<Nodes>();
                AllowedMoveTo = nodeScript.allowedMoveFrom;
                checkPositionOnNode_State = true;
            }
            //else
            //{
            if (checkPositionOnNode_State)
            {
                for (int i = 0; i < AllowedMoveTo.Length; i++)
                {
                    if (positionOnNode == AllowedMoveTo[i])
                    {
                        moveState = true;
                        nodeScript = node.GetComponent<Nodes>();
                        positionOnNode = nodeScript.nodePos;
                        //nodeBefore = node;
                    }

                    if (i == AllowedMoveTo.Length - 1 && positionOnNode != AllowedMoveTo[i])
                    {
                        checkPositionOnNode_State = false;
                    }
                }
            }
            //}
        }
    }

    private void Move()
    {
        if (moveState)
        {
            if (node != null)
            {
                if (transform.position != node.transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, node.transform.position, step);
                }
                else
                {
                    //load battle scene
                    if (nodeScript.isBattle)
                    {
                        SceneManager.LoadScene(battleSceneIndex);
                    }
                    else
                    {
                        //save strategy state to sql
                        sqlScript.SaveStrategyState(positionOnNode, enemyShipId);
                    }

                    if (nodeScript.isPlayerBase)
                    {
                        sqlScript.DoneStrategyState();
                        loseScreen.SetActive(true);
                    }

                    //set no battle value for last node
                    if (node)
                    {
                        nodeScript = nodeBefore.GetComponent<Nodes>();
                        nodeScript.isBattle = false;
                    }

                    //set nodeBefore object variable with current node position
                    nodeBefore = node;
                    Nodes currentNodeScript = node.GetComponent<Nodes>();
                    currentNodeScript.isBattle = true;

                    node = null;
                    moveState = false;
                    strategyMove.enemyTurn = false;
                    selesaiButton.interactable = true;
                }
            }
        }
    }
}