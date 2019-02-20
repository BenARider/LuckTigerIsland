using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerEntity : Entity {

    public int EXP;
    public Text healthText;
    public Text strengthText;
    public Text defenseText;
    public Text speedText;
    public Text levelText;
    public Text manaText;
    public Text magicPowerText;

	protected bool canAttack;

    //for arguments, use the coding convention. so m_health = _health;

	// Use this for initialization
	void Start () {
		if (Class == "Warrior")
		{
			playerCharaStats(100, 100, 20, 10, 20, 50, 1, 0);
		}
		if (Class == "Ninja")
		{
			playerCharaStats(100, 100, 20, 10, 20, 70, 1, 0);
		}
		if (Class == "Cleric")
		{
			playerCharaStats(100, 100, 20, 10, 20, 40, 1, 0);
		}
		if  (Class == "Archer")
		{
			playerCharaStats(100, 100, 20, 10, 20, 65, 1, 0);
		}
		m_requiredSpeedForTurn = m_baseRequiredSpeedForTurn - GetSpeed();
		ResetHealth();
		ResetMana();
		Debug.Log("Player Values Set");
		//healthText.text = " " + Warrior.health.ToString();
  //      strengthText.text = " " + Warrior.strength.ToString();
  //      defenseText.text = " " + Warrior.defense.ToString();
  //      speedText.text = " " + Warrior.speed.ToString();
  //      levelText.text = " " + Warrior.level.ToString();
  //      manaText.text = " " + Warrior.mana.ToString();
  //      magicPowerText.text = " " + Warrior.magicPower.ToString();
    }
	IEnumerator returnPlayer()
	{
		Debug.Log("Player returning");
		BattleControl.side = "Player";
		BattleControl.willDamage = "y";
		yield return new WaitForSeconds(1.5f);
		transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
		SpeedTimer.isPaused = false;
		Invoke("SetAttack", 1.0f);
	}
	void SetAttack()
	{
		m_attackedAlready = false;
	}
	// Update is called once per frame
	private void Update ()
    {
		if (SpeedTimer.m_speedCounter % m_requiredSpeedForTurn == 0 && SpeedTimer.isPaused == false || SpeedTimer.m_speedCounter % m_requiredSpeedForTurn == 0.5 && m_attackedAlready == false && SpeedTimer.m_speedCounter > 1.0f)
		{
            Debug.Log("Player Turn");

            SpeedTimer.isPaused = true;
			if (Input.GetKeyDown("1") && !m_attackedAlready)
			{
				m_attackedAlready = true;
				BattleControl.currentDamage = GetStrength();
				BattleControl.currentTarget = 5;
				transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
				StartCoroutine(returnPlayer());
			}
			if (Input.GetKeyDown("2") && !m_attackedAlready)
			{
				m_attackedAlready = true;
				BattleControl.currentDamage = GetStrength();
				BattleControl.currentTarget = 6;
				transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
				StartCoroutine(returnPlayer());
			}
			if (Input.GetKeyDown("3") && !m_attackedAlready)
			{
				m_attackedAlready = true;
				BattleControl.currentDamage = GetStrength();
				BattleControl.currentTarget = 7;
				transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
				StartCoroutine(returnPlayer());
			}
			if (Input.GetKeyDown("4") && !m_attackedAlready)
			{
				m_attackedAlready = true;
				BattleControl.currentDamage = GetStrength();
				BattleControl.currentTarget = 8;
				transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
				StartCoroutine(returnPlayer());
			}
		}
        if (BattleControl.side == "Enemy")
        {
            CheckForDamage("Player");
        }
        //Damage();
        Death(); //only needed when getting attacked or hp<=0
    }
	protected void playerCharaStats(int hth, int man, int str, int mp, int def, int spd, int lvl, int exp)
	{
		m_maxHealth = hth;
		m_maxMana = man;
		m_strength = str;
		m_defence = def;
		m_speed = spd;
		m_level = lvl;
		mp = m_magicPower;
		EXP = exp;
	}
}
