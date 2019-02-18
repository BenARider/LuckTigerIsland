using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnemyState //Think of mentality of the enemies, this state will change on different values changing i.e low health = fleeing.  Will add to this when i can
{
	eAttcking,
	eDefending, //Will code in when defending is working, Ditto for the other three
	eHealing,
	eFleeing,
	eIdle
}

public class EnemChara : CharaClass
{
	//private bool canAttack = false; //used to prevent the ai from having too many turns. Only enabled on use of state transition.
	public int aggress; //likelihood to attack oppossing attacker (Between 1-20)
	public int intel; //likelihood to attack pm with high value (Between 1-20)
	public int XP; //amount of xp they give
	private int m_Target; //used to determine where the skills will hit(Prone to change later on)
	EnemyState CurrentState;
	/* //currently underdevelopment
    public string elemWeak;
    public string elemResi;
    */

	void enemCharaStats(int hth, int man, int str, int def, int spd, int lvl, int agr, int itl, int xp)
	{
		maxHealth = hth;
		maxMana = man;
		strength = str;
		defense = def;
		speed = spd;
		level = lvl;
		aggress = agr;
		intel = itl;
		XP = xp;
	}
	public EnemChara Tiger;

    // Use this for initialization
    void Start()
	{
        if (Class == "Warrior")
        {
            enemCharaStats(150, 50, 40, 20, 80, 3, 20, 4, 50);
        }
        if (Class == "Ninja")
        {
            enemCharaStats(75, 100, 10, 15, 85, 2, 15, 6, 50);
        }
        if (Class == "Cleric")
        {
            enemCharaStats(50, 150, 5, 7, 70, 3, 5, 5, 50);
        }
        if (Class == "Archer")
        {
            enemCharaStats(70, 125, 20, 10, 75, 2, 10, 8, 50);
        }
		requiredSpeedForTurn = baseRequiredSpeedForTurn - GetSpeed();
		ResetHealth();
		ResetMana();
        Debug.Log("Enemy Values Set");
    }

    void decideState()
    {
        if (GetHealth() < GetHealth() / 2) //would have a && healskill not on cool down later for eHeal
        {
            CurrentState = EnemyState.eDefending;
            Debug.Log("Enemy Defending");
        }
        else if (GetHealth() < GetHealth() / 4)
        {
            CurrentState = EnemyState.eFleeing;
            Debug.Log("The enemy is fleeing");
        }
        else
        {
            CurrentState = EnemyState.eAttcking;
            Debug.Log("The enemy is going to attack" + m_Target);
        }
    }

    //for each valid update, determine the state in the specific ai, pass the state to this script for the results to be used here. This will contain almost all the states for any enemy
    void Update()
	{
		if (SpeedTimer.m_speedCounter % requiredSpeedForTurn == 0 && SpeedTimer.isPaused == false || SpeedTimer.m_speedCounter % requiredSpeedForTurn == 0.5 && m_attackedAlready == false && SpeedTimer.m_speedCounter > 1.0f)
		{
            Debug.Log("Enemy Turn");
            decideState();

			if (m_attackedAlready == false) //Stuff to be rearranged later for balanced
			{

				if (CurrentState == EnemyState.eFleeing)
				{
					battleWon = true;
				}

				if (CurrentState == EnemyState.eAttcking)
				{
                    decideTarget();
                    randomBasicAttack();
				}

				if (CurrentState == EnemyState.eDefending)
				{
					//Defend
				}

				if (CurrentState == EnemyState.eHealing)
				{
					//Heal code/skill
				}
			}
		}
		if (BattleControl.side == "Player")
		{
			CheckForDamage("Enemy");
		}
		//will eventually replace this update with something similar, or just make it check for very global things, eg death
		/*
		Attack();
        Damage();
		*/
		Death();
		SpeedTimer.isPaused = false;
	}
	void decideTarget()
	{
		if (CurrentState == EnemyState.eAttcking)
		{
			m_Target = UnityEngine.Random.Range(1, 4); //determines target for enemy attack
            BattleControl.currentTarget = m_Target;
            Debug.Log("Enemy is going to attack player " + m_Target);
		}

		if (CurrentState == EnemyState.eHealing)
		{
			m_Target = UnityEngine.Random.Range(5, 8); //Random healing for now until healing is actually in game
            BattleControl.currentTarget = m_Target;
            Debug.Log("Enemy is going to heal enemy " + m_Target);
        }
    }

	void SetAttack()
	{
		m_attackedAlready = false;
	}

	IEnumerator returnEnemy()
	{
		EnemyAttack();
		Debug.Log("Enemy attacking");
        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
        SpeedTimer.isPaused = false;
		Invoke("SetAttack", 1.0f);
        Debug.Log("Enemy done attacking");
	}

	void EnemyAttack()
	{
		Debug.Log("Enemy attacking");
		BattleControl.side = "Enemy";
		BattleControl.willDamage = "y";
	}
	//will eventually replace this to utilise inheritance, eg Enemy_AI -> EnemChara -> CharaClass
	void randomBasicAttack()
	{
        Debug.Log("Basic Attack");
		m_attackedAlready = true;
		BattleControl.currentDamage = GetStrength();
		BattleControl.currentTarget = m_Target;
		BattleControl.side = "Enemy";
        transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
        Debug.Log("Enemy has attacked player " + m_Target);
        StartCoroutine(returnEnemy());
	}

	void randomHeal() //when healing implemented, chances are it will just send a negative value through the system as that will give health
	{
		m_attackedAlready = true;
		BattleControl.currentHealValue -= GetStrength(); //will be replaced with the magic formula
		BattleControl.currentTarget = m_Target;
		transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
		StartCoroutine(returnEnemy());
	}

    public void StateTransition(EnemyState currentState)
    {
        CurrentState = currentState;
    }
}

