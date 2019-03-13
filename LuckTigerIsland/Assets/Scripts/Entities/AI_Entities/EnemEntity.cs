﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemEntity : Entity
{
	//private bool canAttack = false; //used to prevent the ai from having too many turns. Only enabled on use of state transition.
	public int aggress; //likelihood to attack oppossing attacker (Between 1-20)
	public int intel; //likelihood to attack pm with high value (Between 1-20)
	public int XP; //amount of xp they give
    public TextMeshProUGUI attackDescriptionText;//describes the attack that is happening/happend;
    public TextMeshProUGUI turnText;//who's turn it is

    protected void SetEnemyStats(int hth, int man, int str, int def, int spd, int lvl, int agr, int itl, int xp)
	{
		m_maxHealth = hth;
		m_maxMana = man;
		m_strength = str;
		m_defence = def;
		m_speed = spd;
		m_level = lvl;
		aggress = agr;
		intel = itl;
		XP = xp;
	}
	public EnemEntity Tiger;

    private BattleControl BC;
    public HandleTurns HT;


    // Use this for initialization
    void Start()
    {
        if (Class == "Goblin")
        {
            SetEnemyStats(150, 50, 40, 20, 50, 3, 20, 4, 50);
        }
        if (Class == "Wizard")
        {
            SetEnemyStats(70, 125, 20, 10, 65, 2, 10, 8, 50);
        }
        if (Class == "Ninja")
        {
            SetEnemyStats(75, 100, 10, 15, 75, 2, 15, 6, 50);
        }
        if (Class == "Cleric")
        {
            SetEnemyStats(50, 150, 5, 7, 60, 3, 5, 5, 50);
        }

        //m_requiredSpeedForTurn = m_baseRequiredSpeedForTurn - GetSpeed();
        SetRequiredSpeed();
        ResetHealth();
		ResetMana();
        Debug.Log("Enemy Values Set");
        currentState = TurnState.eProssesing; //Set the statemachine to the beggining state
        Health_Potion HpPotion = Health_Potion.CreateInstance<Health_Potion>();

        HealthPotions.Add(HpPotion);
        HealthPotions.Add(HpPotion);

        Mana_Potion MpPotion = Mana_Potion.CreateInstance<Mana_Potion>();

        ManaPotions.Add(MpPotion);
        ManaPotions.Add(MpPotion);
        BC = GameObject.Find("BattleControl").GetComponent<BattleControl>(); //makes BattleControl shortform to BC
        startPosition = transform.position; //setting the position based on where the object is on start up
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);
        switch (currentState)
        {
            case (TurnState.eProssesing):
                UpdateSpeed(); //Speed check
                break;
            case (TurnState.eChooseAction):
                rollAttack();
                ChooseAction(); //Do action
                currentState = TurnState.eWaiting; //move to waiting unil BC tells the entity to do the action
                break;
            case (TurnState.eWaiting):

                break;
            case (TurnState.eAction):
                StartCoroutine(TimeForAction()); //do the action stored before
                break;
            case (TurnState.eDead):
                if (!m_alive)
                {
                    return;
                }
                else
                {
                    BC.EnemiesInBattle.Remove(this.gameObject);

                    this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(105, 105, 105, 255);

                    m_alive = false;
                }
                break;
        }
    }

    void UpdateSpeed()
    {
        if (BattleControl.turnBeingHad == false)
        {
            currentSpeed = currentSpeed + 0.25f;
            if (currentSpeed >= GetRequiredSpeed())
            {
                currentState = TurnState.eChooseAction;
                BattleControl.turnBeingHad = true;
                turnText.text = "It is " + this.name + "'s turn";
                Debug.Log("It is " + this.name + "'s turn");
                StartCoroutine("FadeText");
            }
        }
	}

    void rollAttack()
    {
        int num = Random.Range(0, attacks.Count);

        m_chosenAction = attacks[num];

        if (m_chosenAction.attackCost > m_mana)
        {
            rollAttack();
        }
        Debug.Log(this.name + " has chosen the " + m_chosenAction + " attack");

    }

    void ChooseAction()
    {

        HandleTurns myAttack = new HandleTurns
        {
            Attacker = this.name, //Who is attacking
            Type = "Enemy",//What type are they
            AttackingGameObject = this.gameObject, //What gameObject is attacking
            AttackTarget = BC.PartyMembersInBattle[Random.Range(0, BC.PartyMembersInBattle.Count)], //Random a target that is in the List stored in BattleControl
            chosenAttack = m_chosenAction
        };
        attackDescriptionText.text = this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " damage!";
        Debug.Log(this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " damage!");
        StartCoroutine("FadeText");
        BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
    }

    private IEnumerator TimeForAction()
    {
        if (actionHappening) //stops spamming 
        {
            yield break;
        }

        actionHappening = true;

        Vector3 PartyMemberPosition = new Vector3(EntityToAttack.transform.position.x - 1.5f, EntityToAttack.transform.position.y, EntityToAttack.transform.position.z);

        while (MoveTo(PartyMemberPosition))
        {
            yield return null; //wait until moveToward is true
        }

        yield return new WaitForSeconds(1.5f);
        //do damage
        enemyDoDamge();

        while (MoveTo(startPosition))
        {
            yield return null; //wait until moveToward is true
        }

        //remove from list
        BC.NextTurn.RemoveAt(0);

        //reset the statemachine
        BC.battleState = BattleControl.performAction.eWait;

        actionHappening = false;
        currentSpeed = 0f;
        currentState = TurnState.eProssesing;
        BattleControl.turnBeingHad = false;
    }

	void EnemyAttack()
	{
		Debug.Log("Enemy attacking");
		BattleControl.side = "Enemy";
		BattleControl.willDamage = "y";
	}

    void enemyDoDamge()
    {
        int calculateDamage = GetStrength() + BC.NextTurn[0].chosenAttack.attackDamage;
        EntityToAttack.GetComponent<PlayerEntity>().TakeDamage(calculateDamage);
    }
    IEnumerator FadeText()
    {
        yield return new  WaitForSeconds(2.0f);
        attackDescriptionText.text = "";
        turnText.text = "";
    }
}