using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChara : CharaClass {

    public int mana;
    public int magicPower;
    public int EXP;

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
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Damage();
        Death();
    }
}
