using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestListControl : MonoBehaviour {

    [SerializeField]
    private GameObject m_buttonTemplate;
    [SerializeField]
    private TextMeshProUGUI m_description;

    private void Start()
    {
        UpdateQuestUI();
    }

    public void UpdateQuestUI()
    {
        try
        {
            if (QuestManager.Instance.GetQuests().Count > 0)
            {
                SetDefaults();
                for (int i = 0; i < QuestManager.Instance.GetQuests().Count; i++)
                {
                    GameObject button = Instantiate(m_buttonTemplate) as GameObject;
                    button.SetActive(true);

                    if (QuestManager.Instance.m_quests[i] != null)
                    {
                        button.GetComponent<QuestListButton>().SetText(QuestManager.Instance.m_quests[i].GetTitle());
                        button.GetComponent<QuestListButton>().SetDescription(QuestManager.Instance.m_quests[i].GetObjective());
                    }
                    else
                    {
                        button.GetComponent<QuestListButton>().SetText("NullInvSlot");
                        button.GetComponent<QuestListButton>().SetIsNull(true);
                    }

                    button.transform.SetParent(m_buttonTemplate.transform.parent, false);

                }
            }
        }
        catch (System.NullReferenceException)
        {
          
        }
    }

    private void SetDefaults()
    {
        m_description.text = QuestManager.Instance.m_quests[0].GetObjective();
    }

    public void ButtonClicked(string _name, string _description)
    {
        m_description.text = _description;
    }
}
