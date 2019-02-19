using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton
{
    //Array of every quest in the game.
    [SerializeField]
    Quest[] m_quests;

    //List of all currently active quests. Add to this when starting a quest. Remove when finishing a Quest.
    private static List<Quest> m_activeQuestList = new List<Quest> { };

    //Use Quest.GetQuests to get a list of active quests.
    public List<Quest> GetQuests() { return new List<Quest>(m_activeQuestList); }

    //Add to QuestList
    public void AddQuest(Quest _quest)
    {
        if (!m_activeQuestList.Contains(_quest)){
            m_activeQuestList.Add(_quest);
        }
        else
        {
            Debug.LogError("Cannot add quest. " + _quest.title + " is already active.");
        }
            
    }

    //Remove From QuestList
    public void RemoveQuest(Quest _quest)
    {
        if (m_activeQuestList.Contains(_quest)){
            m_activeQuestList.Remove(_quest);
        } else
        {
            Debug.LogError("Cannot remove. " + _quest.title + " was not in the active quest list.");
        }
    }

    void Start()
    {
        //Checks to see if there are quests with duplicate titles.
        for(int i = 0; i < m_quests.Length; i++)
        {
            for(int j = i + 1; j < m_quests.Length; j++)
            {
                if(m_quests[i].title == m_quests[j].title)
                {
                    Debug.LogError("Quest " + i +" has the same title as Quest "+ j );
                }
            }
        }
    }
}