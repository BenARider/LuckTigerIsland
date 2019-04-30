using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

	public PlayerManager PM;
	[SerializeField]
	protected int m_maxHealth;
	[SerializeField]
	protected int m_strength; //basic attack
	[SerializeField]
	public int m_magicPower;
	[SerializeField]
	protected int m_defence;
	[SerializeField]
	protected int m_defenceMGC;
	[SerializeField]
	protected int m_speed;
	[SerializeField]
	protected int m_maxMana;

	protected void SetPlayerStats(int _health, int _strength, int _defence, int _defenceMGC, int _speed, int _mana, int _magicPow)
	{
		m_maxHealth = _health;
		m_strength = _strength;
		m_defence = _defence;
		m_defenceMGC = _defenceMGC;
		m_speed = _speed;
		m_maxMana = _mana;
		m_magicPower = _magicPow;
	}
	public int GetMaxHealth()
	{
		return m_maxHealth;
	}
	public void AddHealth(int value)
	{
		m_maxHealth += value;
	}
	public int GetStrength()
	{
		return m_strength;
	}
	public void AddStrength(int value)
	{
		m_strength += value;
	}
    public void SetStrength(int value)
    {
        m_strength = value;
    }
    public int GetMagicPower()
	{
		return m_magicPower;
	}
	public void AddMagicPower(int value)
	{
		m_magicPower += value;
	}
	public int GetDefence()
	{
		return m_defence;
	}
	public void AddDefence(int value)
	{
		m_defence += value;
	}
    public void SetDefence(int value)
    {
        m_defence = value;
    }
    public int GetMagicDefence()
	{
		return m_defenceMGC;
	}
	public void AddMagicDefence(int value)
	{
		m_defenceMGC += value;
	}
	public int GetSpeed()
	{
		return m_speed;
	}
	public void AddSpeed(int value)
	{
		m_speed += value;
	}
	public int GetMaxMana()
	{
		return m_maxMana;
	}
	public void AddMaxMana(int value)
	{
		m_maxMana += value;
	}
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        PM = PlayerManager.Instance;
    }
}
