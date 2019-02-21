using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    Quest(string _title, string _description, int _exp, int _gold)
    {
        m_title = _title;
        m_description = _description;
        m_expReward = _exp;
        m_goldReward = _gold;
    }
    
    [SerializeField]
    private string m_title;
    [SerializeField]
    private string m_description;
    [SerializeField]
    private int m_expReward;
    [SerializeField]
    private int m_goldReward;

    //Objectives List
    [SerializeField]
    public List<LocationObjective> m_objectives;
    public List<KillObjective> m_killObjectives;

    //For use with creating objects in the inspector. Using o_name to represent objectives_name.
    [SerializeField]
    private EObjectiveType o_type;
    [SerializeField]
    private ELocations o_location;
    [SerializeField]
    private EEnemies o_enemy;
    [SerializeField]
    private int o_enemyAmount;

    //Add Objectives
    public void AddLocationObjective(ELocations _location)
    {
        LocationObjective _lo = new LocationObjective(_location);
        m_objectives.Add(_lo);
    }

    public void AddKillObjective(EEnemies _enemy, int _amount = 1)
    {
        KillObjective _eo = new KillObjective(_enemy, _amount);
        m_objectives.Add(_eo);
    }

    public void RemoveLastFromList()
    {
        if (m_objectives.Count != 0)
        {
            m_objectives.RemoveAt(m_objectives.Count - 1);
        }
    }

    //Add to active quest list.
    public void StartQuest()
    {
        QuestManager.Instance.AddQuest(this);
        Debug.Log("quest added");
    }

    public void EndQuest()
    {
        QuestManager.Instance.RemoveQuest(this);
    }

    //Getters and Setters
    public string GetTitle()
    {
        return m_title;
    }
    public void SetTitle(string _title)
    {
        m_title = _title;
    }

    public string GetObjective()
    {
        return m_description;
    }
    public void SetObjective(string _desc)
    {
        m_description = _desc;
    }

    public int GetExpReward()
    {
        return m_expReward;
    }
    public void SetExpReward(int _exp)
    {
        m_expReward = _exp;
    }

    public int GetGoldReward()
    {
        return m_goldReward;
    }
    public void SetGoldReward(int _gold)
    {
        m_goldReward = _gold;
    }

}



