using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaClass : MonoBehaviour {

	//Should change these to private and call them m_health, for example.
	[SerializeField]
	protected int maxHealth;
	[SerializeField]
	protected int health;
	[SerializeField]
	protected int strength; //basic attack
	[SerializeField]
	protected int defense;
	[SerializeField]
	protected int speed;
	[SerializeField]
	protected int mana;
	[SerializeField]
	protected int maxMana;
	[SerializeField]
	public int magicPower;
	[SerializeField]
	protected int level;
    [SerializeField]
    protected string Class;

    /// Following are used purely for battle integration. Used by both enemies and players.
    [SerializeField]
	protected bool m_attackedAlready = false;
	[SerializeField]
	private int entityNumber;
	[SerializeField]
	protected float baseRequiredSpeedForTurn = 100;
	[SerializeField]
	protected float requiredSpeedForTurn;
	[SerializeField]
	protected bool myTurn = false;
	[SerializeField]
	public bool battleWon = false;

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
        maxHealth = 200;
	}


	// Update is called once per frame
	void Update()
	{
		if (BattleControl.willDamage == "y" && BattleControl.currentTarget == entityNumber)
		{
			health -= BattleControl.currentDamage;
			BattleControl.willDamage = "n";
			BattleControl.currentTarget = 0;
			BattleControl.side = " ";

		}
	}
	public void CheckForDamage(string side)
	{
		if (BattleControl.willDamage == "y" && BattleControl.currentTarget == entityNumber)
		{
			health -= BattleControl.currentDamage;
			Debug.Log(side + ": " + entityNumber + " health total now: " + GetHealth());
			BattleControl.willDamage = "n";
			BattleControl.currentTarget = 0;
			BattleControl.side = " ";
		}
	}
	void TakeDamage(int damageTaken)
	{
		Debug.Log("Enemy taking damage");
		health -= damageTaken;
	}
	//-----------------------------------------------------------------------------------------------------
	//Setters and Getters
	public void Sethealth(int m_health) //The argument should be _health. The body should then be m_health = _health.
	{
		health = m_health;
	}
	public void ResetHealth()  //used at the beginning with initialisation, max health is required for ui calculations and heal effects.
	{
		health = maxHealth;
	}
	public int GetMaxHealth()
	{
		return maxHealth;
	}
	public int GetHealth()
	{
		return health;
	}
	public int GetStrength()
	{
		return strength;
	}
	public int GetMagicPower()
	{
		return magicPower;
	}
    public int GetDefense()
    {
        return defense;
    }

    public int GetSpeed()
    {
        return speed;
    }
	public float GetRequiredSpeed()
	{
		return requiredSpeedForTurn;
	}
	public void SetRequiredSpeed()
	{
		requiredSpeedForTurn = baseRequiredSpeedForTurn - GetSpeed();
	}
	public int GetMaxMana()
	{
		return maxMana;
	}
	public void ResetMana()
	{
		health = maxHealth;
	}
	public int GetMana()
	{
		return mana;
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

    protected void Damage() //this function will be repurposed soon to utilise various values.
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
