using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest{

    protected string m_title;
    protected string m_objective;

    protected static List<Quest> m_questList = new List<Quest> { };

    //Use Quest.GetQuests to get a list of active quests.
    public static List<Quest> GetQuests(){ return new List<Quest>(m_questList); }

    public abstract bool FinishQuest();

    public abstract string GetTitle();

    public abstract string GetObjective();
}

public class KillQuest : Quest
{
    private KillQuest(string _newTitle, string _newObjective)
    {
        m_title = _newTitle;
    }

    //Overrides
    public override bool FinishQuest() //Give rewards and remove from questlist.
    {
        throw new System.NotImplementedException("Finish Quest funciton in Kill Quest class not implemented.");
    }

    public override string GetTitle()
    {
        return m_title;
    }

    public override string GetObjective()
    {
        return m_objective;
    }

    //Class Specific Functions

    //To begin the KillPig Quest, you only need to call FetchQuest.KillPig in the questgiver code.
    public static void KillPig()
    {
        string title = "Boared to Death";
        string objective = "You have to kill the pig";
        m_questList.Add(new KillQuest(title, objective));
    }
    
}