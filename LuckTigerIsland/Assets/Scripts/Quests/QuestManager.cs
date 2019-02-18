using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [SerializeField]
    Quest[] m_quests;

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

    //To make sure there is only one QuestManager.
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only one quest manager can be present");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}