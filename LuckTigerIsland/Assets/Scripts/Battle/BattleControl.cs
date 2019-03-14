using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class BattleControl : MonoBehaviour {
	public static string side;
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
    public List<EnemEntity> Enemies = new List<EnemEntity>();
    public List<GameObject> PartyMembersInBattle = new List<GameObject>();

    // Use this for initialization
    void Start () {
        battleState = performAction.eWait;

        PartyMembersInBattle.AddRange (GameObject.FindGameObjectsWithTag("Party"));
        //prevents the rest of the setup phase until at least one enemy has been instantiated
        while (EnemiesInBattle.Count == 0)
        {
            EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        }

        EnemiesInBattle = EnemiesInBattle.OrderBy(x => x.GetComponent<EnemEntity>().GetEntityNo()).ToList();
        PartyMembersInBattle = PartyMembersInBattle.OrderBy(x => x.GetComponent<PlayerEntity>().GetEntityNo()).ToList();
		//allows the enemies to be targetted and guarantees this is called after they are instantiated
		for (int i = 0; i<EnemiesInBattle.Count; i++)
		{
			EnemiesInBattle[i].GetComponent<EnemEntity>().SetEntityNo(i+1);
		}
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