using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteract : InteractEvent
{

    public override void Interact(int argID)
    {
        PlayerManager.Instance.GetComponent<EncounterManager>().DoEncounter(argID+3);
    }
}
