using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerEntity : Entity {
    private int m_playerIDStats;
    public Text healthText;
    public Text strengthText;
    public Text defenceText;
    public Text defenceMGCText;
    public Text speedText;
    public Text levelText;
    public Text magicPowText;
    public Slider expBar;
    public Button rightButton;
    public Button leftButton;


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

    public PlayerEntity Warrior = new PlayerEntity(150, 20, 20, 10, 5, 1, 20, 5, 0, 40);
    public PlayerEntity Wizard = new PlayerEntity(100, 10, 5, 15, 10, 1, 50, 20, 0, 50);
    public PlayerEntity Cleric = new PlayerEntity(125, 10, 10, 20, 15, 1, 50, 15, 0, 75);
    public PlayerEntity Ninja = new PlayerEntity(100, 15, 10, 5, 20, 1, 35, 10, 0, 30);

    // Use this for initialization
    void Start () {
        rightButton.onClick.AddListener(IncreasePlayerStatID);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if(m_playerIDStats == 0)
        {
            healthText.text = "" + Warrior.m_health;
            strengthText.text = "" + Warrior.m_strength;
            defenceText.text = "" + Warrior.m_defence;
            defenceMGCText.text = "" + Warrior.m_defenceMGC;
            speedText.text = "" + Warrior.m_speed;
            levelText.text = "" + Warrior.m_level;
            magicPowText.text = "" + Warrior.m_magicPow;
            expBar.value = Warrior.m_EXP;
            Debug.Log("Showing Warrior");
        }
        if (m_playerIDStats == 1)
        {
            healthText.text = "3" + Wizard.m_health;
            strengthText.text = "" + Wizard.m_strength;
            defenceText.text = "" + Wizard.m_defence;
            defenceMGCText.text = "" + Wizard.m_defenceMGC;
            speedText.text = "" + Wizard.m_speed;
            levelText.text = "" + Wizard.m_level;
            magicPowText.text = "" + Wizard.m_magicPow;
            expBar.value = Wizard.m_EXP;
            Debug.Log("Showing Wizard");
        }
        if (m_playerIDStats == 2)
        {
            healthText.text = "4" + Cleric.m_health;
            strengthText.text = "" + Cleric.m_strength;
            defenceText.text = "" + Cleric.m_defence;
            defenceMGCText.text = "" + Cleric.m_defenceMGC;
            speedText.text = "" + Cleric.m_speed;
            levelText.text = "" + Cleric.m_level;
            magicPowText.text = "" + Cleric.m_magicPow;
            expBar.value = Cleric.m_EXP;
        }
        if (m_playerIDStats == 3)
        {
            healthText.text = "5" + Ninja.m_health;
            strengthText.text = "" + Ninja.m_strength;
            defenceText.text = "" + Ninja.m_defence;
            defenceMGCText.text = "" + Ninja.m_defenceMGC;
            speedText.text = "" + Ninja.m_speed;
            levelText.text = "" + Ninja.m_level;
            magicPowText.text = "" + Ninja.m_magicPow;
            expBar.value = Ninja.m_EXP;
        }
        Attack();
        Damage();
        Death();
    }
   public void IncreasePlayerStatID()
    {
        m_playerIDStats++;
        Debug.Log("Pressing button");
    }
}
