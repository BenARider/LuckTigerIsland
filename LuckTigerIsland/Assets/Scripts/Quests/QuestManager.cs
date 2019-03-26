using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : LTI.Singleton<QuestManager>
{
    //Array of every quest in the game.
    [SerializeField]
    public List<Quest> m_quests = new List<Quest>();

    //List of all currently active quests. Add to this when starting a quest. Remove when finishing a Quest.
    private static List<Quest> m_activeQuestList = new List<Quest> { };

    //Use Quest.GetQuests to get a list of active quests.
    public List<Quest> GetQuests() { return new List<Quest>(m_activeQuestList); }

    //Used in creating new quests
    [SerializeField]
    private string m_title;
    [SerializeField]
    private string m_description;
    [SerializeField]
    private int m_exp;
    [SerializeField]
    private int m_gold;

    //Create Quest
    public void CreateQuest()
    {
        GameObject _go = new GameObject("Quest: " + m_title);
        Quest _q = _go.AddComponent<Quest>() as Quest;
        _q.SetTitle(m_title);
        _q.SetObjective(m_description);
        _q.SetExpReward(m_exp);
        _q.SetGoldReward(m_gold);
        _go.transform.SetParent(this.transform);       

        m_quests.Add( _q);
    }
    public void DeleteQuest(int _index)
    {
        DestroyImmediate(GetComponent<Transform>().GetChild(_index).gameObject); 
        m_quests.RemoveAt(_index);
    }

    //Add to QuestList
    public void AddQuest(Quest _quest)
    {
        if (!m_activeQuestList.Contains(_quest)){
            m_activeQuestList.Add(_quest);
        }
        else
        {
            Debug.LogError("Cannot add quest. " + _quest.GetTitle() + " is already active.");
        }            
    }

    //Remove From QuestList
    public void RemoveQuest(Quest _quest)
    {
        if (m_activeQuestList.Contains(_quest)){
            m_activeQuestList.Remove(_quest);
        } else
        {
            Debug.LogError("Cannot remove. " + _quest.GetTitle() + " was not in the active quest list.");
        }
    }

    void Start()
    {
        instance = this;
        //Checks to see if there are quests with duplicate titles.
        for(int i = 0; i < m_quests.Count; i++)
        {
            for(int j = i + 1; j < m_quests.Count; j++)
            {
                if(m_quests[i].GetTitle() == m_quests[j].GetTitle())
                {
                    Debug.LogError("Quest " + i +" has the same title as Quest "+ j );
                }
            }
        }
    }
}