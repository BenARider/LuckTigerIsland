﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class PlayerEntity : Entity
{

    private int m_playerIDStats = 0;
    public TextMeshProUGUI[] statTexts;
    public TextMeshProUGUI[] skillTexts;
    public TextMeshProUGUI turnText;//who's turn it is
    public TextMeshProUGUI attackDescriptionText;//describes the attack that is happening/happend
    public TextMeshProUGUI notEnoughManaText;
    public TextMeshProUGUI notEnoughPotionsText;
    public TextMeshProUGUI usedPotionText;
    public Slider expBar;
    public Button playerStatButton;
    public GameObject playerStatMenu;
    public GameObject enemyTargets;
    public GameObject teamTargets;
    public Button enemyTarget;
    public Button teamTarget;
    [SerializeField]
    private bool m_findTextGameObjects;
    public EquipEntity m_equipEntity;


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
        eNinja,
    }
    public Class MyClass;
    //Creates a new player entity with defined stats. Adding an object to one of the entities will apply those stats onto the object
    void SetPlayerStats(int _health, int _strength, int _defence, int _defenceMGC, int _speed, int _level, int _mana, int _magicPow, int _EXP)
    {
        m_maxHealth = _health;
        m_strength = _strength;
        m_defence = _defence;
        m_defenceMGC = _defenceMGC;
        m_speed = _speed;
        m_baseRequiredSpeedForTurn = 100;
        m_level = _level;
        m_maxMana = _mana;
        m_magicPower = _magicPow;
        m_EXP = _EXP;
    }

    private BattleControl BC;
    private BattleUIButton m_BattleButton;
    public HandleTurns HT;

    public List<BasePassive> passiveList = new List<BasePassive>();

    public bool m_hasChosenAction;
    public bool m_chosenTarget;

    void Start()
    {

        if (MyClass == Class.eWarrior)
        {
            SetPlayerStats(150, 20, 20, 10, 75, 1, 20, 5, 40);
        }
        if (MyClass == Class.eWizard)
        {
            SetPlayerStats(100, 10, 5, 15, 50, 1, 50, 20, 50);
        }
        if (MyClass == Class.eCleric)
        {
            SetPlayerStats(125, 10, 10, 20, 50, 1, 50, 15, 75);
        }
        if (MyClass == Class.eNinja)
        {
            SetPlayerStats(100, 15, 10, 5, 50, 1, 35, 10, 30);
        }
        SetRequiredSpeed();
        ResetHealth();
        ResetMana();

        Debug.Log("Player Stats set");

        currentState = TurnState.eProssesing;

        foreach (var passive in passiveList)
        {
            switch (passive.passiveType)
            {
                case (BasePassive.PassiveType.eAll):
                    m_health += passive.passiveAmount;
                    m_strength += passive.passiveAmount;
                    m_mana += passive.passiveAmount;
                    break;
                case (BasePassive.PassiveType.eHealth):
                    m_health += passive.passiveAmount;
                    break;
                case (BasePassive.PassiveType.eStrength):
                    m_strength += passive.passiveAmount;
                    break;
                case (BasePassive.PassiveType.eMagic):
                    m_mana += passive.passiveAmount;
                    break;
            }
        }

        foreach (var passive in passiveActiveList)
        {
            if(passive.passiveActive!=true)
            {
                switch (passive.lessThanOrMoreThan)
                {
                    case (BaseActivePassive.LessThanOrMoreThan.eLessThan):
                        switch (passive.passiveConditionType)
                        {
                            case (BaseActivePassive.PassiveCondition.eHealth):
                                if (m_health <= passive.passiveCondtitionAmount)
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
                                break;
                            case (BaseActivePassive.PassiveCondition.eMagic):
                                if (m_mana <= passive.passiveCondtitionAmount)
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
                                break;
                            case (BaseActivePassive.PassiveCondition.eStrength):
                                if (m_strength <= passive.passiveCondtitionAmount)
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
                                break;
                        }
                        break;
                    case (BaseActivePassive.LessThanOrMoreThan.eMoreThan):
                        switch (passive.passiveConditionType)
                        {
                            case (BaseActivePassive.PassiveCondition.eHealth):
                                if (m_health >= passive.passiveCondtitionAmount)
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
                                break;
                            case (BaseActivePassive.PassiveCondition.eMagic):
                                if (m_mana >= passive.passiveCondtitionAmount)
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
                                break;
                            case (BaseActivePassive.PassiveCondition.eStrength):
                                if (m_strength >= passive.passiveCondtitionAmount)
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
                                break;
                        }
                        break;
                }
            }
        }
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

        statTexts = new TextMeshProUGUI[12];

        skillTexts[0] = GameObject.Find("Action_One").GetComponent<TextMeshProUGUI>();
        skillTexts[1] = GameObject.Find("Action_Two").GetComponent<TextMeshProUGUI>();
        skillTexts[2] = GameObject.Find("Action_Three").GetComponent<TextMeshProUGUI>();
        skillTexts[3] = GameObject.Find("Action_Four").GetComponent<TextMeshProUGUI>();
        skillTexts[4] = GameObject.Find("Action_Five").GetComponent<TextMeshProUGUI>();
        skillTexts[5] = GameObject.Find("Action_Six").GetComponent<TextMeshProUGUI>();
        skillTexts[6] = GameObject.Find("Action_Seven").GetComponent<TextMeshProUGUI>();

        playerStatButton.onClick.AddListener(IncreasePlayerStatID);
        BC = GameObject.Find("BattleControl").GetComponent<BattleControl>();
        startPosition = transform.position; //setting the position based on where the object is on start up
        m_hasChosenAction = false;
        m_BattleButton = GameObject.Find("Action_List_Holder").GetComponent<BattleUIButton>();


        turnText = GameObject.Find("Player_Turn_Text").GetComponent<TextMeshProUGUI>();
        attackDescriptionText = GameObject.Find("Player_Attack_Description_Text").GetComponent<TextMeshProUGUI>();
        notEnoughManaText = GameObject.Find("not_Enough_Mana_Text").GetComponent<TextMeshProUGUI>();
        notEnoughPotionsText = GameObject.Find("not_Enough_Potions_Text").GetComponent<TextMeshProUGUI>();
        usedPotionText = GameObject.Find("used_Potion_Text").GetComponent<TextMeshProUGUI>();

		turnText = GameObject.Find("Player_Turn_Text").GetComponent<TextMeshProUGUI>();
		attackDescriptionText = GameObject.Find("Player_Attack_Description_Text").GetComponent<TextMeshProUGUI>();
		notEnoughManaText = GameObject.Find("not_Enough_Mana_Text").GetComponent<TextMeshProUGUI>();
		notEnoughPotionsText = GameObject.Find("not_Enough_Potions_Text").GetComponent<TextMeshProUGUI>();
		usedPotionText = GameObject.Find("used_Potion_Text").GetComponent<TextMeshProUGUI>();

        SetPreviousStats(); //Keep this at the bottom
    }




    // Update is called once per frame
    void Update()
    {
        foreach (var passive in passiveActiveList)
        {
            if (passive.passiveActive != true)
            {
                switch (passive.lessThanOrMoreThan)
                {
                    case (BaseActivePassive.LessThanOrMoreThan.eLessThan):
                        switch (passive.passiveConditionType)
                        {
                            case (BaseActivePassive.PassiveCondition.eHealth):
                                if (m_health <= passive.passiveCondtitionAmount)
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
                                break;
                            case (BaseActivePassive.PassiveCondition.eMagic):
                                if (m_mana <= passive.passiveCondtitionAmount)
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
                                break;
                            case (BaseActivePassive.PassiveCondition.eStrength):
                                if (m_strength <= passive.passiveCondtitionAmount)
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
                                break;
                        }
                        break;
                    case (BaseActivePassive.LessThanOrMoreThan.eMoreThan):
                        switch (passive.passiveConditionType)
                        {
                            case (BaseActivePassive.PassiveCondition.eHealth):
                                if (m_health >= passive.passiveCondtitionAmount)
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
                                break;
                            case (BaseActivePassive.PassiveCondition.eMagic):
                                if (m_mana >= passive.passiveCondtitionAmount)
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
                                break;
                            case (BaseActivePassive.PassiveCondition.eStrength):
                                if (m_strength >= passive.passiveCondtitionAmount)
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
                                break;
                        }
                        break;
                }
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
                Debug.Log("It is " + this.name + "'s turn");
                StartCoroutine("FadeText");
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


        Debug.Log(this.name + ": Choose Action");
        if (Input.GetKeyDown("1") || m_BattleButton.GetActionTargetNumber() == 1)
        {

            if (attacks[0].attackType == "Buff")
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[0].attackType != "Buff")
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

            if (attacks[1].attackType == "Buff")
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[1].attackType != "Buff")
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
            if (attacks[2].attackType == "Buff")
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[2].attackType != "Buff")
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
            if (attacks[3].attackType == "Buff")
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[3].attackType != "Buff")
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

            if (attacks[4].attackType == "Buff")
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[4].attackType != "Buff")
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

            if (attacks[5].attackType == "Buff")
            {
                teamTarget.Select();
                teamTargets.SetActive(true);
                enemyTargets.SetActive(false);
            }
            if (attacks[5].attackType != "Buff")
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

                if (attacks[6].attackType == "Buff")
                {
                teamTarget.Select();
                teamTargets.SetActive(true);
                    enemyTargets.SetActive(false);
                }
                if (attacks[6].attackType != "Buff")
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
                if (attacks[i].attackType == "Buff")
                {
                    teamTargets.SetActive(true);
                    enemyTargets.SetActive(false);
                }
                if (attacks[i].attackType != "Buff")
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
        if (m_chosenAction.attackType != "Buff")
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
        else if (m_chosenAction.attackType == "Buff")
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

        Vector3 meleeAttack = new Vector3(EntityToAttack.transform.position.x + 1.5f, EntityToAttack.transform.position.y, EntityToAttack.transform.position.z);
        Vector3 magicAttack = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);

        if (m_chosenAction.attackType == "Melee")
        {
            while (MoveTo(meleeAttack))
            {
                yield return null; //wait until moveToward is true
            }
        }
        else if (m_chosenAction.attackType == "Magic")
        {
            while (MoveTo(magicAttack))
            {
                yield return null; //wait until moveToward is true
               
            }
        }
        else if (m_chosenAction.attackType == "Buff")
        {

        }
        else
        {
            while (MoveTo(meleeAttack))
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1.5f);

        //do damage
        if(m_chosenAction.attackType != "Buff")
        {
            playerDoDamge();
        }
        if(m_chosenAction.attackType == "Buff" && m_chosenAction.attackAffliction != "")
        {
            AddBuff(m_chosenAction);
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

    //Used to control the party stat menu by setting and finding all the text values/objects
    public void IncreasePlayerStatID()
    {
        m_playerIDStats += 1;
        if (m_playerIDStats == 4)
        {
            m_playerIDStats = 0;
        }

        if (playerStatMenu.activeInHierarchy == true)
        {
            //Makes sure the objects only need to be found once
            if (m_findTextGameObjects == false)
            {
                statTexts[0] = GameObject.Find("Party_Member_Name").GetComponent<TextMeshProUGUI>();
                statTexts[1] = GameObject.Find("Class_Title").GetComponent<TextMeshProUGUI>();
                statTexts[2] = GameObject.Find("Health_Total").GetComponent<TextMeshProUGUI>();
                statTexts[3] = GameObject.Find("Physical_Damage_Total").GetComponent<TextMeshProUGUI>();
                statTexts[4] = GameObject.Find("Magical_Damage_Total").GetComponent<TextMeshProUGUI>();
                statTexts[5] = GameObject.Find("Physical_Armour_Total").GetComponent<TextMeshProUGUI>();
                statTexts[6] = GameObject.Find("Magical_Armour_Total").GetComponent<TextMeshProUGUI>();
                statTexts[7] = GameObject.Find("Critical_Hit_Chance_Total").GetComponent<TextMeshProUGUI>();
                statTexts[8] = GameObject.Find("Speed_Total").GetComponent<TextMeshProUGUI>();
                statTexts[9] = GameObject.Find("Level_Total").GetComponent<TextMeshProUGUI>();
                statTexts[10] = GameObject.Find("Skill_Points_Total").GetComponent<TextMeshProUGUI>();
                statTexts[11] = GameObject.Find("Mana_Total").GetComponent<TextMeshProUGUI>();

                m_findTextGameObjects = true;
            }

            if (m_playerIDStats == 0)
            {
                statTexts[0].text = "Luck";
                statTexts[1].text = "Cleric";
                statTexts[2].text = "" + cleric.GetHealth();
                statTexts[3].text = "" + cleric.GetStrength();
                statTexts[4].text = "" + cleric.GetMagicPower();
                statTexts[5].text = "" + cleric.GetDefence();
                statTexts[6].text = "" + cleric.GetMagicDefence();
                statTexts[7].text = "" + 50;//crit chance
                statTexts[8].text = "" + cleric.GetSpeed();
                statTexts[9].text = "" + cleric.GetLevel();
                statTexts[10].text = "" + 10;//Skills points
                statTexts[11].text = "" + cleric.GetMana();
                expBar.value = cleric.GetEXP();

            }
            if (m_playerIDStats == 1)
            {
                statTexts[0].text = "Buck";
                statTexts[1].text = "Warrior";
                statTexts[2].text = "" + warrior.GetHealth();
                statTexts[3].text = "" + warrior.GetStrength();
                statTexts[4].text = "" + warrior.GetMagicPower();
                statTexts[5].text = "" + warrior.GetDefence();
                statTexts[6].text = "" + warrior.GetMagicDefence();
                statTexts[7].text = "" + 50;//crit chance
                statTexts[8].text = "" + warrior.GetSpeed();
                statTexts[9].text = "" + warrior.GetLevel();
                statTexts[10].text = "" + 10;//Skills points
                statTexts[11].text = "" + warrior.GetMana();
                expBar.value = warrior.GetEXP();

            }
            if (m_playerIDStats == 2)
            {
                statTexts[0].text = "Duck";
                statTexts[1].text = "Wizard";
                statTexts[2].text = "" + wizard.GetMaxHealth();
                statTexts[3].text = "" + wizard.GetStrength();
                statTexts[4].text = "" + wizard.GetMagicPower();
                statTexts[5].text = "" + wizard.GetDefence();
                statTexts[6].text = "" + wizard.GetMagicDefence();
                statTexts[7].text = "" + 50;//crit chance
                statTexts[8].text = "" + wizard.GetSpeed();
                statTexts[9].text = "" + wizard.GetLevel();
                statTexts[10].text = "" + 10;//Skills points
                statTexts[11].text = "" + wizard.GetMana();
                expBar.value = wizard.GetEXP();
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
                statTexts[11].text = "" + ninja.GetMana();
                expBar.value = ninja.GetEXP();
            }
        }
        if (playerStatMenu.activeInHierarchy == false)
        {
            m_findTextGameObjects = false;
        }
    }
}