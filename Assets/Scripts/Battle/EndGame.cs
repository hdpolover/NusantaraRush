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

    private GameObject[] smallEnemies;
    private GameObject[] mediumEnemies;
    private GameObject[] bigEnemies;

    private int enemyTotal;
    public TextMeshProUGUI enemyTotalText;
    private GameObject player;
    

    private void Start()
    {
        EndGameUI.SetActive(false);

        StartCoroutine(Wait1Sec());
    }

    IEnumerator Wait1Sec()
    {
        yield return new WaitForSeconds(1f);
        player = GameObject.FindWithTag("Player");
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
                if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
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
