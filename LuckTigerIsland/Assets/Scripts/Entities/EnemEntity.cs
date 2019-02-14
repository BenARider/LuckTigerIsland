using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemEntity : Entity {
    //Creates a new entity with defined stats. Adding an object to one of the entities will apply those stats onto the object
    public EnemEntity(int _health, int _strength, int _defence, int _defenceMGC, int _speed, int _level, int _mana, int _magicPow, int _aggress, int _intel, int _XP)
    {
        m_health = _health;
        m_strength = _strength;
        m_defence = _defence;
        m_defenceMGC = _defenceMGC;
        m_speed = _speed;
        m_level = _level;
        m_mana = _mana;
        m_magicPow = _magicPow;
        m_aggress = _aggress;
        m_intel = _intel;
        m_XP = _XP;
    }

   
    public EnemEntity Basic = new EnemEntity(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
    public EnemEntity Tiger = new EnemEntity(100, 10, 5, 15, 10, 4, 0, 0, 16, 4, 150);

	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        Attack();
        Damage();
        Death();
	}

}
