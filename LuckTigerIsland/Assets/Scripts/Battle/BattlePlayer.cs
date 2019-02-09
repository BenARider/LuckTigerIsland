using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : BattleEntity {

	// Use this for initialization
	void Start () {
        requiredSpeedForTurn = baseRequiredSpeedForTurn - m_Speed;
    }
    IEnumerator returnPlayer()
    {
        BattleControl.side = "Player";
        BattleControl.willDamage = "y";
        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
        SpeedTimer.isPaused = false;
        m_attackedAlready = false;

    }
    private void Update()
    {
        if (SpeedTimer.m_speedCounter % requiredSpeedForTurn == 0)
        {
            SpeedTimer.isPaused = true;

            Debug.Log("Players Turn");

            if (Input.GetKeyDown("1") && !m_attackedAlready)
            {
                m_attackedAlready = true;
                BattleControl.currentDamage = m_Damage;
                BattleControl.currentTarget = 1;
                transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
                StartCoroutine(returnPlayer());
            }
            if (Input.GetKeyDown("2") && !m_attackedAlready)
            {
                m_attackedAlready = true;
                BattleControl.currentDamage = m_Damage;
                BattleControl.currentTarget = 2;
                transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
                StartCoroutine(returnPlayer());
            }
            if (Input.GetKeyDown("3") && !m_attackedAlready)
            {
                m_attackedAlready = true;
                BattleControl.currentDamage = m_Damage;
                BattleControl.currentTarget = 3;
                transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
                StartCoroutine(returnPlayer());
            }
            if (Input.GetKeyDown("4") && !m_attackedAlready)
            {
                m_attackedAlready = true;
                BattleControl.currentDamage = m_Damage;
                BattleControl.currentTarget = 4;
                transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
                StartCoroutine(returnPlayer());
            }
        }
    }



}
