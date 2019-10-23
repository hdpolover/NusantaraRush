using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RefillStation : MonoBehaviour
{
    BulletHandler bh;
    PlayerHealth ph;

    private GameObject player;
    public GameObject PanelRefill;
    public TextMeshProUGUI textIsi;

    public Button mgBtn;
    public Button cannonBtn;
    public Button rocketBtn;

    private void Start()
    {
        player = GameObject.Find(PlayerManager.instance.playerShipNaame);
        bh = player.GetComponent<BulletHandler>();
        ph = player.GetComponent<PlayerHealth>();

        PanelRefill.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            PanelRefill.SetActive(true);
            Debug.Log("Masuk");
        } else
        {
            Debug.Log("Lain");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (ph.currentHealth < ph.startHealth)
            {
                ph.currentHealth += 0.05f;
                ph.healthBar.fillAmount = ph.currentHealth / ph.startHealth;
            }

            if (mgBtn.interactable == true)
            {
                if (bh.mgBulletCount < bh.maxMgBulletCount)
                {
                    bh.mgBulletCount += 0.1f;
                }
            }

            if (cannonBtn.interactable == true)
            {
                if (bh.cannonBulletCount < bh.maxCannonBulletCount)
                {
                    bh.cannonBulletCount += 0.05f;
                }
            }

            if (rocketBtn.interactable == true)
            {
                while (bh.rocketBulletCount < bh.maxRocketBulletCount)
                {
                    bh.rocketBulletCount += 0.01f;
                }
            }

            textIsi.SetText("Sedang mengisi...");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        PanelRefill.SetActive(false);
    }
}
