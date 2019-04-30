using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//USE A DIFFERENT SCRIPT FOR FUNCTIONS. 

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemy")]
public class EnemyObject : ScriptableObject
{
    public enum TurnState
    {
        eProssesing,
        eChooseAction,
        eChooseTarget,
        eWaiting,
        eAction,
        eDead
    }

    public enum Affliction
    {
        eNone,
        eOnFire,
        eFrozen,
        eInfected,
        eStunned
    }

    [SerializeField]
    public GameObject go;
    [SerializeField]
    public EEnemies enemyType;

    //General stats used to initialise entities
    [SerializeField]
    protected int m_maxHealth;
    [SerializeField]
    protected int m_health;
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
    protected int m_mana;
    [SerializeField]
    protected int m_maxMana;
    [SerializeField]
    protected bool m_stunned = false;
    [SerializeField]
    protected bool m_afflicted = false;

    //level and class values
    [SerializeField]
    protected int m_level;
    [SerializeField]
    protected int m_xpAward;
    [SerializeField]
    protected int m_EXP;
    [SerializeField]
    protected string Class; //used to determine stat allocation in the other classes.

    /// Following are used purely for battle integration. Used by both enemies and players.
    [SerializeField]
    protected bool m_attackedAlready = false;
    [SerializeField]
    protected int m_entityNumber;
    [SerializeField]
    protected static float m_baseRequiredSpeedForTurn = 100;
    [SerializeField]
    protected float m_requiredSpeedForTurn;
    [SerializeField]
    protected bool myTurn = false;
    [SerializeField]
    public bool battleWon = false;
    [SerializeField]
    protected float currentSpeed = 0f;
    [SerializeField]
    protected bool isAlive = true;
    [SerializeField]
    protected bool countedDead = false; //used to prevent insta wins or gameovers
    [SerializeField]
    protected bool alreadyAfflicted = false;
    [SerializeField]
    protected int afflictionTimes = 0;

    //private bool canAttack = false; //used to prevent the ai from having too many turns. Only enabled on use of state transition.
    public int aggress; //likelihood to attack oppossing attacker (Between 1-20)
    public int intel; //likelihood to attack pm with high value (Between 1-20)
    public int XP; //amount of xp they give
    public TextMeshProUGUI attackDescriptionText;//describes the attack that is happening/happend;
    public TextMeshProUGUI turnText;//who's turn it is

    protected float walkSpeed = 5f;

    //used for damage calculations
    //-------------------------------------------
    public int tempDMGReduct = 0; //the amount of damage reduced to the intial damage
    public int totalDMG = 0; //total amount of damage that goes through.
    public int chanceToHit;

    //CALCULATION IDEA: (atk: strength / def: defense = tempDMGReduct) atk: strength - tempDMGReduct = totalDMG
    //e.g. (100 / 10 = 10) 100 - 10 = 90
    //e.g. (56 / 8 = 7) 56 - 7 = 49

    public bool attacking = false;
    public bool dmgRecieve = false;
    public bool dmgDealt = false;
    
    public TurnState currentState;
    public Affliction currentAffliction;

    protected Vector3 startPosition; //used for animation, move to player when attacking and then back

    protected bool actionHappening = false; //Think attackAlready, stops the entities spamming
    public GameObject EntityToAttack; //What the entity wants to attack

    public List<BaseAttack> attacks = new List<BaseAttack>();
    public List<InventoryObject> HealthPotions = new List<InventoryObject>();
    public List<InventoryObject> ManaPotions = new List<InventoryObject>();
    protected BaseAttack m_chosenAction;

    private BattleControl BC;
    public HandleTurns HT;

    //-----------------------------------------------------------------------------------------------------
    //Setters and Getters
    public void Sethealth(int _health) //The argument should be _health. The body should then be m_health = _health.
    {
        m_health = _health;
    }
    public void ResetHealth()  //used at the beginning with initialisation, max health is required for ui calculations and heal effects.
    {
        m_health = m_maxHealth;
    }
    public int GetMaxHealth()
    {
        return m_maxHealth;
    }
    public int GetHealth()
    {
        return m_health;
    }
    public int GetStrength()
    {
        return m_strength;
    }
    public void SetStrength(int _strength)
    {
        m_strength = _strength;
    }
    public void SetDefence(int _defence)
    {
        m_defence = _defence;
    }
    public int GetMagicPower()
    {
        return m_magicPower;
    }
    public int GetDefence()
    {
        return m_defence;
    }

    public int GetMagicDefence()
    {
        return m_defenceMGC;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public int GetSpeed()
    {
        return m_speed;
    }

    public float GetRequiredSpeed()
    {
        return m_requiredSpeedForTurn;
    }
    public void SetRequiredSpeed()
    {
        m_requiredSpeedForTurn = m_baseRequiredSpeedForTurn - GetSpeed();
    }
    public int GetMaxMana()
    {
        return m_maxMana;
    }
    public void ResetMana()
    {
        m_mana = m_maxMana;
    }
    public int GetMana()
    {
        return m_mana;
    }

    public void SetLevel(int _level)
    {
        m_level = _level;
    }

    public int GetEntityNo()
    {
        return m_entityNumber;
    }
    public void SetEntityNo(int entityNumber)
    {
        m_entityNumber = entityNumber;
    }
    public int GetLevel()
    {
        return m_level;
    }
    public int GetEXP()
    {
        return m_EXP;
    }
}