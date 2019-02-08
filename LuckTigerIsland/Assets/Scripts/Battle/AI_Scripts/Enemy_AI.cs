using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_AI : BattleEntity {

    [SerializeField]
    private int m_TargetedPlayer;

    // Use this for initialization
    void Start () {
        requiredSpeedForTurn = baseRequiredSpeedForTurn - m_Speed;
    }

    IEnumerator returnEnemy()
    {
        BattleControl.side = "Enemy";
        BattleControl.willDamage = "y";
        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
        isPaused = false;
        m_attackedAlready = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_speedCounter % requiredSpeedForTurn == 0)
        {
            isPaused = true;

            Debug.Log("Enemies Turn");

            if (BattleControl.side == "Player")
            {
                CheckForDamage();
            }
            if (m_attackedAlready == false)
            {
                m_attackedAlready = true;
                m_TargetedPlayer = UnityEngine.Random.Range(1, 4); //determines target for enemy attack
                BattleControl.currentDamage = m_Damage;
                BattleControl.currentTarget = m_TargetedPlayer;
                transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
                StartCoroutine(returnEnemy());
            }
            if (m_Health <= 0)
            {
                Debug.Log("The enemy is dead");
            }
        }


    }
}
