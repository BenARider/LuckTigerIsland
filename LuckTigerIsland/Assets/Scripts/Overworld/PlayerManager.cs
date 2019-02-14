﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Interaction
{
    public string text;
    public GameObject obj;
    public InteractionType type;
    public Interaction(GameObject _obj, InteractionType _type, string _text)
    {
        obj = _obj;
        type = _type;
        text = _text;
    }
}


[RequireComponent(typeof(PlayerWorldMove))]
[RequireComponent(typeof(EntityLayerCalc))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

//manages player interaction
public class PlayerManager : MonoBehaviour {
    //list of all current nearby interactable scripts
    public List<Interaction> interactions;
    PlayerWorldMove playerMove;
    

    bool inDialogue = false;
    NPCDialogue activeDialogue;
    

	// Use this for initialization
	void Start () {
        playerMove = GetComponent<PlayerWorldMove>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!inDialogue)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interactions.Count > 0)
                {
                    Interaction interact = interactions[interactions.Count - 1];
                    switch (interact.type)
                    {
                        case InteractionType.Dialogue:
                            NPCDialogue dia = interact.obj.GetComponent<NPCDialogue>();
                            if (dia)
                            {
                                EnterDialogue(dia);
                            }
                            break;
                        case InteractionType.Generic:
                            GenericInteract generic = interact.obj.GetComponent<GenericInteract>();
                            if (generic)
                            {
                                generic.OnInteract(this);
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            playerMove.doMove = false;
            if (activeDialogue)
            {
                //hardcoded input because im a special boy
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    OnDialogueInput(0);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    OnDialogueInput(1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    OnDialogueInput(2);
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    OnDialogueInput(3);
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    OnDialogueInput(4);
                }
            }

        }
    }


    void OnDialogueInput(int index)
    {
        if (activeDialogue.Dialogues[activeDialogue.currentDialogueIndex].Replies.Length > index)
        {
            activeDialogue.currentDialogueIndex = activeDialogue.Dialogues[activeDialogue.currentDialogueIndex].Replies[index].nextDialogue;
            if (activeDialogue.currentDialogueIndex == 0)
            {
                return;
            }
            ShowDialogue(activeDialogue.Dialogues[activeDialogue.currentDialogueIndex]);
        }
    }

    //call when player presses E or when NPC auto initiates dialog
    void EnterDialogue(NPCDialogue _dialogue)
    {
        activeDialogue = _dialogue;
        inDialogue = true;
        _dialogue.BeginDialogue();
        ShowDialogue(_dialogue.Dialogues[_dialogue.startDialogueIndex]);
    }

    //call each time the player is presented with new options / text
    void ShowDialogue(Dialogue _dialogue)
    {
        Debug.Log(_dialogue.DialogueText);
        for (int i = 0; i < _dialogue.Replies.Length; i++)
        {
            Debug.Log((i + 1) + ": " + _dialogue.Replies[i].ReplyText);//TEMP
        }

        if (_dialogue.Replies.Length == 0)
        {
            ExitDialogue();
        }
    }

    //call when an endpoint is reached
    void ExitDialogue()
    {
        activeDialogue = null;
        inDialogue = false;
        playerMove.doMove = true;
    }

    //whenever an interact enters or leaves the players area
    void OnListChanged()
    {
        if (interactions.Count > 0)
        {
            Interaction interact = interactions[interactions.Count - 1];
            Debug.Log("Press E to " + interact.text);//TEMP
            switch (interact.type)
            {
                case InteractionType.Dialogue:
                    //TODO: show prompt / activate Dialogue
                    break;
                case InteractionType.Generic:
                    //TODO: show prompt / ect
                    break;
            }
        }
    }


    public void AddInteract(GameObject _other, InteractionType _type, string _text)
    {
        interactions.Add(new Interaction(_other, _type, _text));
        OnListChanged();
    }

    public void LoseInteract(GameObject _other, InteractionType _type)
    {
        switch (_type)
        {
            case InteractionType.Dialogue:
                //TODO: close prompt
                break;
                //TODO: close prompt / ect.
            case InteractionType.Generic:
                break;
        }

        foreach (Interaction i in interactions)
        {
            if(i.obj == _other)
            {
                interactions.Remove(i);
                OnListChanged();
                return;
            }
        }
    }
}