using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class RewardUI : Selectable, ISelectHandler
{
    BaseEventData m_baseEvent = null;
    private Color m_buttonColorNonSelect;
    private Color m_buttonColorSelect;
    private Image m_buttonHighlight;
    public TextMeshProUGUI ItemTitle;
    public TextMeshProUGUI ItemDescription;
    private Rewards rewards;
    // Use this for initialization
    protected override void Start()
    {
        m_buttonHighlight = GetComponent<Image>();
        m_buttonColorNonSelect = new Color(0.8f, 0.8f, 0.8f, 0.8f);
        m_buttonColorSelect = new Color(1f, 1f, 1f,1f);
        m_buttonHighlight.color = m_buttonColorNonSelect;
       
        rewards = GameObject.Find("RewardUI").GetComponent<Rewards>();
        ItemTitle.text = rewards.m_rewards[0].name;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsHighlighted(m_baseEvent) == true)
        {
            m_buttonHighlight.color = m_buttonColorSelect;

            ItemDescription.text = rewards.m_rewards[0].Description;
        }
        
        if (IsHighlighted(m_baseEvent) == false)
        {
            m_buttonHighlight.color = m_buttonColorNonSelect;

            ItemDescription.text = "";
        }
    }
}
