using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Skill", menuName = "Attacks/Skills")]
public class BaseAttack : ScriptableObject
{
    public string attackName; //feed into ui
    public string attackDescription; //feed into ui
    public string attackType; //declare magic or melee for different movement
    public int attackDamage; //used for strength scaling
    public int attackCost; //mp cost for abilities. Feed into ui
    public Sprite attackImage;
    /* 
    
    To create a new ability/skill you need to create a new script and have it inherit from this class
    Then create a prefab of it and add it to an entitys attack list and they *should* be able to use the new ability with the random being used

     */
}
