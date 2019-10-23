using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthBar;
    public float startHealth = 100;
    public float playerDamage = 50;
    public float enemyDamage = 10;

    private float currentHealth;
    private GameObject player;

    LevelManager lm;
    public GameObject cratePrefab;

    private void Start()
    {
        player = GameObject.Find("PlayerBoat1");
        lm = player.GetComponent<LevelManager>();
        currentHealth = startHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.fillAmount = currentHealth / startHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);

            Instantiate(cratePrefab, gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TakeDamage(playerDamage);
            player.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
        } else if (collision.gameObject.tag == "Island")
        {
            TakeDamage(3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MgBullet")
        {
            Destroy(other.gameObject);

            TakeDamage(10f);
        } else if (other.gameObject.tag == "CannonBullet")
        {
            Destroy(other.gameObject);

            TakeDamage(lm.cannonDamage);
        } else if (other.gameObject.tag == "RocketBullet")
        {
            Destroy(other.gameObject);

            TakeDamage(lm.rocketDamage);
        }
    }
}
