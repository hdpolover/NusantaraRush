﻿using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Player Ship Prefabs")]
    public GameObject[] playerShipPrefabs;

    [Header("Enemy Ship Prefabs")]
    public GameObject enemySmallPrefab;
    public GameObject enemyMediumPrefab;
    public GameObject enemyBigPrefab;

    [Header("Player's ship attributes")]
    public float shipStartHealth;
    public float mgDamage;
    public float cannonDamage;
    public float rocketDamage;

    //variables form db
    public int currentMission;

    public int chosenShip;
    public int mgLevel;
    public int cannonLevel;
    public int rocketLevel;

    [Header("Spawn Points")]
    public Transform[] enemySpawnPoints;
    
    private void Start()
    {
        currentMission = PlayerManager.instance.missionProgress;
        chosenShip = PlayerManager.instance.chosen_ship;

        mgLevel = PlayerManager.instance.mg_level;
        cannonLevel = PlayerManager.instance.cannon_level;
        rocketLevel = PlayerManager.instance.rocket_level;

        //playerShipPrefabs[0].SetActive(false);
        //playerShipPrefabs[1].SetActive(false);
        //playerShipPrefabs[2].SetActive(false);
        //playerShipPrefabs[3].SetActive(false);
        //playerShipPrefabs[4].SetActive(false);

        SetPlayerShip();
        SetShipDamage();
        SetEnemies();
    }

    void SetPlayerShip()
    {
        switch (chosenShip)
        {
            case 0:
                playerShipPrefabs[0].SetActive(true);
                break;
            case 1:
                playerShipPrefabs[1].SetActive(true);
                break;
            case 2:
                playerShipPrefabs[2].SetActive(true);
                break;
            case 3:
                playerShipPrefabs[3].SetActive(true);
                break;
            case 4:
                playerShipPrefabs[4].SetActive(true);
                break;
        }
    }

    void SetShipDamage()
    {
        SetMgDamageBasedOnLevel(mgLevel);
        SetCannonDamageBasedOnLevel(cannonLevel);
        SetRocketDamageBasedOnLevel(rocketLevel);
    }

    void SetMgDamageBasedOnLevel(int level)
    {
        switch (level)
        {
            case 1:
                mgDamage = 1f;
                break;
            case 2:
                mgDamage = 1.5f;
                break;
            case 3:
                mgDamage = 2f;
                break;
            case 4:
                mgDamage = 2.5f;
                break;
            case 5:
                mgDamage = 3f;
                break;
            default:
                mgDamage = 0;
                break;
        }
    }

    void SetCannonDamageBasedOnLevel(int level)
    {
        switch (level)
        {
            case 1:
                cannonDamage = 5f;
                break;
            case 2:
                cannonDamage = 8f;
                break;
            case 3:
                cannonDamage = 11f;
                break;
            case 4:
                cannonDamage = 14f;
                break;
            case 5:
                cannonDamage = 17f;
                break;
            default:
                cannonDamage = 0;
                break;
        }
    }

    void SetRocketDamageBasedOnLevel(int level)
    {
        switch (level)
        {
            case 1:
                rocketDamage = 10f;
                break;
            case 2:
                rocketDamage = 13f;
                break;
            case 3:
                rocketDamage = 16f;
                break;
            case 4:
                rocketDamage = 19f;
                break;
            case 5:
                rocketDamage = 21f;
                break;
            default:
                rocketDamage = 0;
                break;
        }
    }


    void SetEnemies()
    {
        switch (currentMission)
        {
            case 1:
                Instantiate(enemySmallPrefab, enemySpawnPoints[0].position, Quaternion.identity);
                Instantiate(enemySmallPrefab, enemySpawnPoints[1].position, Quaternion.identity);
                break;
            case 2:
                Instantiate(enemySmallPrefab, enemySpawnPoints[1].position, Quaternion.identity);
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            default:
                Instantiate(enemySmallPrefab, enemySpawnPoints[0].position, Quaternion.identity);
                break;

        }
    }
}
