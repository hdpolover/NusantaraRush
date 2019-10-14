using System.Collections;
using System.Collections.Generic;
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

    readonly FireHandler fr;
    public GameObject cratePrefab;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentHealth = startHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.fillAmount = currentHealth / startHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);

            GameObject crate = Instantiate(cratePrefab, new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y + 1.5f, gameObject.transform.position.z), Quaternion.identity) as GameObject;
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
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            gameObject.GetComponent<EnemyHealth>().TakeDamage(10);
        }
    }
}
