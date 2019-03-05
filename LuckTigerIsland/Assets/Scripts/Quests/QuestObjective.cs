using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EObjectiveType
{
    LocationObjective,
    KillObjective,
    InventoryObjective
}

[System.Serializable]
public class QuestObjective{
    protected EObjectiveType m_objectiveType;
    protected bool m_isComplete;

    public bool GetIsComplete()
    {
        return m_isComplete;
    }
    public void SetIsComplete(bool _complete)
    {
        m_isComplete = _complete;
    }
}

[System.Serializable]
public class ItemObjective : QuestObjective
{
    [SerializeField]
    private InventoryObject m_object;

    [SerializeField]
    private int m_totalAmount;
    [SerializeField]
    private int m_currentAmount;


    void Awake()
    {
        m_objectiveType = EObjectiveType.InventoryObjective;
    }

    public ItemObjective(InventoryObject _obj, int _amount)
    {
        m_object = _obj;
        m_totalAmount = _amount;
        m_currentAmount = m_totalAmount;
    }

    public int GetCurrentAmount()
    {
        return m_currentAmount;
    }
    public void IncreaseCurrentAmount(int _amount)
    {
        //use minuses to reduce amount.
        m_currentAmount += _amount;
    }
    public int GetTotalAmount()
    {
        return m_totalAmount;
    }
    public InventoryObject GetInvObject()
    {
        return m_object;
    }

}

[System.Serializable]
public class LocationObjective : QuestObjective
{
    [SerializeField]
    private ELocations m_location;

    void Awake()
    {
        m_objectiveType = EObjectiveType.LocationObjective;
    }

    public LocationObjective(ELocations _location)
    {
        m_location = _location;
    }    

    public ELocations GetLocation()
    {
        return m_location;
    }
    public void SetLocation(ELocations _location)
    {
        m_location = _location;
    }
}

[System.Serializable]
public class KillObjective : QuestObjective
{
    [SerializeField]
    private EEnemies m_enemy;

    [SerializeField]
    private int m_amount;

    private int m_amountRemaining;

    void Awake()
    {
        m_objectiveType = EObjectiveType.KillObjective;
    }

    public KillObjective(EEnemies _enemy, int _amount = 1)
    {
        m_enemy = _enemy;
        m_amount = _amount;
    }

    public EEnemies GetEnemy()
    {
        return m_enemy;
    }
    public void SetLocation(EEnemies _enemy)
    {
        m_enemy = _enemy;
    }

    public int GetAmount()
    {
        return m_amount;
    }
    public void SetAmount(int _amount)
    {
        m_amount = _amount;
    }

    public int GetAmountRemaining()
    {
        return m_amountRemaining;
    }
    public void ReduceAmountRemaining(int _val = 1)
    {
        m_amountRemaining -= _val;
    }
}