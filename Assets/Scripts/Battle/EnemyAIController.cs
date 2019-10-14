using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    [Header("Behaviors")]
    public float lookRadius = 10f;
    public float fireRate;
    public float fireCountDown;

    [Header("Bullet Prefabs")]
    public GameObject rocketPrefab;
    public GameObject mgPrefab;
    public GameObject cannonPrefab;

    [Header("Bullet Attributes")]
    public float bulletForce;
    public float forceMultiplier;
    public float rocketDamage = 20;
    public float mgDamage = 2;
    public float cannonDamage = 15;

    [Header("Turrets")]
    public Transform firePoint;
    public Transform firePoint1;
    public Transform turretToRotate;

    private bool isBig;
    private bool isMedium;
    private bool isSmall;

    private string enemyTag;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        enemyTag = gameObject.tag.ToString();

        CheckEnemyShip();
    }

    void CheckEnemyShip()
    {
        if (enemyTag.Equals("EnemyBig"))
        {
            isBig = true;
            isMedium = false;
            isSmall = false;
        } else if (enemyTag.Equals("EnemyMedium"))
        {
            isBig = false;
            isMedium = true;
            isSmall = false;
        } else if (enemyTag.Equals("EnemySmall"))
        {
            isBig = false;
            isMedium = false;
            isSmall = true;
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                SetBulletForce();

                if (isBig)
                {
                    Vector3 turretDirection = target.position - agent.transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(turretDirection);
                    Vector3 rotation = Quaternion.Lerp(turretToRotate.rotation, lookRotation, 10f * Time.deltaTime).eulerAngles;

                    turretToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                    
                    if (fireCountDown <= 0f)
                    {
                        Attack();
                        fireCountDown = 3f / fireRate;
                    }
            
                } else if (isMedium)
                {
                    Vector3 turretDirection = target.position - firePoint.transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(turretDirection);
                    Vector3 rotation = Quaternion.Lerp(turretToRotate.rotation, lookRotation, 10f * Time.deltaTime).eulerAngles;

                    turretToRotate.rotation = Quaternion.Euler(-90f, rotation.y, 0f);

                    if (fireCountDown <= 0f)
                    {
                        Attack();
                        fireCountDown = 3f / fireRate;
                    }
                } else if (isSmall)
                {
                    Vector3 turretDirection = target.position - firePoint.transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(turretDirection);
                    Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, 10f * Time.deltaTime).eulerAngles;

                    transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

                    if (fireCountDown <= 0f)
                    {
                        Attack();
                        fireCountDown = 1f / fireRate;
                    }
                }

                fireCountDown -= Time.deltaTime;
            }
        }

        bulletForce = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void Attack()
    {
        if (isBig)
        {
            GameObject rocket = Instantiate(rocketPrefab, firePoint.transform.position, Quaternion.identity) as GameObject;

            rocket.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * bulletForce);

            Destroy(rocket, 3.0f);
        } else if (isMedium)
        {
            GameObject cannon = Instantiate(cannonPrefab, firePoint.transform.position, Quaternion.identity) as GameObject;

            cannon.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * bulletForce);

            Destroy(cannon, 3.0f);
        } else if (isSmall)
        {
            GameObject mg = Instantiate(mgPrefab, firePoint.transform.position, Quaternion.identity) as GameObject;
            GameObject mg1 = Instantiate(mgPrefab, firePoint1.transform.position, Quaternion.identity) as GameObject;

            mg.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * bulletForce);
            mg1.GetComponent<Rigidbody>().AddForce(firePoint1.transform.forward * bulletForce);

            Destroy(mg, 2.0f);
            Destroy(mg1, 2.0f);
        }
    }

    void SetBulletForce()
    {
        float distanceToTarget = Vector3.Distance(target.position, firePoint.transform.position);

        bulletForce = distanceToTarget * forceMultiplier;
    }
}
