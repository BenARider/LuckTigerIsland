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
    public TextMeshProUGUI questTitleText;
    public Image buttonHighlight;
    private Color m_buttonColourNonSelect;
    private Color m_buttonColourSelect;
    QuestManager m_questManager;
    [SerializeField]
    private int m_nameNumSet;
    public Button m_itemButton;
    public GameObject m_removeButton;
    // Use this for initialization
    void Start()
    {
        m_questManager = GameObject.Find("_QuestManager").GetComponent<QuestManager>();

    }
    // Update is called once per frame
    void Update()
    {
    }
   public void OnSelect(BaseEventData _eventData)
    {
        //Shows quest description when highlighting a button
 
        questDescriptionText.text = "" + QuestManager.Instance.m_quests[m_nameNumSet].GetObjective();
        m_removeButton.SetActive(false);
    }
  public void OnDeselect(BaseEventData eventData)
    {
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
    public void RemoveQuest()
    {
        Destroy(this.gameObject);
        QuestManager.Instance.DeleteQuest(m_nameNumSet);
    }
}
