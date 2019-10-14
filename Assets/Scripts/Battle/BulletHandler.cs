using UnityEngine;
using TMPro;

public class BulletHandler : MonoBehaviour
{
    public float maxBulletCount = 100;
    public float bulletCount;
    public TextMeshProUGUI t;
    
    void Start()
    {
        bulletCount = 100;
        t.SetText("" + Mathf.Round(bulletCount));
    }

    private void Update()
    {
        t.SetText("" + Mathf.Round(bulletCount));
    }
}
