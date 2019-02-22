using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {

	public static string willDamage; //state n for no damage taken, state y for damage taken. Alternate state could indicate invulnerable or other in future.
	public static int currentDamage;
    public static int currentHealValue;
	public static int currentTarget;
	public static string side;
	public static int totalFighters = 8; //will be used to check when to reset the turn timer and stuff.
    [SerializeField]
    public static bool turnBeingHad;

    public enum performAction
    {
        eWait,
        eTakeAnAction,
        ePerformAction
    }
    public performAction battleState;

    public List<HandleTurns> NextTurn = new List<HandleTurns>();
    public List<GameObject> EnemiesInBattle = new List<GameObject>();
    public List<GameObject> PartyMembersInBattle = new List<GameObject>();

    // Use this for initialization
    void Start () {
        battleState = performAction.eWait;
        EnemiesInBattle.AddRange (GameObject.FindGameObjectsWithTag("Enemy"));
        PartyMembersInBattle.AddRange (GameObject.FindGameObjectsWithTag("Party"));
        willDamage = "n";
		currentDamage = 0;
        currentHealValue = 0;
		currentTarget = 0;
        turnBeingHad = false;
		Debug.Log("Setup complete");
	}
	void Update ()
	{
        switch (battleState)
        {
            case (performAction.eWait):
                if (NextTurn.Count > 0)
                {
                    battleState = performAction.eTakeAnAction;
                }
            break;
            case (performAction.eTakeAnAction):
                GameObject performer = GameObject.Find(NextTurn[0].Attacker);
                if (NextTurn[0].Type == "Enemy")
                {
                    EnemEntity EE = performer.GetComponent<EnemEntity>(); //grab the script from the attack
                    EE.EntityToAttack = NextTurn[0].AttackTarget; //setting the target in the enemy script
                    EE.currentState = EnemEntity.TurnState.eAction; //make the AI do their action script 
                }
                if (NextTurn[0].Type == "Player")
                {
                    PlayerEntity PE = performer.GetComponent<PlayerEntity>();
                    PE.EntityToAttack = NextTurn[0].AttackTarget;
                    PE.currentState = PlayerEntity.TurnState.eAction;
                }
                battleState = performAction.ePerformAction;
                break;
            case (performAction.ePerformAction):

            break;
        }


	}

    public void collectActions(HandleTurns Action)
    {

        NextTurn.Add(Action); 

    }
}