using UnityEngine;
using UnityEngine.UI;

public class PlayerShipInfo : MonoBehaviour
{
    public Text ship_name;
    public Text rocketLevel;
    public Text mgLevel;
    public Text cannonLevel;
    // Start is called before the first frame update
    void Start()
    {
        CheckPlayerShipInfo();
    }

    void CheckPlayerShipInfo()
    {
        int ship_id = PlayerManager.instance.chosen_ship;
        int rocket_level = PlayerManager.instance.rocket_level;
        int mg_level = PlayerManager.instance.mg_level;
        int cannon_level = PlayerManager.instance.cannon_level;

        //Set ship name
        {
            if (ship_id == 1)
            {
                ship_name.text = "Default Ship";
            }
            else if (ship_id == 2)
            {
                ship_name.text = "Main Boat 1";
            }
            else if (ship_id == 3)
            {
                ship_name.text = "Main Boat 2";
            }
            else if (ship_id == 4)
            {
                ship_name.text = "Warship 1";
            }
            else if (ship_id == 5)
            {
                ship_name.text = "Warship 2";
            }
        }

        //Set weapons level
        {
            rocketLevel.text = "Level Rocket : " + rocket_level;
            mgLevel.text = "Level MG : " + mg_level;
            cannonLevel.text = "Level Cannon : " + cannon_level;
        }
    }
}
