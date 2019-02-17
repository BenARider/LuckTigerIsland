using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnemyState //Think of mentality of the enemies, this state will change on different values changing i.e low health = fleeing.  Will add to this when i can
{
	eAttcking,
	eDefending, //Will code in when defending is working, Ditto for thje other three
	eHealing,
	eFleeing,
	eIdle
}
public enum Command
{
	eAttack,
	eDefend, //Will code in when defending is working, Ditto for thje other three
	eHeal,
	eFlee
}
public class EnemChara : CharaClass
{
	private bool canAttack = false; //used to prevent the ai from having too many turns. Only enabled on use of state transition.
	public int aggress; //likelihood to attack oppossing attacker (Between 1-20)
	public int intel; //likelihood to attack pm with high value (Between 1-20)
	public int XP; //amount of xp they give
	private int m_Target; //used to determine where the skills will hit(Prone to change later on)
	EnemyState CurrentState;
	Command Command;
	/* //currently underdevelopment
    public string elemWeak;
    public string elemResi;
    */

	void enemCharaStats(int hth, int man, int str, int def, int spd, int lvl, int agr, int itl, int xpa)
	{
		maxHealth = hth;
		maxMana = man;
		strength = str;
		defense = def;
		speed = spd;
		level = lvl;
		aggress = agr;
		intel = itl;
		XP = xpa;
	}
	public EnemChara Tiger;

	// Use this for initialization
	void Start()
	{
		enemCharaStats(100, 100, 10, 5, 10, 4, 16, 4, 150);
		requiredSpeedForTurn = baseRequiredSpeedForTurn - GetSpeed();
		ResetHealth();
		ResetMana();
	}

	//for each valid update, determine the state in the specific ai, pass the state to this script for the results to be used here. This will contain almost all the states for any enemy
	void Update()
	{
		if (SpeedTimer.isPaused == true && canAttack)
		{

			if (m_attackedAlready == false) //Stuff to be rearranged later for balanced
			{

				if (CurrentState == EnemyState.eFleeing)
				{
					battleWon = true;
				}

				if (CurrentState == EnemyState.eAttcking)
				{
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
			if (GetHealth() <= 0)
			{
				Debug.Log("The enemy is dead");
			}
		}
		if (BattleControl.side == "Player")
		{
			CheckForDamage();
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
		}

		if (CurrentState == EnemyState.eHealing)
		{
			m_Target = UnityEngine.Random.Range(5, 8); //Random healing for now until healing is actually in game
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
		SpeedTimer.isPaused = false;
		Invoke("SetAttack", 1.0f);
	}

	void EnemyAttack()
	{
		Debug.Log("Enemy attacking");
		BattleControl.side = "Enemy";
		BattleControl.willDamage = "y";
		transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
	}
	//will eventually replace this to utilise inheritance, eg Enemy_AI -> EnemChara -> CharaClass
	void randomBasicAttack()
	{
		m_attackedAlready = true;
		canAttack = false;
		decideTarget();
		BattleControl.currentDamage = GetStrength();
		BattleControl.currentTarget = m_Target;
		BattleControl.side = "Enemy";
		transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
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

	public void StateTransition(int stateNumber)
	{
		if(stateNumber == 0)
		{
			canAttack = true;
			CurrentState = EnemyState.eAttcking;
		}
		if (stateNumber == 1)
		{
			CurrentState = EnemyState.eDefending;
		}
		if (stateNumber == 2)
		{
			CurrentState = EnemyState.eFleeing;
		}
		if (stateNumber == 3)
		{
			CurrentState = EnemyState.eHealing;
		}
		if(stateNumber == 4)
		{
			CurrentState = EnemyState.eIdle;
		}
	}
}
