using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
[System.Serializable]
public class PlayerEntity : Entity
{
    public TextMeshProUGUI[] skillTexts;
    public TextMeshProUGUI turnText;//who's turn it is
    public TextMeshProUGUI attackDescriptionText;//describes the attack that is happening/happend
    public TextMeshProUGUI notEnoughManaText;
    public TextMeshProUGUI notEnoughPotionsText;
    public TextMeshProUGUI usedPotionText;
    public TextMeshProUGUI skillDescription;
    public GameObject enemyTargets;
    public GameObject teamTargets;
    public Button enemyTarget;
    public Button teamTarget;
    public EquipEntity m_equipEntity;
    EventSystem m_eventSystem;

    [SerializeField]
    PlayerEntity warrior;
    [SerializeField]
    PlayerEntity wizard;
    [SerializeField]
    PlayerEntity cleric;
    [SerializeField]
    PlayerEntity ninja;

    public enum Class
    {
        eWarrior,
        eCleric,
        eWizard,
        eNinja
    }
    public Class MyClass;
    //Creates a new player entity with defined stats. Adding an object to one of the entities will apply those stats onto the object
    private BattleControl BC;
    private BattleUIButton m_BattleButton;
    public HandleTurns HT;
	public PlayerManager PM;

    public List<BasePassive> passiveList = new List<BasePassive>();

    public bool m_hasChosenAction;
    public bool m_chosenTarget;

    void Start()
    {

        if (MyClass == Class.eWarrior)
        {
			m_maxHealth = PM.warrior.GetMaxHealth();
			m_maxMana = PM.warrior.GetMaxMana();
			m_strength = PM.warrior.GetStrength();
			m_magicPower = PM.warrior.GetMagicPower();
			m_defence = PM.warrior.GetDefence();
			m_defenceMGC = PM.warrior.GetMagicDefence();
			m_speed = PM.warrior.GetSpeed();
			Debug.Log("Stats Applied");
        }
        if (MyClass == Class.eWizard)
        {
			m_maxHealth = PM.wizard.GetMaxHealth();
			m_maxMana = PM.wizard.GetMaxMana();
			m_strength = PM.wizard.GetStrength();
			m_magicPower = PM.wizard.GetMagicPower();
			m_defence = PM.wizard.GetDefence();
			m_defenceMGC = PM.wizard.GetMagicDefence();
			m_speed = PM.wizard.GetSpeed();
			Debug.Log("Stats Applied Wizard");
		}
        if (MyClass == Class.eCleric)
        {
			m_maxHealth = PM.cleric.GetMaxHealth();
			m_maxMana = PM.cleric.GetMaxMana();
			m_strength = PM.cleric.GetStrength();
			m_magicPower = PM.cleric.GetMagicPower();
			m_defence = PM.cleric.GetDefence();
			m_defenceMGC = PM.cleric.GetMagicDefence();
			m_speed = PM.cleric.GetSpeed();
			Debug.Log("Stats Applied Cleric");
		}
        if (MyClass == Class.eNinja)
        {
			m_maxHealth = PM.ninja.GetMaxHealth();
			m_maxMana = PM.ninja.GetMaxMana();
			m_strength = PM.ninja.GetStrength();
			m_magicPower = PM.ninja.GetMagicPower();
			m_defence = PM.ninja.GetDefence();
			m_defenceMGC = PM.ninja.GetMagicDefence();
			m_speed = PM.ninja.GetSpeed();
			Debug.Log("Stats Applied Ninja");
		}
        SetRequiredSpeed();
        ResetHealth();
        ResetMana();

        Debug.Log("Player Stats set");

        ///used to test the afflictions, currently procs every second and will stop after 20. Can be changed later though
        //m_afflicted = true;
        //currentAffliction = Affliction.eOnFire;


        Health_Potion HpPotion = Health_Potion.CreateInstance<Health_Potion>();

        HealthPotions.Add(HpPotion);
        HealthPotions.Add(HpPotion);

        Mana_Potion MpPotion = Mana_Potion.CreateInstance<Mana_Potion>();

        ManaPotions.Add(MpPotion);
        ManaPotions.Add(MpPotion);

        skillTexts = new TextMeshProUGUI[7];


        skillTexts[0] = GameObject.Find("Action_One").GetComponent<TextMeshProUGUI>();
        skillTexts[1] = GameObject.Find("Action_Two").GetComponent<TextMeshProUGUI>();
        skillTexts[2] = GameObject.Find("Action_Three").GetComponent<TextMeshProUGUI>();
        skillTexts[3] = GameObject.Find("Action_Four").GetComponent<TextMeshProUGUI>();
        skillTexts[4] = GameObject.Find("Action_Five").GetComponent<TextMeshProUGUI>();
        skillTexts[5] = GameObject.Find("Action_Six").GetComponent<TextMeshProUGUI>();
        skillTexts[6] = GameObject.Find("Action_Seven").GetComponent<TextMeshProUGUI>();
        BC = GameObject.Find("BattleControl").GetComponent<BattleControl>();

        startPosition = transform.position; //setting the position based on where the object is on start up
        m_hasChosenAction = false;
        m_BattleButton = GameObject.Find("Action_List_Holder").GetComponent<BattleUIButton>();


        turnText = GameObject.Find("Player_Turn_Text").GetComponent<TextMeshProUGUI>();
        attackDescriptionText = GameObject.Find("Player_Attack_Description_Text").GetComponent<TextMeshProUGUI>();
        notEnoughManaText = GameObject.Find("not_Enough_Mana_Text").GetComponent<TextMeshProUGUI>();
        notEnoughPotionsText = GameObject.Find("not_Enough_Potions_Text").GetComponent<TextMeshProUGUI>();
        usedPotionText = GameObject.Find("used_Potion_Text").GetComponent<TextMeshProUGUI>();
        m_eventSystem = EventSystem.current;
        currentState = TurnState.eProssesing;

        foreach (var passive in passiveList)
        {
            ApplyBasePassive(passive);
        }

        foreach (var passive in passiveActiveList)
        {

            if(!passive.passiveActive)
            {
                if (passive.lessThanOrMoreThan == BaseActivePassive.LessThanOrMoreThan.eLessThan)
                    ApplyPassives(passive, PassiveConditionLessthan);
                else
                    ApplyPassives(passive, PassiveConditionGreaterthan);
            }
        }
        SetPreviousStats(); //Keep this at the bottom
    }


    public bool PassiveConditionLessthan(BaseActivePassive target)
    {
        switch (target.passiveConditionType)
        {
            case BaseActivePassive.PassiveCondition.eAll:
                return (m_health <= target.passiveCondtitionAmount) && (m_strength <= target.passiveCondtitionAmount) && (m_mana <= target.passiveCondtitionAmount);
            case BaseActivePassive.PassiveCondition.eStrength:
                return (m_strength <= target.passiveCondtitionAmount);
            case BaseActivePassive.PassiveCondition.eHealth:
                return (m_health <= target.passiveCondtitionAmount);
            case BaseActivePassive.PassiveCondition.eMagic:
                return (m_mana <= target.passiveCondtitionAmount);
        }
        return false;
    }

    public bool PassiveConditionGreaterthan(BaseActivePassive target)
    {
        switch (target.passiveConditionType)
        {
            case BaseActivePassive.PassiveCondition.eAll:
                return (m_health >= target.passiveCondtitionAmount) && (m_strength >= target.passiveCondtitionAmount) && (m_mana >= target.passiveCondtitionAmount);
            case BaseActivePassive.PassiveCondition.eStrength:
                return (m_strength >= target.passiveCondtitionAmount);
            case BaseActivePassive.PassiveCondition.eHealth:
                return (m_health >= target.passiveCondtitionAmount);
            case BaseActivePassive.PassiveCondition.eMagic:
                return (m_mana >= target.passiveCondtitionAmount);
        }
        return false;
    }
    
    public delegate bool Check(BaseActivePassive target);

    void ApplyPassives(BaseActivePassive passive, Check check)
    {
        if (check(passive))
        {
            ApplyBasePassive(passive);
        }
    }

    void ApplyBasePassive(BasePassive passive)
    {
        switch (passive.passiveType)
        {
            case (BaseActivePassive.PassiveType.eAll):
                m_health += passive.passiveAmount;
                m_strength += passive.passiveAmount;
                m_mana += passive.passiveAmount;
                passive.passiveActive = true;
                break;
            case (BaseActivePassive.PassiveType.eHealth):
                m_health += passive.passiveAmount;
                passive.passiveActive = true;
                break;
            case (BaseActivePassive.PassiveType.eStrength):
                m_strength += passive.passiveAmount;
                passive.passiveActive = true;
                break;
            case (BaseActivePassive.PassiveType.eMagic):
                m_mana += passive.passiveAmount;
                passive.passiveActive = true;
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        foreach (var passive in passiveActiveList)
        {

            if (!passive.passiveActive)
            {
                if (passive.lessThanOrMoreThan == BaseActivePassive.LessThanOrMoreThan.eLessThan)
                    ApplyPassives(passive, PassiveConditionLessthan);
                else
                    ApplyPassives(passive, PassiveConditionGreaterthan);
            }
        }
        switch (currentState)
        {
            case (TurnState.eProssesing):
                UpdateSpeed();
                break;
            case (TurnState.eChooseAction):
                CheckBuffs(); //used to purge buffs such as block
                ChooseAction();

                if (m_hasChosenAction)
                {
                    currentState = TurnState.eChooseTarget;
                }

                break;
            case (TurnState.eChooseTarget):

                ChooseTarget();
                if (m_chosenTarget)
                {
                    Debug.Log("switching to waiting");

                    currentState = TurnState.eWaiting;
                }
                break;
            case (TurnState.eAction):
                StartCoroutine(PlayerAction());
                break;
            case (TurnState.eDead):
                if (!isAlive && !countedDead)
                {
                    Debug.Log("DeadPlayers increased");
                    BC.PartyMembersInBattle.Remove(this.gameObject);
                    BC.deadPlayers++;
                    countedDead = true;
                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 90);
                    return;
                }
                else
                {
                    this.gameObject.tag = ("DeadPM");

                    BC.PartyMembersInBattle.Remove(this.gameObject);

                    this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(105, 105, 105, 255);

                    isAlive = false;
                }
                break;
        }
    }

    void UpdateSpeed()
    {
        if (BattleControl.turnBeingHad == false && currentAffliction != Affliction.eStunned)
        {
            currentSpeed = currentSpeed + 0.25f;
            if (currentSpeed >= GetRequiredSpeed())
            {
                currentState = TurnState.eChooseAction;
                BattleControl.turnBeingHad = true;
                turnText.text = "It is " + this.name + "'s turn";
                StartCoroutine(FadeText());
            }
           
        }

    }

    void ChooseAction()
    {
      skillTexts[0].text = "1: " + attacks[0].attackName;
      skillTexts[1].text = "2: " + attacks[1].attackName;
      skillTexts[2].text = "3: " + attacks[2].attackName;
      skillTexts[3].text = "4: " + attacks[3].attackName;
      skillTexts[4].text = "5: " + attacks[4].attackName;
      skillTexts[5].text = "6: " + attacks[5].attackName;
      skillTexts[6].text = "7: " + attacks[6].attackName;
        if (m_eventSystem.currentSelectedGameObject == GameObject.Find("Action_One"))
        {
            skillDescription.text = "Mana Cost: " + attacks[0].attackCost + " Type: " + attacks[0].attackType + "\n" + attacks[0].attackAffliction + " Damage: " +attacks[0].attackDamage + "\n" + "Description: " + attacks[0].attackDescription;
        }
        if (m_eventSystem.currentSelectedGameObject == GameObject.Find("Action_Two"))
        {
            skillDescription.text = "Mana Cost: " + attacks[1].attackCost + " Type: " + attacks[0].attackType + "\n" + attacks[0].attackAffliction + " Damage: " + attacks[1].attackDamage + "\n" + "Description: " + attacks[1].attackDescription;
        }
        if (m_eventSystem.currentSelectedGameObject == GameObject.Find("Action_Three"))
        {
            skillDescription.text = "Mana Cost: " + attacks[2].attackCost + " Type: " + attacks[0].attackType + "\n" + attacks[0].attackAffliction +" Damage: " + attacks[2].attackDamage + "\n" + "Description: " + attacks[2].attackDescription;
        }
        if (m_eventSystem.currentSelectedGameObject == GameObject.Find("Action_Four"))
        {
            skillDescription.text = "Mana Cost: " + attacks[3].attackCost + " Type: " + attacks[0].attackType + "\n" + attacks[0].attackAffliction + " Damage: " + attacks[3].attackDamage + "\n" + "Description: " + attacks[3].attackDescription;
        }
        if (m_eventSystem.currentSelectedGameObject == GameObject.Find("Action_Five"))
        {
            skillDescription.text = "Mana Cost: " + attacks[4].attackCost + " Type: " + attacks[0].attackType + "\n" + attacks[0].attackAffliction +" Damage: " + attacks[4].attackDamage + "\n" + "Description: " + attacks[4].attackDescription;
        }
        if (m_eventSystem.currentSelectedGameObject == GameObject.Find("Action_Six"))
        {
            skillDescription.text = "Mana Cost: " + attacks[5].attackCost + " Type: " + attacks[0].attackType + "\n" + attacks[0].attackAffliction + " Damage: " + attacks[5].attackDamage + "\n" + "Description: " + attacks[5].attackDescription;
        }
        if (m_eventSystem.currentSelectedGameObject == GameObject.Find("Action_Seven"))
        {
            skillDescription.text = "Mana Cost: " + attacks[6].attackCost + " Type: " + attacks[0].attackType + "\n" + attacks[0].attackAffliction +" Damage: " + attacks[0].attackDamage + "\n" + "Description: " + attacks[6].attackDescription;
        }
        if (m_eventSystem.currentSelectedGameObject == GameObject.Find("Action_Eight"))
        {
            skillDescription.text = "Restores: " + Health_Potion.healthGiven + " Health";
        }
        if (m_eventSystem.currentSelectedGameObject == GameObject.Find("Action_Nine"))
        {
            skillDescription.text = "Restores: " + Mana_Potion.manaGiven + " Mana";
        }

        Debug.Log(this.name + ": Choose Action");
        if (Input.GetKeyDown("1") || m_BattleButton.GetActionTargetNumber() == 1)
        {

            if (attacks[0].attackType == BaseAttack.AttackType.eBuff)
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[0].attackType != BaseAttack.AttackType.eBuff)
            {
                enemyTarget.Select();
                teamTargets.SetActive(false);
                enemyTargets.SetActive(true);
            }

            m_chosenAction = attacks[0];

            if (attacks[0].attackCost < m_mana)
            {
                m_hasChosenAction = true;
                m_mana -= m_chosenAction.attackCost;
            }
            else
            {
                notEnoughManaText.text = this.name + " does not have enough mana!";
                Debug.Log(this.name + " does not have enough mana!");
            }
            StartCoroutine("FadeText");
        }
        if (Input.GetKeyDown("2") || m_BattleButton.GetActionTargetNumber() == 2)
        {
            m_chosenAction = attacks[1];

            if (attacks[1].attackType == BaseAttack.AttackType.eBuff)
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[1].attackType != BaseAttack.AttackType.eBuff)
            {
                enemyTarget.Select();
                teamTargets.SetActive(false);
                enemyTargets.SetActive(true);
            }
            if (attacks[1].attackCost < m_mana)
            {
                m_hasChosenAction = true;
                m_mana -= m_chosenAction.attackCost;
            }
            else
            {
                notEnoughManaText.text = this.name + " does not have enough mana!";
                Debug.Log(this.name + " does not have enough mana!");
            }
            
            StartCoroutine("FadeText");
        }
        if (Input.GetKeyDown("3") || m_BattleButton.GetActionTargetNumber() == 3)
        {
            if (attacks[2].attackType == BaseAttack.AttackType.eBuff)
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[2].attackType != BaseAttack.AttackType.eBuff)
            {
                enemyTarget.Select();
                teamTargets.SetActive(false);
                enemyTargets.SetActive(true);
            }
            m_chosenAction = attacks[2];
            if (attacks[2].attackCost < m_mana)
            {
             
                m_hasChosenAction = true;
                m_mana -= m_chosenAction.attackCost;
            }
            else
            {
                notEnoughManaText.text = this.name + " does not have enough mana!";
                Debug.Log(this.name + " does not have enough mana!");
            }
           
            StartCoroutine("FadeText");
        }
        if (Input.GetKeyDown("4") || m_BattleButton.GetActionTargetNumber() == 4)
        {
            if (attacks[3].attackType == BaseAttack.AttackType.eBuff)
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[3].attackType != BaseAttack.AttackType.eBuff)
            {
                enemyTarget.Select();
                teamTargets.SetActive(false);
                enemyTargets.SetActive(true);
            }
            m_chosenAction = attacks[3];

            if (attacks[3].attackCost < m_mana)
            {
                m_hasChosenAction = true;
                m_mana -= m_chosenAction.attackCost;
            }
            else
            {
                notEnoughManaText.text = this.name + " does not have enough mana!";
                Debug.Log(this.name + " does not have enough mana!");
            }
            
            StartCoroutine("FadeText");
        }
        if (Input.GetKeyDown("5") || m_BattleButton.GetActionTargetNumber() == 5)
        {

            if (attacks[4].attackType == BaseAttack.AttackType.eBuff)
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[4].attackType != BaseAttack.AttackType.eBuff)
            {
                enemyTarget.Select();
                teamTargets.SetActive(false);
                enemyTargets.SetActive(true);
            }
            m_chosenAction = attacks[4];

            if (attacks[4].attackCost < m_mana)
            {
                m_hasChosenAction = true;
                m_mana -= m_chosenAction.attackCost;
            }
            else
            {
                notEnoughManaText.text = this.name + " does not have enough mana!";
                Debug.Log(this.name + " does not have enough mana!");
            }
            
            StartCoroutine("FadeText");
        }
        if (Input.GetKeyDown("6") || m_BattleButton.GetActionTargetNumber() == 6)
        {

            if (attacks[5].attackType == BaseAttack.AttackType.eBuff)
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[5].attackType != BaseAttack.AttackType.eBuff)
            {
                enemyTarget.Select();
                teamTargets.SetActive(false);
                enemyTargets.SetActive(true);
            }

            m_chosenAction = attacks[5];

            if (attacks[5].attackCost < m_mana)
            {
                m_hasChosenAction = true;
                m_mana -= m_chosenAction.attackCost;
            }
            else
            {
                notEnoughManaText.text = this.name + " does not have enough mana!";
                Debug.Log(this.name + " does not have enough mana!");
            }
            
            StartCoroutine("FadeText");
        }
        if (Input.GetKeyDown("7") || m_BattleButton.GetActionTargetNumber() == 7)
        {

                if (attacks[6].attackType == BaseAttack.AttackType.eBuff)
                {
                teamTarget.Select();
                teamTargets.SetActive(true);
                    enemyTargets.SetActive(false);
                }
                if (attacks[6].attackType != BaseAttack.AttackType.eBuff)
                {
                enemyTarget.Select();
                teamTargets.SetActive(false);
                    enemyTargets.SetActive(true);
                }

            
            m_chosenAction = attacks[6];

            if (attacks[6].attackCost < m_mana)
            {
                m_hasChosenAction = true;
                m_mana -= m_chosenAction.attackCost;
            }
            else
            {
                notEnoughManaText.text = this.name + " does not have enough mana!";
                Debug.Log(this.name + " does not have enough mana!");
            }
            
            StartCoroutine("FadeText");
        }
        if (Input.GetKeyDown("8") || m_BattleButton.GetActionTargetNumber() == 8)
        {
           
            if (HealthPotions.Count > 0)
            {
                m_health += Health_Potion.healthGiven;
                if (m_health > m_maxHealth)
                {
                    m_health = m_maxHealth;
                    usedPotionText.text = this.name + " used a health potion";
                    Debug.Log(this.name + " used a health potion");
                    HealthPotions.RemoveAt(0);
                }
                Debug.Log("Resetting speed");
                currentSpeed = 0;
                m_BattleButton.ResetTargetActionNumber();
                currentState = TurnState.eProssesing;
                BattleControl.turnBeingHad = false;
            }
            else
            {
                notEnoughPotionsText.text = this.name + "does not have enough health potions";
                Debug.Log(this.name + " does not have enough health potions");
            }
            StartCoroutine("FadeText");
        }

        if (Input.GetKeyDown("9") || m_BattleButton.GetActionTargetNumber() == 9)
        {
            for (int i = 0; i < attacks.Count; ++i)
            {
                if (attacks[i].attackType == BaseAttack.AttackType.eBuff)
                {
                    teamTargets.SetActive(true);
                    enemyTargets.SetActive(false);
                }
                if (attacks[i].attackType != BaseAttack.AttackType.eBuff)
                {
                    teamTargets.SetActive(false);
                    enemyTargets.SetActive(true);
                }

            }
            if (ManaPotions.Count > 0)
            {
                m_mana += Mana_Potion.manaGiven;
                if (m_mana > m_maxMana)
                {
                    m_mana = m_maxMana;
                    usedPotionText.text = this.name + " used a mana potion";
                    Debug.Log(this.name + " used a mana potion");

                    ManaPotions.RemoveAt(0);
                }
                currentSpeed = 0;
                m_BattleButton.ResetTargetActionNumber();
                currentState = TurnState.eProssesing;
                BattleControl.turnBeingHad = false;
            }
            else
            {
                
                notEnoughPotionsText.text = this.name + "does not have enough mana potions";
                Debug.Log(this.name + " does not have enough mana potions");
            }
            StartCoroutine("FadeText");
        }
    }

    void ChooseTarget()
    {
        ///Targetting enemies
        Debug.Log(this.name + ": Choose Target");
        if (m_chosenAction.attackType != BaseAttack.AttackType.eBuff)
        {
            if (Input.GetKeyDown("1") || m_BattleButton.GetActionTargetNumber() == 10)
            {
                HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    chosenAttack = m_chosenAction,
                    AttackTarget = BC.EnemiesInBattle[0]

                };
                if (BC.EnemiesInBattle[0].GetComponent<EnemEntity>().isAlive)
                {
                    BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                    m_chosenTarget = true;
                    attackDescriptionText.text = this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " damage!";
                    Debug.Log(this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " damage!");
                    StartCoroutine("FadeText");
                }
                else
                {
                    Debug.Log("That enemy is dead choose another");
                }
            }
            if (Input.GetKeyDown("2") || m_BattleButton.GetActionTargetNumber() == 11)
            {
                HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    chosenAttack = m_chosenAction,
                    AttackTarget = BC.EnemiesInBattle[1]
                };
                if (BC.EnemiesInBattle[1].GetComponent<EnemEntity>().isAlive)
                {
                    BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                    m_chosenTarget = true;
                    attackDescriptionText.text = this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " damage!";
                    Debug.Log(this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " damage!");
                    StartCoroutine("FadeText");
                }
                else
                {
                    Debug.Log("That enemy is dead choose another");
                }
            }
            if (Input.GetKeyDown("3") || m_BattleButton.GetActionTargetNumber() == 12)
            {
                HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    chosenAttack = m_chosenAction,
                    AttackTarget = BC.EnemiesInBattle[2]
                };
                if (BC.EnemiesInBattle[2].GetComponent<EnemEntity>().isAlive)
                {
                    BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                    m_chosenTarget = true;
                    attackDescriptionText.text = this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " damage!";
                    Debug.Log(this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " damage!");
                    StartCoroutine("FadeText");
                }
                else
                {
                    Debug.Log("That enemy is dead choose another");
                }
            }
            if (Input.GetKeyDown("4") || m_BattleButton.GetActionTargetNumber() == 13)
            {
                HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    chosenAttack = m_chosenAction,
                    AttackTarget = BC.EnemiesInBattle[3]
                };
                if (BC.EnemiesInBattle[3].GetComponent<EnemEntity>().isAlive)
                {
                    BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                    m_chosenTarget = true;
                    attackDescriptionText.text = this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " damage!";
                    Debug.Log(this.gameObject.name + " Is going to attack " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " damage!");
                    StartCoroutine("FadeText");
                }
                else
                {
                    Debug.Log("That enemy is dead choose another");
                }
            }
        }
        ///ally targetting
        else if (m_chosenAction.attackType == BaseAttack.AttackType.eBuff)
        {
            if (Input.GetKeyDown("1") || m_BattleButton.GetActionTargetNumber() == 10)
            {
                HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    chosenAttack = m_chosenAction,
                    AttackTarget = BC.PartyMembersInBattle[0]

                };
                if (BC.PartyMembersInBattle[0].GetComponent<PlayerEntity>().isAlive)
                {
                    BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                    m_chosenTarget = true;
                    attackDescriptionText.text = this.gameObject.name + " Is going to help " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName;
                    StartCoroutine("FadeText");
                }
                else
                {
                    Debug.Log("That player is dead choose another");
                }
            }
            if (Input.GetKeyDown("2") || m_BattleButton.GetActionTargetNumber() == 11)
            {
                HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    chosenAttack = m_chosenAction,
                    AttackTarget = BC.PartyMembersInBattle[1]
                };
                if (BC.PartyMembersInBattle[1].GetComponent<PlayerEntity>().isAlive)
                {
                    BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                    m_chosenTarget = true;
                    attackDescriptionText.text = this.gameObject.name + " Is going to help " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName;
                    StartCoroutine("FadeText");
                }
                else
                {
                    Debug.Log("That player is dead choose another");
                }
            }
            if (Input.GetKeyDown("3") || m_BattleButton.GetActionTargetNumber() == 12)
            {
                HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    chosenAttack = m_chosenAction,
                    AttackTarget = BC.PartyMembersInBattle[2]
                };
                if (BC.PartyMembersInBattle[2].GetComponent<PlayerEntity>().isAlive)
                {
                    BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                    m_chosenTarget = true;
                    attackDescriptionText.text = this.gameObject.name + " Is going to help " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName;
                    StartCoroutine("FadeText");
                }
                else
                {
                    Debug.Log("That player is dead choose another");
                }
            }
            if (Input.GetKeyDown("4") || m_BattleButton.GetActionTargetNumber() == 13)
            {
                HandleTurns myAttack = new HandleTurns
                {
                    Attacker = name, //Who is attacking
                    Type = "Player",//What type are they
                    AttackingGameObject = this.gameObject, //What gameObject is attacking
                    chosenAttack = m_chosenAction,
                    AttackTarget = BC.PartyMembersInBattle[3]
                };
                if (BC.PartyMembersInBattle[3].GetComponent<PlayerEntity>().isAlive)
                {
                    BC.collectActions(myAttack); //Thow the attack to the stack in BattleControl
                    m_chosenTarget = true;
                    attackDescriptionText.text = this.gameObject.name + " Is going to help " + myAttack.AttackTarget.name + " with " + myAttack.chosenAttack.attackName;
                    StartCoroutine("FadeText");
                }
                else
                {
                    Debug.Log("That player is dead choose another");
                }
            }
        }
        else
            Debug.Log("ERROR: Attack doesn't have a valid type.");
    }

    private IEnumerator PlayerAction()
    {
        if (actionHappening) //stops spamming 
        {
            yield break;
        }

        actionHappening = true;

        Vector3 meleeAttack = new Vector3(EntityToAttack.transform.position.x - 1.5f, EntityToAttack.transform.position.y, EntityToAttack.transform.position.z);
        Vector3 magicAttack = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);

        if (m_chosenAction.attackType == BaseAttack.AttackType.eMelee)
        {
            while (MoveTo(meleeAttack))
            {
                yield return null; //wait until moveToward is true
            }
        }
        else if (m_chosenAction.attackType == BaseAttack.AttackType.eMagic)
        {
            while (MoveTo(magicAttack))
            {
                yield return null; //wait until moveToward is true
               
            }
        }
        else if (m_chosenAction.attackType == BaseAttack.AttackType.eBuff)
        {

        }

        yield return new WaitForSeconds(1.5f);

        //do damage
        if(m_chosenAction.attackType == BaseAttack.AttackType.eMelee || m_chosenAction.attackType == BaseAttack.AttackType.eMagic)
        {
            playerDoDamge();
        }
        else if(m_chosenAction.attackType == BaseAttack.AttackType.eBuff)
        {
            AddBuff(m_chosenAction);
        }
        else
        {
            PartyWideDamage();
            Debug.Log("Party wide damage");
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
        m_hasChosenAction = false; //Reset battle conditions
        m_chosenTarget = false;
        currentSpeed = 0f;
        currentState = TurnState.eProssesing;
        BattleControl.turnBeingHad = false;
    }
    IEnumerator FadeText()
    {
        yield return new WaitForSeconds(4.0f);
        attackDescriptionText.text = "";
        turnText.text = "";
        notEnoughManaText.text = "";
        notEnoughPotionsText.text = "";
        usedPotionText.text = "";
}
    void playerDoDamge() // calls the take damage on the enemy w/ damage calc
    {
        int calculateDamage = GetStrength() + BC.NextTurn[0].chosenAttack.attackDamage; //calc should be done here before damage

        EntityToAttack.GetComponent<EnemEntity>().TakeDamage(calculateDamage, m_chosenAction);
    }
    void PartyWideDamage()
    {
        int calculateDamage = GetStrength() + BC.NextTurn[0].chosenAttack.attackDamage;
        for (int i = 0; i < BC.EnemiesInBattle.Count; i++)
        {
            BC.EnemiesInBattle[i].GetComponent<EnemEntity>().TakeDamage(calculateDamage, m_chosenAction);
        }
    }
}