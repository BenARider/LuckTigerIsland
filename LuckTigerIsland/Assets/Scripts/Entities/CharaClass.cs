using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaClass : MonoBehaviour {

    //Should change these to private and call them m_health, for example.
    public int health;
    public int strength; //basic attack
    public int defense;
    public int speed;
    public int level;

    public int tempDMGReduct = 0; //the amount of damage reduced to the intial damage
    public int totalDMG = 0; //total amount of damage that goes through.
    public int chanceToHit;

    //CALCULATION IDEA: (atk: strength / def: defense = tempDMGReduct) atk: strength - tempDMGReduct = totalDMG
    //e.g. (100 / 10 = 10) 100 - 10 = 90
    //e.g. (56 / 8 = 7) 56 - 7 = 49

    public bool attacking = false;
    public bool dmgRecieve = false;
    public bool dmgDealt = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	//void Update ()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    health --;
	//}
    //-----------------------------------------------------------------------------------------------------
    //Setters and Getters
    public void Sethealth(int m_health) //The argument should be _health. The body should then be m_health = _health.
    {
        health = m_health;
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetStrength()
    {
        return strength;
    }

    public int GetDefense()
    {
        return defense;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public void SetLevel(int m_level)
    {
        level = m_level;
    }
    
    public int GetLevel()
    {
        return level;
    }

    //-----------------------------------------------------------------------------------------------------
    protected void Attack()
    {
        if(attacking == true)
        {
            chanceToHit = Random.Range(1, 100);

            if (chanceToHit <= 85)
            {
                dmgRecieve = true;
            }
            else
                return;
     
        }
    }

    protected void Damage()
    {
        if (dmgRecieve == true)
        {
            GetStrength();
            GetDefense();
            //(GetStrength() / GetDefense() = tempDMGReduct) GetStrength - tempDMGReduct = totalDMG;
            dmgRecieve = false;
            dmgDealt = true;

            if(dmgDealt == true)
            {
                //m_health - totalDMG;
                dmgDealt = false;
            }
        }
    }

    protected void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
     
    }
}
