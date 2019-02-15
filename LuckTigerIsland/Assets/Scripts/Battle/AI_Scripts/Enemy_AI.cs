using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum EnemyState //Think of mentality of the enemies, this state will change on different values changing i.e low health = fleeing.  Will add to this when i can
{ 
    eAttcking,
    eDefending, //Will code in when defending is working, Ditto for thje other three
    eHealing,
    eFleeing
}

public enum Command
{
    eAttack,
    eDefend, //Will code in when defending is working, Ditto for thje other three
    eHeal,
    eFlee
}

public class Enemy_AI : BattleEntity
{
    EnemyState CurrentState;
    Command Command;

    public void StateTransition(EnemyState currentState, Command command)
    {
        CurrentState = currentState;
        Command = command;
    }


    [SerializeField]
    private int m_Target;

    // Use this for initialization
    void Start ()
    {
        requiredSpeedForTurn = baseRequiredSpeedForTurn - m_Speed;
    }
	void SetAttack()
	{
		m_attackedAlready = false;
	}

    void EnemyAttack()
    {
        Debug.Log("Enemy attacking");
        BattleControl.side = "Enemy";
        BattleControl.willDamage = "y";
        transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
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
    void randomBasicAttack()
    {
        m_attackedAlready = true;
        BattleControl.currentDamage = m_Damage;
        BattleControl.currentTarget = m_Target;
        transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
        StartCoroutine(returnEnemy());
    }

    void randomHeal()
    {
        m_attackedAlready = true;
        BattleControl.currentHealValue = m_HealValue;
        BattleControl.currentTarget = m_Target;
        transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
        StartCoroutine(returnEnemy());
    }

    IEnumerator returnEnemy()
    {
        EnemyAttack();
        yield return new WaitForSeconds(1.5f);
        SpeedTimer.isPaused = false;
		Invoke("SetAttack", 1.0f);
    }

    void decideState()
    {
        if (m_Health< m_Health/2) //would have a && healskill not on cool down later for eHeal
        {
            CurrentState = EnemyState.eDefending;
            Debug.Log("Enemy Defending");
        }
        else if (m_Health < m_Health/4)
        {
            CurrentState = EnemyState.eFleeing;
            Debug.Log("The enemy is fleeing");
        }
        else
        {
            CurrentState = EnemyState.eAttcking;
            decideTarget();
            Debug.Log("The enemy is going to attack" + m_Target);
        }
    }


    bool BattleWon;

    // Update is called once per frame
    void Update()
    {
        if (SpeedTimer.m_speedCounter % requiredSpeedForTurn == 0)
        {
            SpeedTimer.isPaused = true;

            Debug.Log("Enemies Turn");

            decideState();

            if (BattleControl.side == "Player")
            {
                CheckForDamage();
            }

            if (m_attackedAlready == false) //Stuff to be rearranged later for balanced
            {

                if (CurrentState == EnemyState.eFleeing)
                {
                   BattleWon = true;
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
            if (m_Health <= 0)
            {
                Debug.Log("The enemy is dead");
            }
        }


    }
}
