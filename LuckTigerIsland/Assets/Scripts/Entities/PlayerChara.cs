using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerChara : CharaClass {


	[SerializeField]
	private string playerClass;
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
		if (playerClass == "Warrior")
		{
			playerCharaStats(100, 100, 20, 10, 20, 50, 1, 0);
		}
		if (playerClass == "Ninja")
		{
			playerCharaStats(100, 100, 20, 10, 20, 70, 1, 0);
		}
		if (playerClass == "Cleric")
		{
			playerCharaStats(100, 100, 20, 10, 20, 40, 1, 0);
		}
		if  (playerClass == "Archer")
		{
			playerCharaStats(100, 100, 20, 10, 20, 65, 1, 0);
		}
		requiredSpeedForTurn = baseRequiredSpeedForTurn - GetSpeed();
		ResetHealth();
		ResetMana();
		Debug.Log("Values Set");
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
		if (SpeedTimer.m_speedCounter % requiredSpeedForTurn == 0 && SpeedTimer.isPaused == false || SpeedTimer.m_speedCounter % requiredSpeedForTurn == 0.5 && m_attackedAlready == false && SpeedTimer.m_speedCounter > 1.0f)
		{ 
			SpeedTimer.isPaused = true;
			if (Input.GetKeyDown("1") && !m_attackedAlready)
			{
				m_attackedAlready = true;
				BattleControl.currentDamage = GetStrength();
				BattleControl.currentTarget = 1;
				transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
				StartCoroutine(returnPlayer());
			}
			if (Input.GetKeyDown("2") && !m_attackedAlready)
			{
				m_attackedAlready = true;
				BattleControl.currentDamage = GetStrength();
				BattleControl.currentTarget = 2;
				transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
				StartCoroutine(returnPlayer());
			}
			if (Input.GetKeyDown("3") && !m_attackedAlready)
			{
				m_attackedAlready = true;
				BattleControl.currentDamage = GetStrength();
				BattleControl.currentTarget = 3;
				transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
				StartCoroutine(returnPlayer());
			}
			if (Input.GetKeyDown("4") && !m_attackedAlready)
			{
				m_attackedAlready = true;
				BattleControl.currentDamage = GetStrength();
				BattleControl.currentTarget = 4;
				transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
				StartCoroutine(returnPlayer());
			}
		}
			//Damage();
			Death(); //only needed when getting attacked or hp<=0
    }
	protected void playerCharaStats(int hth, int man, int str, int mp, int def, int spd, int lvl, int exp)
	{
		maxHealth = hth;
		maxMana = man;
		strength = str;
		defense = def;
		speed = spd;
		level = lvl;
		mp = magicPower;
		EXP = exp;
	}
}
