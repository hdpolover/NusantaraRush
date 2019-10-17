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

    FireHandler fr;
    public GameObject cratePrefab;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        fr = player.GetComponent<FireHandler>();
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
            gameObject.GetComponent<EnemyHealth>().TakeDamage(playerDamage);
            player.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
        } else if (collision.gameObject.tag == "Island")
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MgBullet")
        {
            Destroy(other.gameObject);

            gameObject.GetComponent<EnemyHealth>().TakeDamage(fr.mgDamage);
        } else if (other.gameObject.tag == "CannonBullet")
        {
            Destroy(other.gameObject);

            gameObject.GetComponent<EnemyHealth>().TakeDamage(fr.cannonDamage);
        } else if (other.gameObject.tag == "RocketBullet")
        {
            Destroy(other.gameObject);

            gameObject.GetComponent<EnemyHealth>().TakeDamage(fr.rocketDamage);
        }
    }
}
