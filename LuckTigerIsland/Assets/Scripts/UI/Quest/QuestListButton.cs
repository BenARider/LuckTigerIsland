using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
public class QuestListButton : MonoBehaviour,ISelectHandler {


    [SerializeField]
    private TextMeshProUGUI m_text;
    [SerializeField]
    private QuestListControl m_questControl;

    private string m_name;
    private string m_description;


    private bool isNull = false;

    public void SetText(string _text)
    {
        m_text.text = _text;
        m_name = _text;
    }
    public void SetDescription(string _desc)
    {
        m_description = _desc;
    }
    public void SetIsNull(bool _b)
    {
        isNull = _b;
    }


    public void OnSelect(BaseEventData _data)
    {
        if (!isNull)
        {
            m_questControl.ButtonClicked(m_name, m_description);
        }
        else
        {
            Debug.LogError("Inventory Slot has nothing in it!");
        }
    }
}
