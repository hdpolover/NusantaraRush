using System.Collections;
using UnityEngine;

public class IslandDamage : MonoBehaviour
{
    PlayerHealth ph;

    private void Start()
    {
        StartCoroutine(FindPlayer());
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ph.currentHealth -= 0.1f;
            ph.healthBar.fillAmount = ph.currentHealth / ph.startHealth;
        }
    }

    IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(1f);
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
}
