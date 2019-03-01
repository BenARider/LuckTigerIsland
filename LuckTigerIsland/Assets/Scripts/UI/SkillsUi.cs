using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class SkillsUi : Selectable,  ISelectHandler
{
    BaseEventData m_baseEvent = null;
    public TextMeshProUGUI skillDescriptionText;
    public TextMeshProUGUI skillTitleText;
    public List<BaseAttack> attacks = new List<BaseAttack>();
    // Use this for initialization
    protected override void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (IsHighlighted(m_baseEvent) == true)
        {
            if (this.gameObject.name == "Slash")
            {
                skillDescriptionText.text = attacks[0].attackDescription;
                skillTitleText.text = attacks[0].attackName;
            } 
            if (this.gameObject.name == "Bash")
            {
                skillDescriptionText.text = attacks[1].attackDescription; 
                skillTitleText.text = attacks[1].attackName;
            }
            if (this.gameObject.name == "Fireball")
            {
                skillDescriptionText.text = attacks[2].attackDescription;
                skillTitleText.text = attacks[2].attackName;
            }  
            if(this.gameObject.name == "Ice Lance")
            {
                skillDescriptionText.text = attacks[3].attackDescription;
                skillTitleText.text = attacks[3].attackName;
            } 
            if(this.gameObject.name == "Clear Text")
            {
                skillDescriptionText.text = "";
                skillTitleText.text = "";
            }
        }
    }
}
