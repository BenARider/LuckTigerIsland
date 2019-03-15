using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    /*public Quest(string _title, string _description, int _exp, int _gold)
    {
        m_title = _title;
        m_description = _description;
        m_expReward = _exp;
        m_goldReward = _gold;
    }*/
    
    [SerializeField]
    private string m_title;
    [SerializeField]
    private string m_description;
    [SerializeField]
    private int m_expReward;
    [SerializeField]
    private int m_goldReward;
    [SerializeField]
    private InventoryObject m_itemReward;


    //Objectives Lists. Make a new list when a new type of objective is created.
    public List<QuestObjective> m_allObjectives;
    [SerializeField]    
    public List<KillObjective> m_killObjectives;
    [SerializeField]
    public List<LocationObjective> m_locationObjectives;
    [SerializeField]
    public List<ItemObjective> m_inventoryObjectives;

    //For use with creating objects in the inspector. Using o_name to represent objectives_name.
    [SerializeField]
    private EObjectiveType o_type;
    [SerializeField]
    private ELocations o_location;
    [SerializeField]
    private EEnemies o_enemy;
    [SerializeField]
    private int o_enemyAmount;

    // Add/Remove Objectives
    public void AddLocationObjective(ELocations _location)
    {
        LocationObjective _lo = new LocationObjective(_location);
        m_locationObjectives.Add(_lo);
        m_allObjectives.Add(_lo);
    }

    public void AddKillObjective(EEnemies _enemy, int _amount = 1)
    {
        KillObjective _eo = new KillObjective(_enemy, _amount);
        m_killObjectives.Add(_eo);
        m_allObjectives.Add(_eo);
    }

    public void RemoveLastLocationObjective()
    {
        if (m_locationObjectives.Count != 0)
        {
            m_locationObjectives.RemoveAt(m_locationObjectives.Count - 1);
        }
     }

    public void RemoveLastKillObjective()
    {
        if (m_killObjectives.Count != 0)
        {
            m_killObjectives.RemoveAt(m_killObjectives.Count - 1);
        }
    }


    //Add and remove from active quest list.
    public void StartQuest()
    {
        QuestManager.Instance.AddQuest(this);
        Debug.Log("quest added");
    }

    public void EndQuest()
    {
        QuestManager.Instance.RemoveQuest(this);
        Debug.Log("quest ended");
    }

    public void CheckCompletion()
    {
        //Check All Objectives
        if(m_allObjectives.Count != 0)
        {
            bool finished = true;
            foreach(QuestObjective _qo in m_allObjectives)
            {
                if (!_qo.GetIsComplete())
                {
                    finished = false;
                }
            }

            //If all quest objectives were finished, End the Quest.
            if (!finished)
            {
                EndQuest();
            }
        }
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



