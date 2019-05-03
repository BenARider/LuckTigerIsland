using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfQuestIsCompleted : InteractEvent {

    [SerializeField]
    Quest prereqQuest;
    [SerializeField]
    Quest handoutQuest;

    public override void Interact(int argID)
    {
        if(prereqQuest.hasBeenAccepted && !prereqQuest.isActive)
        {
            if (!handoutQuest.hasBeenAccepted)
            {
                handoutQuest.StartQuest();
            }
        }
    }

}
