using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------------
    //Stats
    protected int m_health;
    protected int m_mana;
    protected int m_strength;
    protected int m_defence;
    protected int m_defenceMGC; //defense against magic
    protected int m_speed;
    protected int m_magicPow; //strength of magic

    //--------------------------------------------------------------------------------------------------
    //Level and Experience values
    protected int m_level; //current level of the character
    protected int m_XP; //amount of xp given
    protected int m_EXP; //current amount of xp

    //--------------------------------------------------------------------------------------------------
    // Player Specific values
    protected int m_value; //The value of the party member in question, determines the likelihood of being attacked by intelligent enemies.

    //--------------------------------------------------------------------------------------------------
    //Enemy specific values
    protected int m_aggress; //likelihood to attack oppossing attacker (Between 1-20)
    protected int m_intel; //likelihood to attack pm with high value (Between 1-20)

    //currently underdevelopment
    /*
    public string elemWeak;
    public string elemResi;
    */
    

    //--------------------------------------------------------------------------------------------------
    //Damage values
    public int tempDMGReduct = 0; //the amount of damage reduced to the intial damage
    public int totalDMG = 0; //total amount of damage that goes through.
    public int chanceToHit;

    //CALCULATION IDEA: (atk: strength / def: defense = tempDMGReduct) atk: strength - tempDMGReduct = totalDMG
    //e.g. (100 / 10 = 10) 100 - 10 = 90
    //e.g. (56 / 8 = 7) 56 - 7 = 49

    //--------------------------------------------------------------------------------------------------
    //bools
    public bool attacking = false;
    public bool dmgRecieve = false;
    public bool dmgDealt = false;

    //--------------------------------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update ()
    {
     
    }

    //--------------------------------------------------------------------------------------------------
    //Setters and Getters
    public void Sethealth(int _health)
    {
        m_health = _health;
    }
    public int GetHealth()
    {
        return m_health;
    }
    public int GetStrength()
    {
        return m_strength;
    }

    public int GetDefence()
    {
        return m_defence;
    }

    public int GetSpeed()
    {
        return m_speed;
    }

    public void SetLevel(int _level)
    {
        m_level = _level;
    }

    public int GetLevel()
    {
        return m_level;
    }

    //--------------------------------------------------------------------------------------------------
    protected void Attack()
    {
        if (attacking == true)
        {
            //Chance of the attack hitting
            chanceToHit = Random.Range(1, 100);

            if (chanceToHit <= 85)
            {
                dmgRecieve = true;
            }
            else
                return;

        }
    }
    
    //General logic of damage calculation
    protected void Damage()
    {
        if (dmgRecieve == true)
        {
            tempDMGReduct = GetStrength() / GetDefence();
            totalDMG = GetStrength() - tempDMGReduct;

            dmgRecieve = false;
            dmgDealt = true;

            if (dmgDealt == true)
            {
                m_health = m_health - totalDMG;
                dmgDealt = false;
            }
        }
    }

    //death function
    protected void Death()
    {
        //if (m_health == 0)
        //{
          // Destroy(gameObject);
        //}

    }
}

