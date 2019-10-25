using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RefillStation : MonoBehaviour
{
    BulletHandler bh;
    PlayerHealth ph;
    FireHandler fh;

    public AudioSource stationSound;
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
        fh = player.GetComponent<FireHandler>();

        mgBtn = fh.mgBtn;
        cannonBtn = fh.cannonBtn;
        rocketBtn = fh.rocketBtn;

        PanelRefill.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        StationPlay();
        if (other.gameObject.tag == player.tag)
        {
            PanelRefill.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == player.tag)
        {
            if (ph.currentHealth < ph.startHealth)
            {
                ph.currentHealth += 0.05f;
                ph.healthBar.fillAmount = ph.currentHealth / ph.startHealth;
            }

            if (fh.hasMg)
            {
                if (bh.mgBulletCount < bh.maxMgBulletCount)
                {
                    bh.mgBulletCount += 0.1f;
                    mgBtn.interactable = true;
                }
            }

            if (fh.hasCannon)
            {
                if (bh.cannonBulletCount < bh.maxCannonBulletCount)
                {
                    bh.cannonBulletCount += 0.05f;
                    cannonBtn.interactable = true;
                }
            }

            if (fh.hasRocket)
            {
                while (bh.rocketBulletCount < bh.maxRocketBulletCount)
                {
                    bh.rocketBulletCount += 0.01f;
                    rocketBtn.interactable = true;
                }
            }

            textIsi.SetText("Sedang mengisi...");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        PanelRefill.SetActive(false);
    }

    public void StationPlay()
    {
        stationSound.Play();
    }
}
