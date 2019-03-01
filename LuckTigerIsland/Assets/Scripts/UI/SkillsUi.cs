using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class SkillsUi : Selectable, ISelectHandler
{
    BaseEventData m_baseEvent = null;
    public TextMeshProUGUI skillDescriptionText;
    public TextMeshProUGUI skillTitleText;
    // Use this for initialization
    protected override void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsHighlighted(m_baseEvent) == true)
        {
            if (this.gameObject.name == "Stone Armour")
            {
                skillDescriptionText.text = "Increases the party members physical defence";
                skillTitleText.text = this.gameObject.name;
            }
            if (this.gameObject.name == "Heal")
            {
                skillDescriptionText.text = "A personal Heal";
                skillTitleText.text = this.gameObject.name;
            }
            if(this.gameObject.name == "Lighting Bolt")
            {
                skillDescriptionText.text = "Strikes a single target";
                skillTitleText.text = this.gameObject.name;
            }
            if(this.gameObject.name == "Clear Text")
            {
                skillDescriptionText.text = "";
                skillTitleText.text = "";
            }
        }
    }
}
