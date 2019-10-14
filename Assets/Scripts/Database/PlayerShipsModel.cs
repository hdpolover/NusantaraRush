using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerShipsModel
{
    public int id { get; set; }
    public int shipType { get; set; }
    public int rocketEquip { get; set; }
    public int mgEquip { get; set; }
    public int cannonEquip { get; set; }
    public int abilityEquip { get; set; }
    public int health { get; set; }

    public PlayerShipsModel(int id, int shipType, int rocketEquip, int mgEquip, int cannonEquip, int abilityEquip, int health)
    {
        this.id = id;
        this.shipType = shipType;
        this.rocketEquip = rocketEquip;
        this.mgEquip = mgEquip;
        this.cannonEquip = cannonEquip;
        this.abilityEquip = abilityEquip;
        this.health = health;
    }
}
