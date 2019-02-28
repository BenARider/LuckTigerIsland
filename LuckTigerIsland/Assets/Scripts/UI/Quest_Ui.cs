using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Quest_Ui : MonoBehaviour
{
    public TMPro.TMP_Text questDescription;
    public TMPro.TMP_Text questTitle;
    public TMPro.TMP_Text questDescription2;
    public TMPro.TMP_Text questTitle2;


    private int m_currentQuestId = 1;
   
    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        List<Quest> questNames = QuestManager.Instance.GetQuests();
        
        if (questNames.Count !=0)
        {
            questDescription.text = "" + questNames[0].GetObjective();
            questTitle.text = "" + questNames[0].GetTitle();

            questDescription2.text = "" + questNames[1].GetObjective();
            questTitle2.text = "" + questNames[1].GetTitle();

        }
       
       
    }

    public void SetQuestDescription(int _id)
    {
        m_currentQuestId += _id;
    }
}
