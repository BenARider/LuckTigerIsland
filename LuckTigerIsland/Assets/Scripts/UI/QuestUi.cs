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
    public Image buttonHighlight;
    private Color m_buttonColourNonSelect;
    private Color m_buttonColourSelect;
    // Use this for initialization
    protected override void Start()
    {
        //Creates a text array and gets all of the quest title objects to be filled
        questTitleText = new TextMeshProUGUI[5];
        questTitleText[0] = GameObject.Find("Quest_Title1").GetComponent<TextMeshProUGUI>();
        questTitleText[1] = GameObject.Find("Quest_Title2").GetComponent<TextMeshProUGUI>();
        buttonHighlight = GetComponent<Image>();

        m_buttonColourNonSelect = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        m_buttonColourSelect = new Color(0.1f, 0.1f, 0.1f, 0.1f);
        buttonHighlight.color = m_buttonColourNonSelect;
    }

    // Update is called once per frame
    void Update()
    {
        //Fills the quest titles
        List<Quest> questNames = QuestManager.Instance.GetQuests();
        for (int i = 0; i < questNames.Count; i++)
        {
            questTitleText[0].text = "" + questNames[0].GetTitle();
            questTitleText[1].text = "" + questNames[1].GetTitle();
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
