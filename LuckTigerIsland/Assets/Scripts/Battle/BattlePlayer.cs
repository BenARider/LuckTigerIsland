using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : PlayerEntity {

	// Use this for initialization
	void Start () {
		
		m_requiredSpeedForTurn = m_baseRequiredSpeedForTurn - GetSpeed();
		ResetHealth();
		ResetMana();
    }

    IEnumerator returnPlayer()
    {
		Debug.Log("Player returning");
        BattleControl.side = "Player";
        BattleControl.willDamage = "y";
        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
        SpeedTimer.isPaused = false;
		Invoke("SetAttack",1.0f);
    }
	void SetAttack()
	{
		m_attackedAlready = false;
	}
	private void Update()
	{ }




}
