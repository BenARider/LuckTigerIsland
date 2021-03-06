﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class EnemEntity : Entity
{
    public enum Agression
    {
        eBackStabber,
        eRandomAttacker
    }
    public Agression AgressionState;

    /*public enum Class
    {
        nullEnemy,
        PigBoss, 
        Goblin,
        Farmer,
        Bandit,
        Knight,
        Wolf,
        Tiger,
        Troll,
        Birdman,
        Skeleton,
        Golem,
        Spirit,
        Warlock,
        ChickenBoss,
        TigerBoss, 
        PorkBoss,
        MinotaurSkeleton  
    }*/
    public EEnemies MyClass;

    /// <summary>
    /// reference from this
    /// </summary>
    public EnemyObject thisEnemyObject;
	//private bool canAttack = false; //used to prevent the ai from having too many turns. Only enabled on use of state transition.
	public int aggress; //likelihood to attack oppossing attacker (Between 1-20)
	public int intel; //likelihood to attack pm with high value (Between 1-20)
	public int XP; //amount of xp they give
    private GameObject m_AttackTarget;
    public TextMeshProUGUI attackDescriptionText;//describes the attack that is happening/happend;
    public TextMeshProUGUI turnText;//who's turn it is
    HandleTurns myAttack = new HandleTurns();
    protected void SetEnemyStats(int hth, int man, int str, int def, int magDef, int magPower, int spd, int lvl, int xp)
	{
		m_maxHealth = hth;
		m_maxMana = man;
		m_strength = str;
		m_defence = def;
        m_defenceMGC = magDef;
        m_magicPower = magPower;
		m_speed = spd;
		m_level = lvl;
		XP = xp;
	}
    private BattleControl BC;
    public HandleTurns HT;
    void Start()
    {
        switch (MyClass)
        {
            //maybe put stat initiations here but they're gonna be in the editor for the time being.
        }

        this.name = GetEntityNo() + ":" + this.name;

        SetRequiredSpeed();
        ResetHealth();
		ResetMana();
        Debug.Log("Enemy Values Set");
        currentState = TurnState.eProssesing; //Set the statemachine to the beggining state
        currentAffliction = Affliction.eNone;
        Health_Potion HpPotion = Health_Potion.CreateInstance<Health_Potion>();

        HealthPotions.Add(HpPotion);
        HealthPotions.Add(HpPotion);

        Mana_Potion MpPotion = Mana_Potion.CreateInstance<Mana_Potion>();

        ManaPotions.Add(MpPotion);
        ManaPotions.Add(MpPotion);
		transform.position = new Vector2(this.transform.position.x,this.transform.position.y - (GetEntityNo() - 1)); //orders the enemies by their entity number.
        BC = GameObject.Find("BattleControl").GetComponent<BattleControl>(); //makes BattleControl shortform to BC
        startPosition = transform.position; //setting the position based on where the object is on start up

        attackDescriptionText = GameObject.Find("Enemy_Attack_Description_Text").GetComponent<TextMeshProUGUI>();
        turnText = GameObject.Find("Enemy_Turn_Text").GetComponent<TextMeshProUGUI>();
        SetPreviousStats();//Keep this at the bottom
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
                CheckBuffs();
                rollsAttempted = 0;
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
                if (!isAlive && !countedDead)
                {
                    Debug.Log("DeadEnemies increased");
                    BC.deadEnemies++;
                    countedDead = true;
                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 90);
                    //BC.battleGoldReward += m_goldAward; //adds the bounty of the enemy to the gold pool for the end
                    //List<EEnemies> tempList = new List<EEnemies>
                    //{
                    //    thisEnemyObject.enemyType
                    //};
                    //EventManager.Instance.SetLastBattle(tempList);
                }
                else
                {
                    this.gameObject.tag = ("DeadPM");

                    isAlive = false;

                    for (int i = 0; i > BC.NextTurn.Count; i++)
                    {
                        if (BC.NextTurn[i].AttackingGameObject == this)
                        {
                            BC.NextTurn.Remove(BC.NextTurn[i]);
                        }
                    }

                    this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(105, 105, 105, 255);

                    isAlive = false;
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
                //turnText.text = "It is " + this.name + "'s turn";
                Debug.Log("It is " + this.name + "'s turn");
                StartCoroutine("FadeText");
            }
        }
	}

    int rollsAttempted = 0;
    int maxRolls = 10;

    void rollAttack()
    {
        rollsAttempted++;

        int num = Random.Range(0, attacks.Count);

        m_chosenAction = attacks[num];

        if (m_chosenAction.attackCost > m_mana && rollsAttempted < maxRolls)
        {
            rollAttack();
        }

        if(rollsAttempted >= maxRolls)
        {
            m_chosenAction = attacks.First(x => x.attackCost < m_mana);
        }
    }

    void ChooseAction()
    {
        if (AgressionState == Agression.eRandomAttacker)
        m_AttackTarget = BC.PartyMembersInBattle[Random.Range(0, BC.PartyMembersInBattle.Count)];
        {
            Debug.Log("Random");
        }

        if(AgressionState==Agression.eBackStabber)
        {
            BC.TargetingListForAI = BC.PartyMembersInBattle.OrderBy(x => x.GetComponent<PlayerEntity>().GetHealth()).ToList();

        }
        myAttack.Attacker = this.name; //Who is attacking

        myAttack.Type = "Enemy";//What type are they
        myAttack.AttackingGameObject = this.gameObject; //What gameObject is attacking
        myAttack.AttackTarget = m_AttackTarget; //Random a target that is in the List stored in BattleControl
        myAttack.chosenAttack = m_chosenAction;

        if (m_AttackTarget.GetComponent<PlayerEntity>().currentBuff != Buffs.eInvisable)
        {
            BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
            StartCoroutine("FadeText");
        }
    }

    private IEnumerator TimeForAction()
    {
        if (actionHappening) //stops spamming 
        {
            yield break;
        }

        actionHappening = true;

        Vector3 PartyMemberPosition = new Vector3(EntityToAttack.transform.position.x + 1.5f, EntityToAttack.transform.position.y, EntityToAttack.transform.position.z);

        while (MoveTo(PartyMemberPosition))
        {
            yield return null; //wait until moveToward is true
        }

        yield return new WaitForSeconds(1.5f);
        //do damage
        if (m_chosenAction.attackType == BaseAttack.AttackType.ePartyWide)
        {
            EnemyPartyWideDamage();
        }
        if (m_chosenAction.attackType == BaseAttack.AttackType.eMelee)
        {
            enemyDoDamge();
        }
		else if (m_chosenAction.attackType == BaseAttack.AttackType.eMagic)
		{
			enemyDoMagicDamage();
		}
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
	}

    void enemyDoDamge()
    {
        int calculateDamage = GetStrength() + BC.NextTurn[0].chosenAttack.attackDamage * (BC.NextTurn[0].chosenAttack.skillMultiplier);
		calculateDamage = calculateDamage - EntityToAttack.GetComponent<PlayerEntity>().GetDefence();
		if (calculateDamage < 5)
		{
			calculateDamage = 5;
		}
		m_displayDamage = calculateDamage;
        attackDescriptionText.text = this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + m_displayDamage + " damage!";
        StartCoroutine("FadeText");
        EntityToAttack.GetComponent<PlayerEntity>().TakeDamage(calculateDamage, m_chosenAction);
    }
	void enemyDoMagicDamage()
	{
		int calculateDamage = GetMagicPower() + BC.NextTurn[0].chosenAttack.attackDamage * (BC.NextTurn[0].chosenAttack.skillMultiplier);
		calculateDamage = calculateDamage - EntityToAttack.GetComponent<PlayerEntity>().GetMagicDefence();
		if (calculateDamage < 5)
		{
			calculateDamage = 5;
		}
		m_displayDamage = calculateDamage;
        attackDescriptionText.text = this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + m_displayDamage + " damage!";
        StartCoroutine("FadeText");
        EntityToAttack.GetComponent<PlayerEntity>().TakeDamage(calculateDamage, m_chosenAction);
	}
    void EnemyPartyWideDamage()
    {
		int tempDamage;
        int calculateDamage = GetStrength() + BC.NextTurn[0].chosenAttack.attackDamage;
        for(int i = 0; i < BC.PartyMembersInBattle.Count;i++)
        {
			tempDamage = calculateDamage - BC.PartyMembersInBattle[i].GetComponent<PlayerEntity>().GetDefence();
			if (tempDamage < 5)
			{
				tempDamage = 5;
			}
			m_displayDamage = tempDamage;
            attackDescriptionText.text = this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + m_displayDamage + " damage!";
            StartCoroutine("FadeText");
            BC.PartyMembersInBattle[i].GetComponent<PlayerEntity>().TakeDamage(calculateDamage, m_chosenAction);
        }
    }
    IEnumerator FadeText()
    {
        yield return new  WaitForSeconds(2.0f);
        attackDescriptionText.text = "";
        turnText.text = "";
    }
}
