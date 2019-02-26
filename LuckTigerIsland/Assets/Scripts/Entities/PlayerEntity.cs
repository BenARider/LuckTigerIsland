using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerEntity : Entity {

    private int m_playerIDStats = 0;
    public Text[] statTexts;
    public Slider expBar;
    public Button rightButton;
    public Button playerStatButton;
    public GameObject playerStatMenu;
    [SerializeField]
    private bool m_findTextGameObjects;
    [SerializeField]
    protected float currentSpeed = 0f;

    [SerializeField]
    PlayerEntity warrior;
    [SerializeField]
    PlayerEntity wizard;
    [SerializeField]
    PlayerEntity cleric;
    [SerializeField]
    PlayerEntity ninja;

    //Creates a new player entity with defined stats. Adding an object to one of the entities will apply those stats onto the object
    void SetPlayerStats(int _health, int _strength, int _defence, int _defenceMGC, int _speed, int _level, int _mana, int _magicPow, int _EXP)
    {
        m_maxHealth = _health;
        m_strength = _strength;
        m_defence = _defence;
        m_defenceMGC= _defenceMGC;
        m_speed = _speed;
        m_baseRequiredSpeedForTurn = 100;
        m_level = _level;
        m_maxMana = _mana;
        m_magicPower = _magicPow;
        m_EXP = _EXP;
    }

    private BattleControl BC;
    public HandleTurns HT;

    private bool m_chosenAction;

    void Start () {

        if (Class == "Warrior")
        {
            SetPlayerStats(150, 20, 20, 10, 60, 1, 20, 5, 40);
        }
        if (Class == "Wizard")
        {
            SetPlayerStats(100, 10, 5, 15, 45, 1, 50, 20, 50);
        }
        if (Class == "Cleric")
        {
            SetPlayerStats(125, 10, 10, 20, 70, 1, 50, 15, 75);
        }
        if (Class == "Ninja")
        {
            SetPlayerStats(100, 15, 10, 5, 40, 1, 35, 10, 30);
        }
        SetRequiredSpeed();
        ResetHealth();
        ResetMana();
        
        Debug.Log("Player Stats set");
        currentState = TurnState.eProssesing;
        statTexts = new Text[11];
        playerStatButton.onClick.AddListener(IncreasePlayerStatID);
        rightButton.onClick.AddListener(IncreasePlayerStatID);
        BC = GameObject.Find("BattleControl").GetComponent<BattleControl>();
        startPosition = transform.position; //setting the position based on where the object is on start up
        m_chosenAction = false;
    }

    // Update is called once per frame
    void Update ()
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            case (TurnState.eProssesing):
                UpdateSpeed();
                break;
            case (TurnState.eChooseAction):
                    ChooseAction();
                if (m_chosenAction)
                {
                    currentState = TurnState.eWaiting;
                }
                break;
            case (TurnState.eWaiting):

                break;
            case (TurnState.eAction):
                StartCoroutine(PlayerAction());
                break;
            case (TurnState.eDead):
                Destroy(gameObject);
                break;
        }
        if (GetHealth() <= 0)
        {
            currentState = TurnState.eDead;
        }
    }

    void UpdateSpeed()
    {
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
        Debug.Log("Player Choose Action");
            if (Input.GetKeyDown("1"))
            {
            int num = Random.Range(0, attacks.Count);

            HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    AttackTarget = BC.EnemiesInBattle[3], //Ignore the fact that this says three its always where enemy 1 is in the list ditto for the rest
                    chosenAttack = attacks[num]
                };
                BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                m_chosenAction = true;
            }
            if (Input.GetKeyDown("2"))
            {
            int num = Random.Range(0, attacks.Count);
            HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    AttackTarget = BC.EnemiesInBattle[1],
                    chosenAttack = attacks[num]

                };
                BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                m_chosenAction = true;

            }
            if (Input.GetKeyDown("3"))
            {
            int num = Random.Range(0, attacks.Count);
            HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    AttackTarget = BC.EnemiesInBattle[0] ,
                    chosenAttack = attacks[num]
                };
                BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                m_chosenAction = true;
            }
            if (Input.GetKeyDown("4"))
            {
            int num = Random.Range(0, attacks.Count);
            HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    AttackTarget = BC.EnemiesInBattle[2],
                    chosenAttack = attacks[num]
                };
                BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                m_chosenAction = true;
            }
    }

    private IEnumerator PlayerAction()
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
        playerDoDamge();

        while (MoveTo(startPosition))
        { 
            yield return null; //wait until moveToward is true
        }

        //remove from list
        BC.NextTurn.RemoveAt(0);

        //reset the statemachine
        BC.battleState = BattleControl.performAction.eWait;

        actionHappening = false;
        m_chosenAction = false; //Reset battle conditions
        currentSpeed = 0f;
        currentState = TurnState.eProssesing;
        BattleControl.turnBeingHad = false;
    }

    void playerDoDamge()
    {
        int calculateDamage = GetStrength() +  BC.NextTurn[0].chosenAttack.attackDamage; //calc should be done here before damage
         
        EntityToAttack.GetComponent<EnemEntity>().TakeDamage(calculateDamage);
    }

    //Used to control the party stat menu by setting and finding all the text values/objects
    public void IncreasePlayerStatID()
    {
        m_playerIDStats += 1;
        if (m_playerIDStats == 4)
        { 
            m_playerIDStats=0;
        }
       
        if (playerStatMenu.activeInHierarchy == true)
        {
            //Makes sure the objects only need to be found once
            if (m_findTextGameObjects == false)
            {
                statTexts[0] = GameObject.Find("Party_Member_Name").GetComponent<Text>();
                statTexts[1] = GameObject.Find("Class_Title").GetComponent<Text>();
                statTexts[2] = GameObject.Find("Health_Total").GetComponent<Text>();
                statTexts[3] = GameObject.Find("Physical_Damage_Total").GetComponent<Text>();
                statTexts[4] = GameObject.Find("Magical_Damage_Total").GetComponent<Text>();
                statTexts[5] = GameObject.Find("Physical_Armour_Total").GetComponent<Text>();
                statTexts[6] = GameObject.Find("Magical_Armour_Total").GetComponent<Text>();
                statTexts[7] = GameObject.Find("Critical_Hit_Chance_Total").GetComponent<Text>();
                statTexts[8] = GameObject.Find("Speed_Total").GetComponent<Text>();
                statTexts[9] = GameObject.Find("Level_Total").GetComponent<Text>();
                statTexts[10] = GameObject.Find("Skill_Points_Total").GetComponent<Text>();

                m_findTextGameObjects = true;
            }

            if (m_playerIDStats == 0)
            {
                
                statTexts[0].text = "Luck";
                statTexts[1].text = "Cleric";
                statTexts[2].text = "" + warrior.GetHealth();
                statTexts[3].text = "" + warrior.GetStrength();
                statTexts[4].text = "" + warrior.GetMagicPower();
                statTexts[5].text = "" + warrior.GetDefence();
                statTexts[6].text = "" + warrior.GetMagicDefence();
                statTexts[7].text = "" + 50;//crit chance
                statTexts[8].text = "" + warrior.GetSpeed();
                statTexts[9].text = "" + warrior.GetLevel();
                statTexts[10].text = "" + 10;//Skills points
                expBar.value = warrior.GetEXP();
                
            }
            if (m_playerIDStats == 1)
            {           
                statTexts[0].text = "Buck";
                statTexts[1].text = "Warrior";
                statTexts[2].text = "" + wizard.GetMaxHealth();
                statTexts[3].text = "" + wizard.GetStrength();
                statTexts[4].text = "" + wizard.GetMagicPower();
                statTexts[5].text = "" + wizard.GetDefence();
                statTexts[6].text = "" + wizard.GetMagicDefence();
                statTexts[7].text = "" + 50;//crit chance
                statTexts[8].text = "" + wizard.GetSpeed();
                statTexts[9].text = "" + wizard.GetLevel();
                statTexts[10].text = "" + 10;//Skills points
                expBar.value = wizard.GetEXP();


            }
            if (m_playerIDStats == 2)
            {
                statTexts[0].text = "Duck";
                statTexts[1].text = "Wizard";
                statTexts[2].text = "" + cleric.GetHealth();
                statTexts[3].text = "" + cleric.GetStrength();
                statTexts[4].text = "" + cleric.GetMagicPower();
                statTexts[5].text = "" + cleric.GetDefence();
                statTexts[6].text = "" + cleric.GetMagicDefence();
                statTexts[7].text = "" + 50;//crit chance
                statTexts[8].text = "" + cleric.GetSpeed();
                statTexts[9].text = "" + cleric.GetLevel();
                statTexts[10].text = "" + 10;//Skills points
                expBar.value = cleric.GetEXP();

            }
            if (m_playerIDStats == 3)
            {
                statTexts[0].text = "Phil";
                statTexts[1].text = "Ninja";
                statTexts[2].text = "" + ninja.GetHealth();
                statTexts[3].text = "" + ninja.GetStrength();
                statTexts[4].text = "" + ninja.GetMagicPower();
                statTexts[5].text = "" + ninja.GetDefence();
                statTexts[6].text = "" + ninja.GetMagicDefence();
                statTexts[7].text = "" + 50;//crit chance
                statTexts[8].text = "" + ninja.GetSpeed();
                statTexts[9].text = "" + ninja.GetLevel();
                statTexts[10].text = "" + 10;//Skills points
                expBar.value = ninja.GetEXP();

            }
        }
        if (playerStatMenu.activeInHierarchy == false)
        {
            m_findTextGameObjects = false;
        }
    }
}
