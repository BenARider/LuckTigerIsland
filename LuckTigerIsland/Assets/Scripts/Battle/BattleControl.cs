using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;
[System.Serializable]
public class BattleControl : MonoBehaviour {
	public static string side;
	public static int totalFighters = 8; //will be used to check when to reset the turn timer and stuff.
    [SerializeField]
    public static bool turnBeingHad;

    public GameObject gameOverPrefab;
    bool isGameOver = false;

    public enum performAction
    {
        eWait,
        eTakeAnAction,
        ePerformAction,
        eLoss,
        eWin
    }
    public performAction battleState;

    public List<HandleTurns> NextTurn = new List<HandleTurns>();
    public List<GameObject> EnemiesInBattle = new List<GameObject>();
    //public List<EnemEntity> Enemies = new List<EnemEntity>();
    public List<GameObject> PartyMembersInBattle = new List<GameObject>();
    public List<GameObject> TargetingListForAI = new List<GameObject>();
    public List<InventoryObject> rewards;
    public RewardUI rewardUI;
    [SerializeField]
    TextMeshProUGUI m_itemsAdded;
    EventSystem m_eventSystem;
    private bool m_itemRolled = false;
    public GameObject actionOne;
    public int battleGoldReward = 0;
    public int deadEnemies = 0;
    public int deadPlayers = 0;
    private int m_itemRoll;
    //private void Awake()
    //{
    //    PlayerManager.Instance.cleric = GameObject.Find("Player1").GetComponent<ClericStats>();
    //    PlayerManager.Instance.warrior = GameObject.Find("Player2").GetComponent<WarriorStats>();
    //    PlayerManager.Instance.wizard = GameObject.Find("Player3").GetComponent<WizardStats>();
    //    PlayerManager.Instance.ninja = GameObject.Find("Player4").GetComponent<NinjaStats>();
    //}


    // Use this for initialization
    void Start () {
        battleState = performAction.eWait;
        m_eventSystem = EventSystem.current;
        m_eventSystem.SetSelectedGameObject(actionOne);
        PartyMembersInBattle.AddRange (GameObject.FindGameObjectsWithTag("Party"));
        TargetingListForAI.AddRange(GameObject.FindGameObjectsWithTag("Party"));
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
        if(deadPlayers>=4)
        {
            battleState = performAction.eLoss;
        }
        if(deadEnemies>= EnemiesInBattle.Count)
        {
            battleState = performAction.eWin;
        }
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
                if (NextTurn[0].Type == "PartyBuff")
                {
                    PlayerEntity PE = performer.GetComponent<PlayerEntity>();
                    PE.EntityToAttack = NextTurn[0].AttackTarget;
                    PE.currentState = PlayerEntity.TurnState.eAction;
                }
                battleState = performAction.ePerformAction;
                break;
            case (performAction.ePerformAction):
                break;
            case (performAction.eWin):
              //  PlayerManager.Instance.transform.root.GetComponent<ReturnToMain>().Return();
              if (!m_itemRolled)
                {
                    //battleGoldReward = Random.Range(100, 150);
                    
                    //PlayerManager.Instance.AddXP(Random.Range(100, 150));
                    //m_itemRoll = Random.Range(0, rewards.Count()-1);
                    //
                    //
                    //Inventory.Instance.AddToInventory(rewards[m_itemRoll]);
                    //

                    //Debug.Log("Additem success" + rewards[m_itemRoll]);
                    //m_itemRolled = true;
                    //m_itemsAdded.text = "Items gained: " + "\n"+ rewards[m_itemRoll].objectName;

                    battleGoldReward = 0;
                    int battleExpReward = 0;
                    List<EEnemies> toSet = new List<EEnemies>();
                    for (int i=0; i< EnemiesInBattle.Count; ++i)
                    {
                
                       toSet.Add(EnemiesInBattle[i].GetComponent<EnemEntity>().MyClass);
                        battleExpReward += EnemiesInBattle[i].GetComponent<EnemEntity>().GetEXP();
                        Debug.Log("EXP Gained" + battleExpReward);

                        battleGoldReward += EnemiesInBattle[i].GetComponent<EnemEntity>().GetGold();
                        Debug.Log("Gold Gained" + battleGoldReward);
                    }
                    PlayerManager.Instance.AddXP(battleExpReward);
                    Inventory.Instance.IncreaseGold(battleGoldReward);
                    EventManager.Instance.SetLastBattle(toSet);
                    PlayerManager.Instance.transform.parent.GetComponent<ReturnToMain>().Return();
                    Debug.Log("Won");
                        //go to victory screen/overworld here
                }
                break;
            case (performAction.eLoss):
                //go to gameover screen here
                if (!isGameOver)
                {
                    GameObject go = Instantiate(gameOverPrefab, transform.parent);
                    m_eventSystem.SetSelectedGameObject(go.transform.GetChild(2).gameObject);
                    isGameOver = true;
                }
                break;
        }
    }
    public void collectActions(HandleTurns Action)
    {

        NextTurn.Add(Action); 

    }
}