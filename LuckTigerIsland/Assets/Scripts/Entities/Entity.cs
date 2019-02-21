using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour {

	//General stats used to initialise entities
	[SerializeField]
	protected int m_maxHealth;
	[SerializeField]
	protected int m_health;
	[SerializeField]
	protected int m_strength; //basic attack
	[SerializeField]
	public int m_magicPower;
	[SerializeField]
	protected int m_defence;
	[SerializeField]
	protected int m_defenceMGC;
	[SerializeField]
	protected int m_speed;
	[SerializeField]
	protected int m_mana;
	[SerializeField]
	protected int m_maxMana;
	
	//level and class values
	[SerializeField]
	protected int m_level;
	[SerializeField]
	protected int m_xpAward;
	[SerializeField]
	protected int m_EXP;
	[SerializeField]
    protected string Class; //used to determine stat allocation in the other classes.

    /// Following are used purely for battle integration. Used by both enemies and players.
    [SerializeField]
	protected bool m_attackedAlready = false;
	[SerializeField]
	protected int m_entityNumber;
	[SerializeField]
	protected float m_baseRequiredSpeedForTurn = 100;
	[SerializeField]
	protected float m_requiredSpeedForTurn;
	[SerializeField]
	protected bool myTurn = false;
	[SerializeField]
	public bool battleWon = false;

	//used for damage calculations
	//-------------------------------------------
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
	void Start() {
       
	}


	// Update is called once per frame
	void Update()
	{
		if (BattleControl.willDamage == "y" && BattleControl.currentTarget == m_entityNumber)
		{
			m_health -= BattleControl.currentDamage;
			BattleControl.willDamage = "n";
			BattleControl.currentTarget = 0;
			BattleControl.side = " ";

		}
	}

	public void CheckForDamage(string side)
	{
		if (BattleControl.willDamage == "y" && BattleControl.currentTarget == m_entityNumber)
		{
			m_health -= BattleControl.currentDamage;
			Debug.Log(side + ": " + m_entityNumber + " health total now: " + GetHealth());
			BattleControl.willDamage = "n";
			BattleControl.currentTarget = 0;
			BattleControl.side = " ";
		}
	}

	void TakeDamage(int damageTaken)
	{
		Debug.Log("Enemy taking damage");
		m_health -= damageTaken;
	}

	//-----------------------------------------------------------------------------------------------------
	//Setters and Getters
	public void Sethealth(int _health) //The argument should be _health. The body should then be m_health = _health.
	{
		m_health = _health;
	}
	public void ResetHealth()  //used at the beginning with initialisation, max health is required for ui calculations and heal effects.
	{
		m_health = m_maxHealth;
	}
	public int GetMaxHealth()
	{
		return m_maxHealth;
	}
	public int GetHealth()
	{
		return m_health;
	}
	public int GetStrength()
	{
		return m_strength;
	}
	public int GetMagicPower()
	{
		return m_magicPower;
	}
    public int GetDefence()
    {
        return m_defence;
    }

    public int GetSpeed()
    {
        return m_speed;
    }
  
	public float GetRequiredSpeed()
	{
		return m_requiredSpeedForTurn;
	}
	public void SetRequiredSpeed()
	{
		m_requiredSpeedForTurn = m_baseRequiredSpeedForTurn - GetSpeed();
	}
	public int GetMaxMana()
	{
		return m_maxMana;
	}
	public void ResetMana()
	{
		m_mana = m_maxMana;
	}
	public int GetMana()
	{
		return m_mana;
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

    protected void Death()
    {
        if (m_health <= 0)
        {
            Destroy(gameObject); //could rework to set the object to be sideways/inactive instead of destroyed
        }
     
    }
}

