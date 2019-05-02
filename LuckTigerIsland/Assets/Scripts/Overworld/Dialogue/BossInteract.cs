using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteract : InteractEvent
{

    public override void Interact(int argID)
    {
        PlayerManager.Instance.GetComponent<EncounterManager>().DoEncounter(argID+3);

        IEnumerator enumerator = doDestroy();
        StartCoroutine(enumerator);
       
    }

    IEnumerator doDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
