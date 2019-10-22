using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI partText;
    public TextMeshProUGUI ammoText;
    
    private int goldAmount;
    private int partAmount;
    private int ammoAmount;

    private void Start()
    {
        goldAmount = 0;
        goldText.SetText(goldAmount + "");
        partAmount = 0;
        partText.SetText(partAmount + "");
        ammoAmount = 0;
        ammoText.SetText(ammoAmount + "");
    }

    public void UpdateResources(int gAmount, int pAmount, int aAmount)
    {
        goldAmount += gAmount;
        goldText.SetText("" + goldAmount);
        partAmount += pAmount;
        partText.SetText("" + partAmount);
        ammoAmount += aAmount;
        ammoText.SetText("" + ammoAmount);
    }

}
