using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public GameObject EndGameUI;
    public GameObject pauseButton;
    public TextMeshProUGUI endLabel;

    private GameObject[] smallEnemies;
    private GameObject[] mediumEnemies;
    private GameObject[] bigEnemies;

    private int enemyTotal;
    public TextMeshProUGUI enemyTotalText;
    private GameObject player;
    

    private void Start()
    {
        EndGameUI.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    int getEnemyTotal()
    {
        smallEnemies = GameObject.FindGameObjectsWithTag("EnemySmall");
        mediumEnemies = GameObject.FindGameObjectsWithTag("EnemyMedium");
        bigEnemies = GameObject.FindGameObjectsWithTag("EnemyBig");

        enemyTotal = smallEnemies.Length + mediumEnemies.Length + bigEnemies.Length;
        return enemyTotal;
    }

    private void Update()
    {
        getEnemyTotal();
        enemyTotalText.SetText(enemyTotal + "");

        if (enemyTotal == 0)
        {
            endLabel.SetText("Kamu berhasil!");
            Win();
        } else
        {
            if (player != null)
            {
                if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
                {
                    Destroy(player.gameObject);
                    endLabel.SetText("Kamu gagal!");
                    GameOver();
                }
            }
        }
    }

    public void GameOver()
    {
        pauseButton.SetActive(false);
        EndGameUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        pauseButton.SetActive(false);
        EndGameUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Lanjutan()
    {
        Time.timeScale = 1f;
    }
}
