using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class QuestSpaceCreator : MonoBehaviour
{
    [SerializeField]
    EventSystem m_eventSystem;
    [SerializeField]
    Transform m_parentTransform;
    [SerializeField]
    QuestUi m_questUI;
    [SerializeField]
    GameObject m_closeObject;
    private QuestManager m_questManager;
    private bool m_startIncrease;
    // Use this for initialization
    void Start()
    {

        m_eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnEnable()
    {
        QuestManager.Instance.m_quests[0].StartQuest();//Temp active
        QuestManager.Instance.m_quests[1].StartQuest();//Temp active
        List<Quest> questNames = QuestManager.Instance.GetQuests();
        m_eventSystem.SetSelectedGameObject(m_closeObject);
        m_questUI = GameObject.Find("Quest1").GetComponent<QuestUi>();
    
        for (int i = 0; i < questNames.Count; ++i)
        {
            if (m_startIncrease == false)
            {
             m_questUI.SetNameNumSet(0);
            }
            if (i < 20)
            {
                m_questUI = Instantiate(m_questUI, new Vector2(m_parentTransform.transform.position.x + 350, i * -45.0f), m_parentTransform.rotation);
                m_questUI.transform.SetParent(m_parentTransform, false);
                m_questUI.m_itemButton.interactable = true;
            }
            if (m_startIncrease == true)
            {
           m_questUI.SetNameNumSet(1);
            }
            m_startIncrease = true;
           m_questUI.questTitleText.text =  QuestManager.Instance.m_quests[m_questUI.GetNameNumSet()].GetTitle();
        }
        print("SetNameNum" + questNames.Count);
    }
    private void OnDisable()
    {
        foreach (Transform child in m_parentTransform.transform)
        {
            Destroy(child.gameObject);
            m_startIncrease = false;
        }
        m_questUI.m_itemButton.interactable = false;

    }

}