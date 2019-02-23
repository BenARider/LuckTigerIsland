using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerEntity : Entity {

    private int m_playerIDStats = 0;
    public TextMeshProUGUI[] statTexts;
    public Slider expBar;
    public Button rightButton;
    public Button playerStatButton;
    public GameObject playerStatMenu;
    [SerializeField]
    private bool m_findTextGameObjects;

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
    // Use this for initialization
 
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
        float baseSpeed = 100;
        SetRequiredSpeed();
        ResetHealth();
        ResetMana();
        
        Debug.Log("Player Stats set");
        Debug.Log("Base required speed is: " + baseSpeed);
        statTexts = new TextMeshProUGUI[12];
        playerStatButton.onClick.AddListener(IncreasePlayerStatID);
        rightButton.onClick.AddListener(IncreasePlayerStatID);

    }
	
	// Update is called once per frame
	void Update ()
    {
        Attack();
        Damage();
        Death();
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
