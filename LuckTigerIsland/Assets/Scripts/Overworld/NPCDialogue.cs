using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
	Dialogue, Generic
}

[System.Serializable]
public class DialogueReply
{
	public string ReplyText;
	public int nextDialogue;
}

[System.Serializable]
public class Dialogue
{

	[HideInInspector]
	public bool isEndPoint = false;
	public Quest questHandout;
    public Quest questForDialogueObj;
	public int interactArgs;
	public InteractEvent interactEvent;
	public string DialogueText;
	public DialogueReply[] Replies;
    public int index = 0;
    public bool hasQuest = false;

    public void OnEnable()
    {
        Debug.Log("fffff ");
        if (hasQuest)
        {
            if (QuestManager.Instance)
            {
                Debug.Log("gets " + QuestManager.Instance.transform.GetChild(index).name);
                questHandout = QuestManager.Instance.transform.GetChild(index).GetComponent<Quest>();
            }
            else
            {
                Debug.Log("no quest :c ");
            }
        }
    }

	public Dialogue(bool _isEnd = false)
	{
		isEndPoint = _isEnd;
		if (isEndPoint)
		{
			DialogueText = "ENDPOINT DO NOT TOUCH";
		}
	}
}

public class NPCDialogue : MonoBehaviour
{
	//The Dialogue element that will come up first. Always the dialogue after the endpoint.
	public int startDialogueIndex = 1;
	public int currentDialogueIndex;
	public string interactAudio = "";
	public List<Dialogue> Dialogues = new List<Dialogue> { new Dialogue(true), new Dialogue() };

	public void BeginDialogue()
	{
		currentDialogueIndex = startDialogueIndex;
	}

    private void OnEnable()
    {
        foreach(Dialogue d in Dialogues)
        {
            d.OnEnable();
        }
    }
}
