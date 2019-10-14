using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    private float goldAmount;

    private void Start()
    {
        goldAmount = 0;
        goldText.SetText(goldAmount + "");
    }

    public void UpdateGold(float amount)
    {
        goldAmount += amount;
        goldText.SetText("" + goldAmount);
    }

}
