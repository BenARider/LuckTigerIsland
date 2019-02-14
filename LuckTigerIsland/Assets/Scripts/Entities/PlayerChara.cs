using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerChara : CharaClass {

    public int mana;
    public int magicPower;
    public int EXP;
    public Text healthText;
    public Text strengthText;
    public Text defenseText;
    public Text speedText;
    public Text levelText;
    public Text manaText;
    public Text magicPowerText;


    //for arguments, use the coding convention. so m_health = _health;
    public PlayerChara(int hth, int str, int def, int spd, int lvl, int man, int mp, int exp)
    {
        health = hth;
        strength = str;
        defense = def;
        speed = spd;
        level = lvl;
        mana = man;
        mp = magicPower;
        EXP = exp;
       
    }

    public PlayerChara Warrior = new PlayerChara(100, 20, 20, 5, 1, 100, 10, 0);

	// Use this for initialization
	void Start () {
        healthText.text = " " + Warrior.health.ToString();
        strengthText.text = " " + Warrior.strength.ToString();
        defenseText.text = " " + Warrior.defense.ToString();
        speedText.text = " " + Warrior.speed.ToString();
        levelText.text = " " + Warrior.level.ToString();
        manaText.text = " " + Warrior.mana.ToString();
        magicPowerText.text = " " + Warrior.magicPower.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Damage();
        Death();
    }
}
