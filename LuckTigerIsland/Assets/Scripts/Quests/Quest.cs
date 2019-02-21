using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public Quest(string _title, string _objective, int _exp, int _gold)
    {
        title = _title;
        objective = _objective;
        expReward = _exp;
        goldReward = _gold;
    }

    public string title;
    public string objective;
    public int expReward;
    public int goldReward;

    public void StartQuest()
    {
        QuestManager.instance.AddQuest(this);
        Debug.Log("quest added");
    }

    public void EndQuest()
    {
        QuestManager.instance.RemoveQuest(this);
    }

   
}



