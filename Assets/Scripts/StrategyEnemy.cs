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

    bool nodeUpdate = true;

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
                if (!nodeScript.isEnemy)
                {
                    AllowedMoveTo = nodeScript.allowedMoveFrom;
                    checkPositionOnNode_State = true;
                }
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
                        nodeUpdate = true;
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
                //if (strategyMove.enemyIdTurn == enemyShipId)
                //{

                if (transform.position != node.transform.position)
                {
                    //move enemy
                    transform.position = Vector3.MoveTowards(transform.position, node.transform.position, step);

                    if (nodeUpdate)
                    {
                        //save strategy state to sql
                        sqlScript.SaveStrategyState(positionOnNode, enemyShipId);

                        //set no battle value for last node
                        if (node)
                        {
                            Nodes nodeScript2 = nodeBefore.GetComponent<Nodes>();
                            nodeScript2.isBattle = false;
                            nodeScript2.isEnemy   = false;;
                        }
                        //Debug.Log(enemyShipId  + " saat jalan -> "+nodeScript.nodePos);
                        nodeUpdate = false;
                    }    

                    selesaiButton.interactable = false;
                }
                else
                {
                    //Debug.Log(enemyShipId + " saat sampai -> " + node.GetComponent<Nodes>().nodePos);
                    //Debug.Log(node.GetComponent<Nodes>().isBattle && !node.GetComponent<Nodes>().isEnemy);
                    //load battle scene
                    if (node.GetComponent<Nodes>().isBattle && !node.GetComponent<Nodes>().isEnemy)
                    {
                        SceneManager.LoadScene(battleSceneIndex);
                    }
                    else
                    {
                        //set nodeBefore object variable with current node position
                        nodeBefore = node;
                        Nodes currentNodeScript = node.GetComponent<Nodes>();
                        currentNodeScript.isEnemy = true;
                        currentNodeScript.isBattle = true;
                    }


                    if (nodeScript.isPlayerBase)
                    {
                        sqlScript.DoneStrategyState();
                        loseScreen.SetActive(true);
                    }

                    node = null;
                    moveState = false;
                    strategyMove.enemyTurn = false;
                    selesaiButton.interactable = true;
                }
                //}
            }
        }
    }
}