using UnityEngine;

public class CrateHandler : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find(PlayerManager.instance.playerShipNaame);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == player.tag)
        {
            Destroy(gameObject);

            player.GetComponent<PlayerStats>().UpdateResources(getRandomGold(),
                getRandomPart(),
                getRandomAmmo());
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * 1);
    }

    int getRandomGold()
    {
        return Random.Range(10, 50);
    }

    int getRandomPart()
    {
        return Random.Range(10, 50);
    }

    int getRandomAmmo()
    {
        return Random.Range(10, 50);
    }
}
