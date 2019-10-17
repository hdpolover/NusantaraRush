using UnityEngine;
using TMPro;

public class BulletHandler : MonoBehaviour
{
    public float maxMgBulletCount = 300;
    public float maxCannonBulletCount = 50;
    public float maxRocketBulletCount = 20;

    [HideInInspector] public float mgBulletCount;
    [HideInInspector] public float cannonBulletCount;
    [HideInInspector] public float rocketBulletCount;

    public TextMeshProUGUI mgCountText;
    public TextMeshProUGUI cannonCountText;
    public TextMeshProUGUI rocketCountText;

    private void Update()
    {
        mgCountText.SetText("" + Mathf.Round(mgBulletCount));
        cannonCountText.SetText("" + Mathf.Round(cannonBulletCount));
        rocketCountText.SetText("" + Mathf.Round(rocketBulletCount));
    }
}
