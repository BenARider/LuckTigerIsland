using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : BaseAttack
{

    public Fireball()
    {
        attackName = "Fireball";
        attackDescription = "a simple fireball to launch at an enemy";
        attackType = "Magic";
        attackDamage = 15;
        attackCost = 5;
    }
    
}
