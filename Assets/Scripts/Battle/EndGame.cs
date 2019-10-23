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

        // StartCoroutine(GetPlayerHealth());
        SetPlayerShip();
        ph = player.GetComponent<PlayerHealth>();
        if (ph == null)
        {
            Debug.Log("Halo");
        } else
        {
            Debug.Log("po");
        }
    }

    void SetPlayerShip()
    {
        switch (PlayerManager.instance.chosen_ship)
        {
            case 0:
                player = playerShips[0];
                break;
            case 1:
                player = playerShips[1];
                break;
            case 2:
                player = playerShips[2];
                break;
            case 3:
                player = playerShips[3];
                break;
            case 4:
                player = playerShips[4];
                break;
        }
    }
    /*
    IEnumerator GetPlayerHealth()
    {
        yield return new WaitForSeconds(2f);
        player = GameObject.FindGameObjectWithTag("Player");
        ph = player.GetComponent<PlayerHealth>();
        Debug.Log("Done");
        Debug.Log("ini: " + ph.currentHealth);
    }
    */

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
