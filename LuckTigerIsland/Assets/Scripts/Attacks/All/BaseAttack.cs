using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Skill", menuName = "Attacks/Skills")]
public class BaseAttack : ScriptableObject
{
    public enum AttackAffliction
    {
        eFire,
        eFreeze,
        eInfect,
        eStun,
        ePoison,
        eNone,
        eBlock,
        eMagic,
        eStrength,
        eAttack,
		eDeffence,
        eHeal
    }

    public enum AttackType
    {
        eMelee,
        eMagic,
        eBuff,
        ePartyWide
    }

    public string attackName; //feed into ui
    public string attackDescription; //feed into ui
    public AttackType attackType; //declare magic or melee for different movement
    public int attackDamage; //used for strength scaling
    public int skillMultiplier = 1; //used for multipliers for strong attacks/buffs. Keep at 1 otherwise
    public float skillDuration = 0; //used for anything with afflictions or buffs. Use whole numbers for afflictions, can use decimals for buffs.
    public int attackCost; //mp cost for abilities. Feed into ui
    public Sprite attackImage;
    public AttackAffliction attackAffliction; //declares what affliction should be applied on contact

    /* 
    
    To create a new ability/skill you need to create a new script and have it inherit from this class
    Then create a prefab of it and add it to an entitys attack list and they *should* be able to use the new ability with the random being used

     */
}
