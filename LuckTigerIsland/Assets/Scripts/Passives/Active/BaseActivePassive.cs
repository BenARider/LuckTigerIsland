using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Passive", menuName = "Passive/Active")]
public class BaseActivePassive : BasePassive
{
    //public bool passiveActive = false;

    public enum PassiveCondition
    {
        eAll,
        eStrength,
        eHealth,
        eMagic
    }
    public PassiveCondition passiveConditionType;

    public enum LessThanOrMoreThan
    {
        eLessThan,
        eMoreThan
    }
    public LessThanOrMoreThan lessThanOrMoreThan;
    public int passiveCondtitionAmount;


    //public enum PassiveType
    //{
    //    eAll,
    //    eStrength,
    //    eHealth,
    //    eMagic
    //}

    //public PassiveType passiveType;

    //public int passiveAmount;

}

