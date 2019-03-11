using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class SkillsUi : Selectable,  ISelectHandler
{
    PlayerEntity m_playerEntity = null;
    BaseEventData m_baseEvent = null;
    public TextMeshProUGUI skillDescriptionText;
    public TextMeshProUGUI skillTitleText;
    public BaseAttack attack;
    public GameObject m_skillImages;
    private string m_skillName = null;
    // Use this for initialization
    protected override void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        m_skillName = this.gameObject.name;
        if (IsHighlighted(m_baseEvent) == true)
        { 
            if (this.gameObject.name == m_skillName)
            {
                skillDescriptionText.text = attack.attackDescription;
                skillTitleText.text = attack.attackName;
            }
        }
    }
}
