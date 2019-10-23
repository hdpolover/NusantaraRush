using System.Collections;
using UnityEngine;

public class IslandDamage : MonoBehaviour
{
    PlayerHealth ph;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find(PlayerManager.instance.playerShipNaame);
        ph = player.GetComponent<PlayerHealth>();
        //StartCoroutine(FindPlayer());
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == player)
        {
            ph.currentHealth -= 0.1f;
            ph.healthBar.fillAmount = ph.currentHealth / ph.startHealth;
        }
    }

    /*
    IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(1f);
        ph = player.GetComponent<PlayerHealth>();
    }
    */
}
