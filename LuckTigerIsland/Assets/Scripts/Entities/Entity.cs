﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : MonoBehaviour {

	//General stats used to initialise entities
	[SerializeField]
	protected int m_maxHealth;
	[SerializeField]
	protected int m_health;
	[SerializeField]
	protected int m_strength; //basic attack
    private int m_previousStrength;
	[SerializeField]
	public int m_magicPower;
    private int m_previousMagicPower;
    [SerializeField]
	protected int m_defence;
    private int m_previousDefence;
    [SerializeField]
	protected int m_defenceMGC;
    private int m_previousDefenceMGC;
    [SerializeField]
	protected int m_speed;
    private int m_previousSpeed;
    [SerializeField]
	protected int m_mana;
	[SerializeField]
	protected int m_maxMana;
    [SerializeField]
    protected bool m_stunned = false;
    [SerializeField]
    protected bool m_afflicted = false;

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
    protected bool m_canBeBuffed = true;
	[SerializeField]
	protected int m_entityNumber; 
	[SerializeField]
	protected static float m_baseRequiredSpeedForTurn = 100;
	[SerializeField]
	protected float m_requiredSpeedForTurn;
	[SerializeField]
	protected bool myTurn = false;
	[SerializeField]
	public bool battleWon = false;
    [SerializeField]
    protected float currentSpeed = 0f;
    [SerializeField]
    public bool isAlive = true;
    [SerializeField]
    protected bool countedDead = false; //used to prevent insta wins or gameovers
	[SerializeField]
	protected bool alreadyAfflicted = false;
	[SerializeField]
	protected int afflictionTimes = 0;


    protected float walkSpeed = 5f;


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

    public enum TurnState
    {
        eProssesing,
        eChooseAction,
        eChooseTarget,
        eWaiting,
        eAction,
        eDead
    }
    public TurnState currentState;
    
    public enum Affliction
    {
        eNone,
        eOnFire,
        eFreeze,
        eInfected,
        eStunned
    }

    public enum Buffs
    {
        eNone,
        eBlock,
        eStrength,
        eMagic
    }
    public Affliction currentAffliction;
    public Buffs currentBuff;
    protected Vector3 startPosition; //used for animation, move to player when attacking and then back

    protected bool actionHappening = false; //Think attackAlready, stops the entities spamming
    public GameObject EntityToAttack; //What the entity wants to attack

    public List<BaseAttack> attacks = new List<BaseAttack>();
    public List<InventoryObject> HealthPotions = new List<InventoryObject>();
    public List<InventoryObject> ManaPotions = new List<InventoryObject>();
    protected BaseAttack m_chosenAction;


    protected bool MoveTo(Vector3 target)
    {
        Debug.Log("Player moving");
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, walkSpeed * Time.deltaTime)); //returns false until the enity is at its target
    }

	protected IEnumerator checkAffliction(int maxAfflictions)
	{

		if (currentAffliction == Affliction.eNone)
		{
			yield break;
		}

		yield return new WaitForSeconds(1.0f);
		afflictionTimes++;

		if (currentAffliction == Affliction.eOnFire)
		{
			m_health -= 5; //Do some damge calc against resistances and weaknesses
			Debug.Log("on fire");
		}

		if (currentAffliction == Affliction.eInfected)
		{
			m_health -= 5; //Do some damge calc against resistances and weaknesses
		}

		if (currentAffliction == Affliction.eFreeze)
		{
			m_health -= 2;
		}

		if (currentAffliction == Affliction.eStunned)
		{
			m_stunned = true;
		}

		if (afflictionTimes >= maxAfflictions)
		{
			stopAfflictions();
			StopCoroutine("checkAffliction");
		}
	}
	protected IEnumerator resetAffliction()
	{
		if (currentAffliction != Affliction.eNone)
		{
			yield return new WaitForSeconds(10.0f);
			currentAffliction = Affliction.eNone;
		}
	}
	protected void stopAfflictions()
	{
        afflictionTimes = 0;
		m_afflicted = false;
		alreadyAfflicted = false;
	}

    protected void addAffliction(BaseAttack AttackAffliction)
    {

        if (AttackAffliction.attackAffliction == "Fire")
            currentAffliction = Affliction.eOnFire;

        if (AttackAffliction.attackAffliction == "Freeze")
            currentAffliction = Affliction.eFreeze;

        if (AttackAffliction.attackAffliction == "Infect")
            currentAffliction = Affliction.eInfected;

        if (AttackAffliction.attackAffliction == "Stun")
            currentAffliction = Affliction.eStunned;

    }
    protected void CheckBuffs()
    {
        if (currentBuff == Buffs.eBlock)
        {
            Debug.Log("Purging block");
            ClearBuffs();
        }
    }
    protected IEnumerator ApplyBuff(float buffTime, int buffMultiplier)
    {
        switch (currentBuff)
        {
            case Buffs.eNone:

                break;
            case Buffs.eBlock:
            case Buffs.eStrength:
                break;
            case Buffs.eMagic:
                break;
            default:
                break;
        }

        if (currentBuff == Buffs.eNone)
        {
            Debug.Log("Returning from apply");
            m_canBeBuffed = true;
            yield break;
        }
        if (currentBuff == Buffs.eBlock)
        {
            Debug.Log("Applying Block");
            m_previousDefence = m_defence;
            m_previousDefenceMGC = m_defenceMGC;
            m_defence = (m_defence * buffMultiplier);
            m_defenceMGC = (m_defenceMGC * buffMultiplier);
        }
        if(currentBuff == Buffs.eMagic)
        {
            m_previousMagicPower = m_magicPower;
            m_magicPower = m_magicPower * buffMultiplier;
        }
        if(currentBuff == Buffs.eStrength)
        {
            m_previousStrength = m_strength;
            m_strength = m_strength * buffMultiplier;
        }
        if(buffTime != 0)
        {
            Debug.Log("About to remove buffs");
            yield return new WaitForSeconds(buffTime);
            ClearBuffs();
        }
    }

    protected void AddBuff(BaseAttack AttackBuff)
    {
        Debug.Log("Adding buff");
        if(AttackBuff.attackAffliction == "Block")
        {
            currentBuff = Buffs.eBlock;
        }
        if(AttackBuff.attackAffliction == "Magic")
        {
            currentBuff = Buffs.eMagic;
        }
        if(AttackBuff.attackAffliction == "Strength")
        {
            currentBuff = Buffs.eStrength;
        }
        if(AttackBuff.attackAffliction != "" && m_canBeBuffed == true)
        {
            Debug.Log("Applying buff");
            m_canBeBuffed = false;
            StartCoroutine(ApplyBuff(AttackBuff.skillDuration, AttackBuff.skillMultiplier));
            Debug.Log("Pranked");
        }
    }
    protected void ClearBuffs()
    {
        currentBuff = Buffs.eNone;
        m_defence = m_previousDefence;
        m_defenceMGC = m_previousDefenceMGC;
        m_magicPower = m_previousMagicPower;
        m_strength = m_previousStrength;
        m_speed = m_previousSpeed;
        m_canBeBuffed = true;
    }

    //-----------------------------------------------------------------------------------------------------
    //Setters and Getters

    public void SetPreviousStats()
    {
        m_previousDefence = m_defence;
        m_previousDefenceMGC = m_defenceMGC;
        m_previousMagicPower = m_magicPower;
        m_previousStrength = m_strength;
        m_previousSpeed = m_speed;
    }
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
    public void SetStrength(int _strength)
    {
        m_strength = _strength;
    }
    public void SetDefence(int _defence)
    {
        m_defence = _defence;
    }
    public int GetMagicPower()
	{
		return m_magicPower;
	}
    public int GetDefence()
    {
        return m_defence;
    }
 
    public int GetMagicDefence()
    {
        return m_defenceMGC;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
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

    public int GetEntityNo()
    {
        return m_entityNumber;
    }
	public void SetEntityNo(int entityNumber)
	{
		m_entityNumber = entityNumber;
	}
    public int GetLevel()
    {
        return m_level;
    }
    public int GetEXP()
    {
        return m_EXP;
    }
    public void TakeDamage(int damageAmount, BaseAttack attack)
    {
        m_health -= damageAmount;
        if (m_chosenAction.attackAffliction != "" && m_chosenAction.attackType != "Buff" && alreadyAfflicted == false)
        {
            alreadyAfflicted = true;
            addAffliction(m_chosenAction);
            StartCoroutine("checkAffliction", m_chosenAction.skillDuration);
        }

        if (GetHealth() <= 0)
        {
            currentState = TurnState.eDead;
        }
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
           // Destroy(gameObject); //could rework to set the object to be sideways/inactive instead of destroyed
        }
     
    }
}

