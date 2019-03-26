using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class QuestUi : MonoBehaviour, ISelectHandler
{
    public TextMeshProUGUI questDescriptionText;
    private TextMeshProUGUI[] questTitleText;
    public TextMeshProUGUI questTitleText2;
    public Image buttonHighlight;
    private Color m_buttonColourNonSelect;
    private Color m_buttonColourSelect;
    QuestManager m_questManager;
    [SerializeField]
    private int m_nameNumSet;
    public Button m_itemButton;
    // Use this for initialization
    void Start()
    {
        //Creates a text array and gets all of the quest title objects to be filled
        questTitleText = new TextMeshProUGUI[5];
        questTitleText[0] = GameObject.Find("Quest_Title1").GetComponent<TextMeshProUGUI>();
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
   public void OnSelect(BaseEventData _eventData)
    {
        //Shows quest description when highlighting a button
        buttonHighlight.color = m_buttonColourSelect;
        questDescriptionText.text = "" + QuestManager.Instance.m_quests[m_nameNumSet].GetObjective();
    }
  public void OnDeselect(BaseEventData eventData)
    {
        buttonHighlight.color = m_buttonColourNonSelect;
        questDescriptionText.text = "";
    }
    public int GetNameNumSet()
    {
        return m_nameNumSet;
    }
    public void SetNameNumSet(int _nameNum)
    {
        m_nameNumSet += _nameNum;
    }
}
