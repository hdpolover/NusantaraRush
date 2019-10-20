using UnityEngine;

public class IslandDamage : MonoBehaviour
{
    PlayerHealth ph;

    private void Start()
    {
        ph = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ph.currentHealth -= 0.1f;
            ph.healthBar.fillAmount = ph.currentHealth / ph.startHealth;
        }
    }
}
