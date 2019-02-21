using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerEntity : Entity {
    private int m_playerIDStats = 0;
    public Text[] statTexts;
    public Slider expBar;
    public Button rightButton;
    public GameObject playerStatMenu;
    [SerializeField]
    private bool m_findTextGameObjects;


    //Creates a new player entity with defined stats. Adding an object to one of the entities will apply those stats onto the object
    public PlayerEntity(int _health, int _strength, int _defence, int _defenceMGC, int _speed, int _level, int _mana, int _magicPow, int _EXP, int _value)
    {
        m_health = _health;
        m_strength = _strength;
        m_defence = _defence;
        m_defenceMGC= _defenceMGC;
        m_speed = _speed;
        m_level = _level;
        m_mana = _mana;
        m_magicPow = _magicPow;
        m_EXP = _EXP;
        m_value = _value;
    }

    public PlayerEntity Warrior;
    public PlayerEntity Wizard;
    public PlayerEntity Cleric;
    public PlayerEntity Ninja;

    // Use this for initialization
    void Start () {
    
        Warrior = new PlayerEntity(150, 20, 20, 10, 5, 1, 20, 5, 0, 40);
        Wizard = new PlayerEntity(100, 10, 5, 15, 10, 1, 50, 20, 0, 50);
        Cleric = new PlayerEntity(125, 10, 10, 20, 15, 1, 50, 15, 0, 75);
        Ninja = new PlayerEntity(100, 15, 10, 5, 20, 1, 35, 10, 0, 30);
        statTexts = new Text[11];
        
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
                statTexts[1].text = "Warrior";
                statTexts[2].text = "" + Warrior.m_health;
                statTexts[3].text = "" + Warrior.m_strength;
                statTexts[4].text = "" + Warrior.m_magicPow;
                statTexts[5].text = "" + Warrior.m_defence;
                statTexts[6].text = "" + Warrior.m_defenceMGC;
                statTexts[7].text = "" + 50;//crit chance
                statTexts[8].text = "" + Warrior.m_speed;
                statTexts[9].text = "" + Warrior.m_level;
                statTexts[10].text = "" + 10;//Skills points
                expBar.value = Warrior.m_EXP;
            }
            if (m_playerIDStats == 1)
            {
                statTexts[0].text = "Buck";
                statTexts[1].text = "Wizard";
                statTexts[2].text = "" + Wizard.m_health;
                statTexts[3].text = "" + Wizard.m_strength;
                statTexts[4].text = "" + Wizard.m_magicPow;
                statTexts[5].text = "" + Wizard.m_defence;
                statTexts[6].text = "" + Wizard.m_defenceMGC;
                statTexts[7].text = "" + 25;//crit chance
                statTexts[8].text = "" + Wizard.m_speed;
                statTexts[9].text = "" + Wizard.m_level;
                statTexts[10].text = "" + 8;//Skills points
                expBar.value = Wizard.m_EXP;
            }
            if (m_playerIDStats == 2)
            {
                statTexts[0].text = "Duck";
                statTexts[1].text = "Cleric";
                statTexts[2].text = "" + Cleric.m_health;
                statTexts[3].text = "" + Cleric.m_strength;
                statTexts[4].text = "" + Cleric.m_magicPow;
                statTexts[5].text = "" + Cleric.m_defence;
                statTexts[6].text = "" + Cleric.m_defenceMGC;
                statTexts[7].text = "" + 10;//crit chance
                statTexts[8].text = "" + Cleric.m_speed;
                statTexts[9].text = "" + Cleric.m_level;
                statTexts[10].text = "" + 4;//Skills points
                expBar.value = Cleric.m_EXP;
            }
            if (m_playerIDStats == 3)
            {
                statTexts[0].text = "Phil";
                statTexts[1].text = "Ninja";
                statTexts[2].text = "" + Ninja.m_health;
                statTexts[3].text = "" + Ninja.m_strength;
                statTexts[4].text = "" + Ninja.m_magicPow;
                statTexts[5].text = "" + Ninja.m_defence;
                statTexts[6].text = "" + Ninja.m_defenceMGC;
                statTexts[7].text = "" + 60;//crit chance
                statTexts[8].text = "" + Ninja.m_speed;
                statTexts[9].text = "" + Ninja.m_level;
                statTexts[10].text = "" + 2;//Skills points
                expBar.value = Ninja.m_EXP;
            }            
        }
        if (playerStatMenu.activeInHierarchy == false)
        {
            m_findTextGameObjects = false;
        }
    }
}
