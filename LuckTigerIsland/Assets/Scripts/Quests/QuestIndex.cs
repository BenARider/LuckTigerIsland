using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIndex : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //
    void QuestLogic(ref Quest _quest)
    {
        switch (_quest.GetID())
        {
            case 0:
                break;

            default:
                Debug.Log("Quest " + _quest.GetTitle() + "'s ID is not used in QuestLogic function in QuestIndex class.");
                break;
        }
    }
}
