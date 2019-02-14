using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EQuestState
{
    undiscovered,
    active,
    completed
};

[System.Serializable]
public class Quest : MonoBehaviour {

    private int m_ID;
    private string m_Name;
    private string m_Description;

    private EQuestState m_currentState;

    public int GetID()
    {
        return m_ID;
    }

    public string GetTitle()
    {
        return m_Name;
    }

    public string GetDescription()
    {
        return m_Description;
    }

    public EQuestState GetState()
    {
        return m_currentState;
    }

    public void SetState(EQuestState _state)
    {
        m_currentState = _state;
    }
}
