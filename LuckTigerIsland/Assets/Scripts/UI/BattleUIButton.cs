﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class BattleUIButton : MonoBehaviour,ISelectHandler
{
    private int m_actionTargetNumber;
    public TextMeshProUGUI m_skillDescription;
    private int m_currentSelectedAction;
    
    // Use this for initialization
    void Start()
    {
 


    }

    // Update is called once per frame
    void Update()
    {
  

    }
    public void OnSelect(BaseEventData _eventData)
    {
        if(this.gameObject.name == "Action_One")
        {
            m_currentSelectedAction = 1;
        }
        if (this.gameObject.name == "Action_Two")
        {
            m_currentSelectedAction = 2;
        }
        if (this.gameObject.name == "Action_Three")
        {
            m_currentSelectedAction = 3;
        }
        if (this.gameObject.name == "Action_Four")
        {
            m_currentSelectedAction = 4;
        }
        if (this.gameObject.name == "Action_Five")
        {
            m_currentSelectedAction = 5;

        }
        if (this.gameObject.name == "Action_Six")
        {
            m_currentSelectedAction = 6;

        }
        if (this.gameObject.name == "Action_Seven")
        {
            m_currentSelectedAction = 7;

        }
        if (this.gameObject.name == "Action_Eight")
        {
            m_currentSelectedAction = 8;

        }
        if (this.gameObject.name == "Action_Nine")
        {
            m_currentSelectedAction = 9;

        }

    }
    public int GetCurrentAction()
    {
        return m_currentSelectedAction;
    }
    public void SetTargetActionNumber(int _number)
    {
        m_actionTargetNumber = _number;
    }
	public void ResetTargetActionNumber()
	{
		m_actionTargetNumber = 0;
	}
    public int GetActionTargetNumber()
    {
        return m_actionTargetNumber;
    }

}






