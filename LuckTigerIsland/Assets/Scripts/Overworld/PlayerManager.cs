﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
public class PlayerManager : LTI.Singleton<PlayerManager> {
    //list of all current nearby interactable scripts
    public List<Interaction> interactions;
	public string currentSceneName;
    public PlayerWorldMove playerMove;
    TextMeshProUGUI[] textArray;

    bool inDialogue = false;
    Interaction lastDialogueInteract;
    NPCDialogue activeDialogue;

    // Use this for initialization
    void Start () {
        instance = this;

        playerMove = GetComponent<PlayerWorldMove>();

        Transform overUI = GameObject.Find("OverworldUI").transform;
       
        textArray = new TextMeshProUGUI[] {
        overUI.GetChild(0).GetComponent<TextMeshProUGUI>(), //interact
        overUI.GetChild(1).GetComponent<TextMeshProUGUI>(), //npc text
        overUI.GetChild(2).GetComponent<TextMeshProUGUI>(), //reply 1,2,3,4
        overUI.GetChild(3).GetComponent<TextMeshProUGUI>(),
        overUI.GetChild(4).GetComponent<TextMeshProUGUI>(),
        overUI.GetChild(5).GetComponent<TextMeshProUGUI>()};//
        
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
                                lastDialogueInteract = interact;
                                EnterDialogue(dia);
                            }
                            break;
                        case InteractionType.Generic:
                            GenericInteract generic = interact.obj.GetComponent<GenericInteract>();
                            if (generic)
                            {
                                generic.OnInteract(this);
								interactions.Remove(interact);
								textArray[0].text = "";
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

            //Activate Quest if one exists on the dialogue step.
            if (activeDialogue.Dialogues[activeDialogue.currentDialogueIndex].quest != null)
            {
                activeDialogue.Dialogues[activeDialogue.currentDialogueIndex].quest.StartQuest();
            }
            //

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

        textArray[1].text = _dialogue.DialogueText;

        for(int i = 2; i < 6; i++)
        {
            textArray[i].text = "";
        }
        

        if(activeDialogue.interactAudio != "")
        {
            AudioManager.Instance.PlaySound(activeDialogue.interactAudio);
        }

        for(int i = 0; i< _dialogue.Replies.Length; i++)
        {
            textArray[i+2].text = (i+1) + ": " + _dialogue.Replies[i].ReplyText;
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
            textArray[0].text = "Press E to " + interact.text;
            /*switch (interact.type)
            {
                case InteractionType.Dialogue:
                    //TODO: show prompt / activate Dialogue
                    break;
                case InteractionType.Generic:
                    //TODO: show prompt / ect
                    break;
            }*/
            bool hasLeft = true;
            foreach (Interaction checkInteraction in interactions)
            {
                if (checkInteraction.Equals(lastDialogueInteract))
                {
                    hasLeft = false;
                }
            }
            if (hasLeft)
            {
                textArray[1].text = "";
            }
        }
        else
        {
            textArray[0].text = "";
            if(!inDialogue) textArray[1].text = "";
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
