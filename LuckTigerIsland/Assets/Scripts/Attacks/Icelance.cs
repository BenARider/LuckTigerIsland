using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icelance : BaseAttack {

    public Icelance()
    {
        attackName = "IceLance";
        attackDescription = "a simple Icelance to launch at an enemy";
        attackType = "Magic";
        attackDamage = 25;
        attackCost = 12;
    }
}
