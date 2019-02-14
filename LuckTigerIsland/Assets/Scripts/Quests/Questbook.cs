using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questbook : MonoBehaviour {

    private Quest[] m_questList;
    private List<Quest> m_activeQuests;
    private List<Quest> m_completedQuests;


    void Start () {       
    
	}

    //Change from update to a specifically called function?
    void Update()
    {
        //Make sure Quests are in the correct lists based on their state.
        foreach(Quest q in m_questList)
        {
            switch (q.GetState())
            {
                case EQuestState.completed:
                    if (m_activeQuests.Contains(q))
                    {
                        m_activeQuests.Remove(q);
                        m_completedQuests.Add(q);
                    }
                    break;

                case EQuestState.active:
                    if (!m_activeQuests.Contains(q))
                    {
                        m_activeQuests.Add(q);
                    }
                    break;

                case EQuestState.undiscovered:
                    if (q.GetState() == EQuestState.undiscovered)
                    {
                        if (m_activeQuests.Contains(q))
                        {
                            m_activeQuests.Remove(q);
                        }
                        if (m_completedQuests.Contains(q))
                        {
                            m_completedQuests.Remove(q);
                        }
                    }
                    break;

                default:
                    break;
            }
        }    
    }

}
