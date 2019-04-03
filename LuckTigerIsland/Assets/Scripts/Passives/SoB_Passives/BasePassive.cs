using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Passive", menuName = "Passive/StartOfBattle")]
public class BasePassive : ScriptableObject
{
    public enum PassiveType
    {
        eAll,
        eStrength,
        eHealth,
        eMagic
    }
    public PassiveType passiveType;

    public int passiveAmount;

}
