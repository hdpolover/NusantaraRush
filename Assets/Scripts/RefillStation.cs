using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RefillStation : MonoBehaviour
{
    BulletHandler bh;
    PlayerHealth ph;

    public GameObject PanelRefill;
    public TextMeshProUGUI textIsi;

    public Button mgBtn;
    public Button cannonBtn;
    public Button rocketBtn;

    private void Start()
    {
        bh = GameObject.FindGameObjectWithTag("Player").GetComponent<BulletHandler>();
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        PanelRefill.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        PanelRefill.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (ph.currentHealth < ph.startHealth)
            {
                ph.currentHealth += 0.05f;
                ph.healthBar.fillAmount = ph.currentHealth / ph.startHealth;
            }

            if (mgBtn.interactable == true)
            {
                while (bh.mgBulletCount < bh.maxMgBulletCount)
                {
                    bh.mgBulletCount += 1;
                }
            }

            if (cannonBtn.interactable == true)
            {
                while (bh.cannonBulletCount < bh.maxCannonBulletCount)
                {
                    bh.cannonBulletCount += 1;
                }
            }

            if (rocketBtn.interactable == true)
            {
                while (bh.rocketBulletCount < bh.maxRocketBulletCount)
                {
                    bh.rocketBulletCount += 1;
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
