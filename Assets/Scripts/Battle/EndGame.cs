using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGame : MonoBehaviour
{
    public GameObject EndGameUI;
    public GameObject pauseButton;

    public TextMeshProUGUI endLabel;
    public TextMeshProUGUI descLabel;
    public Image icon;
    public Sprite happyIcon;
    public Sprite sadIcon;

    [Header("Ship Prefabs")]
    public GameObject[] playerShips;
    private GameObject[] smallEnemies;
    private GameObject[] mediumEnemies;
    private GameObject[] bigEnemies;

    private int enemyTotal;
    public TextMeshProUGUI enemyTotalText;
    private GameObject player;

    PlayerHealth ph;

    private void Start()
    {
        EndGameUI.SetActive(false);
        
        player = GameObject.Find(PlayerManager.instance.playerShipNaame);
        ph = player.GetComponent<PlayerHealth>();
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
            endLabel.SetText("KAMU BERHASIL!");
            descLabel.SetText("Kamu sudah mengalahkan semua kapal musuh. Selamat!");
            icon.sprite = happyIcon;
            Win();
        } else
        {
            if (player != null)
            {
                if (ph.currentHealth <= 0)
                {
                    Destroy(player.gameObject);
                    endLabel.SetText("KAMU GAGAL!");
                    descLabel.SetText("Sepertinya kamu perlu perkuat kapalmu dan atur strategi baru. Tetap semangat!");
                    icon.sprite = sadIcon;
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
        Debug.Log("over");
    }

    public void Win()
    {
        pauseButton.SetActive(false);
        EndGameUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("win");
    }

    public void Lanjutan()
    {
        Time.timeScale = 1f;
    }
}
