using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    public string attackName;
    public string attackDescription;
    public int attackDamage;
    public float attackCost; //for stuff like magic or stanima if we decide to include that
    /* 
    
    To create a new ability/skill you need to create a new script and have it inherit from this class
    Then create a prefab of it and add it to an entitys attack list and they *should* be able to use the new ability with the random being used

     */
}
