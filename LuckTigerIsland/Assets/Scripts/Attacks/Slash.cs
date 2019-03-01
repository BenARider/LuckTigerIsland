using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : BaseAttack {

    public Slash()
    {
        attackName = "Slash";
        attackDescription = "a simple slash towards multiple enemies";
        attackType = "Melee";
        attackDamage = 5;
        attackCost = 0;
    }
}
