using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public float startHealth;
    [HideInInspector] public float currentHealth;

    private void Start()
    {
        startHealth = GameObject.Find("LevelManager").GetComponent<LevelManager>().shipStartHealth;
        currentHealth = startHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.fillAmount = currentHealth / startHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MgBullet")
        {
            Destroy(other.gameObject);

            TakeDamage(0.5f);
        }
        else if (other.gameObject.tag == "CannonBullet")
        {
            Destroy(other.gameObject);

            TakeDamage(5f);
        }
        else if (other.gameObject.tag == "RocketBullet")
        {
            Destroy(other.gameObject);

            TakeDamage(10);
        }
    }
}
