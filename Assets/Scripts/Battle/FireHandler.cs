using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireHandler : MonoBehaviour
{ 
    public GameObject player;
    public GameObject firePoint;

    public GameObject rocketPrefab;
    public GameObject mgPrefab;
    public GameObject cannonPrefab;
    
    public Button mgBtn;
    public Button cannonBtn;
    public Button rocketBtn;
    //public GameObject panelHabis;
    BulletHandler bh;

    public float bulletForce;

    public float rocketDamage = 20;
    public float mgDamage = 2;
    public float cannonDamage = 15;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bh = player.GetComponent<BulletHandler>();

        mgBtn = GameObject.Find("Mg").GetComponent<Button>();
        rocketBtn = GameObject.Find("Rocket").GetComponent<Button>();
        cannonBtn = GameObject.Find("Cannon").GetComponent<Button>();

        //panelHabis.SetActive(false);

        CheckPlayerShip();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            FireCannon();
        }
    }

    void CheckPlayerShip()
    {
        if (player.name.Equals("PlayerBoat1"))
        {
            rocketBtn.interactable = false;
            rocketBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            mgBtn.interactable = false;
            mgBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        } else if (player.name.Equals("PlayerBoat2"))
        {
            cannonBtn.interactable = false;
            cannonBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            rocketBtn.interactable = false;
            rocketBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        } else if (player.name.Equals("PlayerWarship1"))
        {
            mgBtn.interactable = false;
            mgBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        } else if (player.name.Equals("PlayerWarship2"))
        {
            cannonBtn.interactable = false;
            cannonBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            mgBtn.interactable = false;
            mgBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            rocketBtn.interactable = false;
            rocketBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        }
    }

    public void FireRocket()
    {
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(rocketPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
        
        //mporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
        
        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
        
        Temporary_RigidBody.AddForce(transform.forward * bulletForce);
        ReduceAmmo();

        Destroy(Temporary_Bullet_Handler, 5.0f);

    }

    public void FireMg()
    {
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(mgPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;

        //mporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        Temporary_RigidBody.AddForce(transform.forward * bulletForce);
        ReduceAmmo();

        Destroy(Temporary_Bullet_Handler, 5.0f);

    }

    public void FireCannon()
    {
        if (bh.bulletCount <= 0.5f)
        {
            //panelHabis.SetActive(true);
            cannonBtn.interactable = false;
            cannonBtn.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        }
        else
        {
            
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(cannonPrefab, firePoint.transform.position, Quaternion.identity) as GameObject;

            Temporary_Bullet_Handler.transform.Rotate(Vector3.left);

            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            Temporary_RigidBody.AddForce(transform.forward * bulletForce);
            ReduceAmmo();

            Destroy(Temporary_Bullet_Handler, 3.0f);
        }
    }

    public void ReduceAmmo()
    {
       bh.bulletCount -= 1;
    }
}