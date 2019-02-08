using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : MonoBehaviour {


    [SerializeField]
    protected int m_Health;
    [SerializeField]
    protected int m_Damage;
    [SerializeField]
    protected int m_Speed;
    [SerializeField]
    protected bool m_attackedAlready = false;
    [SerializeField]
    private int m_requiredTurn;
    [SerializeField]
    private int entityNumber;
    [SerializeField]
    private int m_enemyNumber;
    [SerializeField]
    private string side;
    [SerializeField]
    protected float baseRequiredSpeedForTurn = 100;
    [SerializeField]
    protected float requiredSpeedForTurn;
    [SerializeField]
    protected bool myTurn = false;

    public float m_speedCounter = 0.0f;
    public bool isPaused;

    public float GetSpeed() { return m_Speed; }

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {
            m_speedCounter+=0.1f;
            Debug.Log(m_speedCounter);
        }

        if (BattleControl.willDamage == "y" && BattleControl.currentTarget == entityNumber && BattleControl.side == "Enemy")
        {
            m_Health -= BattleControl.currentDamage;
            BattleControl.willDamage = "n";
            BattleControl.currentTarget = 0;
            BattleControl.side = " ";

        }
 
    }
    public void SetRequiredTurn(int turnNo)
    {
        m_requiredTurn = turnNo;
    }
    public void CheckForDamage()
    {
        if (BattleControl.willDamage == "y" && BattleControl.currentTarget == m_enemyNumber)
        {
            m_Health -= BattleControl.currentDamage;
            Debug.Log("Enemy: " + m_enemyNumber + "health total now: " + m_Health);
            BattleControl.willDamage = "n";
            BattleControl.currentTarget = 0;
            BattleControl.side = " ";
        }
    }
    void TakeDamage(int damageTaken)
    {
        Debug.Log("Enemy taking damage");
        m_Health -= damageTaken;
    }
}

