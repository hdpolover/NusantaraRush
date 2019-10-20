using UnityEngine;

public class MapCam : MonoBehaviour
{

    public Transform target;
    
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        }
    }
}
