using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EObjectiveType
{
    LocationObjective,
    KillObjective
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
}