using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIButton : MonoBehaviour
{
    private int m_actionTargetNumber;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
          
    }
    public void SetTargetActionNumber(int _number)
    {
        m_actionTargetNumber = _number;
    }
    public int GetActionTargetNumber()
    {
        return m_actionTargetNumber;
    }

}






