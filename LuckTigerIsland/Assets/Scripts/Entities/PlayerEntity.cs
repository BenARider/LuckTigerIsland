using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {
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
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Attack();
        Damage();
        Death();
    }
}
