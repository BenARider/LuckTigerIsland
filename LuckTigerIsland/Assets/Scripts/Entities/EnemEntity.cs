using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemEntity : Entity
{
	//private bool canAttack = false; //used to prevent the ai from having too many turns. Only enabled on use of state transition.
	public int aggress; //likelihood to attack oppossing attacker (Between 1-20)
	public int intel; //likelihood to attack pm with high value (Between 1-20)
	public int XP; //amount of xp they give
	private int m_Target; //used to determine where the skills will hit(Prone to change later on)
    [SerializeField]
    protected float currentSpeed = 0f;


    void SetEnemyStats(int hth, int man, int str, int def, int spd, int lvl, int agr, int itl, int xp)
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
        if (Class == "Ninja")
        {
            SetEnemyStats(75, 100, 10, 15, 75, 2, 15, 6, 50);
        }
        if (Class == "Cleric")
        {
            SetEnemyStats(50, 150, 5, 7, 60, 3, 5, 5, 50);
        }
        if (Class == "Archer")
        {
            SetEnemyStats(70, 125, 20, 10, 65, 2, 10, 8, 50);
        }
        //m_requiredSpeedForTurn = m_baseRequiredSpeedForTurn - GetSpeed();
        SetRequiredSpeed();
        ResetHealth();
		ResetMana();
        Debug.Log("Enemy Values Set");
        currentState = TurnState.eProssesing; //Set the statemachine to the beggining state
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
                ChooseAction(); //Do action
                currentState = TurnState.eWaiting; //move to waiting unil BC tells the entity to do the action
                break;
            case (TurnState.eWaiting):

                break;
            case (TurnState.eAction):
                StartCoroutine(TimeForAction()); //do the action stored before
                break;
            case (TurnState.eDead):
                Destroy(gameObject);
                break;
        }
        if (GetHealth() < 0)
        {
          currentState = TurnState.eDead;
        }
    }

    void UpdateSpeed()
    {
        //if (SpeedTimer.m_speedCounter % m_requiredSpeedForTurn == 0 && SpeedTimer.isPaused == false)
        //{
        //    SpeedTimer.isPaused = true;
        //    currentState = TurnState.eChooseAction;
        //}

        if (BattleControl.turnBeingHad == false)
        {
            currentSpeed = currentSpeed + 0.5f;
            if (currentSpeed >= GetRequiredSpeed())
            {
                currentState = TurnState.eChooseAction;
                BattleControl.turnBeingHad = true;
            }
        }
    }

    void ChooseAction()
    {
        HandleTurns myAttack = new HandleTurns
        {
            Attacker = this.name, //Who is attacking
            Type = "Enemy",//What type are they
            AttackingGameObject = this.gameObject, //What gameObject is attacking
            AttackTarget = BC.PartyMembersInBattle[Random.Range(0, BC.PartyMembersInBattle.Count)] //Random a target that is in the List stored in BattleControl
        };
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
}
