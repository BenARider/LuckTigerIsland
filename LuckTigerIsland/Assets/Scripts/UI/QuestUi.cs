using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class QuestUi : Selectable, ISelectHandler
{
    public TextMeshProUGUI questDescriptionText;
    private TextMeshProUGUI[] questTitleText;
    public TextMeshProUGUI questTitleText2;
    public Image buttonHighlight;
    private Color m_buttonColourNonSelect;
    private Color m_buttonColourSelect;
    QuestManager m_questManager;
    // Use this for initialization
    protected override void Start()
    {
        //Creates a text array and gets all of the quest title objects to be filled
        questTitleText = new TextMeshProUGUI[5];
        questTitleText[0] = GameObject.Find("Quest_Title1").GetComponent<TextMeshProUGUI>();
        questTitleText[1] = GameObject.Find("Quest_Title2").GetComponent<TextMeshProUGUI>();
        m_questManager = GameObject.Find("_QuestManager").GetComponent<QuestManager>();
        buttonHighlight = GetComponent<Image>();

        m_buttonColourNonSelect = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        m_buttonColourSelect = new Color(0.8f, 0.8f, 0.8f, 0.8f);
        buttonHighlight.color = m_buttonColourNonSelect;
    }

    // Update is called once per frame
    void Update()
    {
     
       
    }
    protected override void OnEnable()
    {
        //Fills the quest titles
        QuestManager.Instance.m_quests[0].StartQuest();//Temp active
        List<Quest> questNames = QuestManager.Instance.GetQuests();

        if (questNames.Count != 0)
        {
            for (int i = 0; i < questNames.Count; i++)
            {
                questTitleText2.text =  questNames[i].GetTitle();
                questDescriptionText.text = "" + questNames[i].GetObjective();

            }
        }
    }
    public override void OnSelect(BaseEventData _eventData)
    {
   
        //Shows quest description when highlighting a button
        buttonHighlight.color = m_buttonColourSelect;
        List<Quest> questNames = QuestManager.Instance.GetQuests();
        if (questNames.Count != 0)
        {
            if (this.gameObject.name == "Quest1")
            {
                Debug.Log(this.gameObject.name + " was selected");
                questDescriptionText.text = "" + questNames[0].GetObjective();
            }
            if (this.gameObject.name == "Quest2")
            {
                questDescriptionText.text = "" + questNames[1].GetObjective();
            }
        }
    
    }
    public override void OnDeselect(BaseEventData eventData)
    {
        buttonHighlight.color = m_buttonColourNonSelect;
    }
}
