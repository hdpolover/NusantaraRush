using UnityEngine;

public class EnemyFireHandler : MonoBehaviour
{
    public Transform target;

    public GameObject firePoint;

    [Header("Bullet Prefabs")]
    public GameObject rocketPrefab;
    public GameObject mgPrefab;
    public GameObject cannonPrefab;
    
    [Header("Bullet Attributes")]
    public float bulletForce;
    public float rocketDamage = 20;
    public float mgDamage = 2;
    public float cannonDamage = 15;

    void Start()
    {
        //CheckEnemyShip();
    }

    private void Update()
    {
    }

    /*
    void CheckPlayerShip()
    {
        if (player.name.Equals("PlayerBoat1"))
        {
            rocketBtn.interactable = false;
            rocketBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            mgBtn.interactable = false;
            mgBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        }
        else if (player.name.Equals("PlayerBoat2"))
        {
            cannonBtn.interactable = false;
            cannonBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            rocketBtn.interactable = false;
            rocketBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        }
        else if (player.name.Equals("PlayerWarship1"))
        {
            mgBtn.interactable = false;
            mgBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        }
        else if (player.name.Equals("PlayerWarship2"))
        {
            cannonBtn.interactable = false;
            cannonBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            mgBtn.interactable = false;
            mgBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            rocketBtn.interactable = false;
            rocketBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        }
    }
    */

    public void FireRocket()
    {
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(rocketPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;

        //mporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        Temporary_RigidBody.AddForce(transform.forward * bulletForce);

        Destroy(Temporary_Bullet_Handler, 5.0f);
    }
    
}