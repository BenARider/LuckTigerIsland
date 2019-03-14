using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : BaseAttack{
    public Bash()
    {
        attackName = "Bash";
        attackDescription = "a simple bash towards an enemy";
        attackType = "Melee";
        attackDamage = 10;
        attackCost = 0;
    }          
}

