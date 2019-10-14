using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public bool isBattle = false;
    public int nodePos;
    public int[] allowedMoveFrom;
    StrategyMove strategyMove;
    // Start is called before the first frame update
    void Start()
    {
        strategyMove = GameObject.Find("Scripts").GetComponent<StrategyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTouchDown(){
        CheckShipPosition();
    }

    void OnMouseDown(){
        CheckShipPosition();
    }

    private void CheckShipPosition()
    {
        if (!strategyMove.shipMove)
        {
            for (int i = 0; i < allowedMoveFrom.Length; i++)
            {
                if (strategyMove.shipNodePos == allowedMoveFrom[i])
                {
                    if (strategyMove.movePoints > 0)
                    {
                        if (strategyMove.shipTerpilih)
                        {
                            strategyMove.shipMove = true;
                            strategyMove.nodeTarget = gameObject;
                            strategyMove.targetNodePos = nodePos;
                            strategyMove.targetNodeIsBattle = isBattle;
                        }
                        else
                        {
                            strategyMove.shipMove = false;
                            strategyMove.nodeTarget = null;
                            strategyMove.targetNodePos = 0;
                        }
                    }
                }
            }
        }
    }
}
