using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public GameObject EndGameUI;
    public GameObject pauseButton;
    public TextMeshProUGUI endLabel;
    public TextMeshProUGUI lanjutan;

    private GameObject[] enemies;
    public float enemyTotal;
    private GameObject player;
    

    private void Start()
    {
        EndGameUI.SetActive(false);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyTotal = enemies.Length;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (enemyTotal == 0)
        {
            endLabel.SetText("You Won!");
            Win();
        } else
        {
            /*
            if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
            {
                Destroy(player.gameObject);
                endLabel.SetText("Game Over!");
                GameOver();
            }
            */
        }
    }

    public void GameOver()
    {
        pauseButton.SetActive(false);
        EndGameUI.SetActive(true);
        Time.timeScale = 0f;
        lanjutan.SetText("Menu");
    }

    public void Win()
    {
        pauseButton.SetActive(false);
        EndGameUI.SetActive(true);
        Time.timeScale = 0f;
        lanjutan.SetText("Lanjut");
    }

    public void Lanjutan()
    {
        Time.timeScale = 1f;
    }
}
