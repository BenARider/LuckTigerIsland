using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put on a trigger to allow the player to interact with it
public class InteractZone : MonoBehaviour {
    public InteractionType type;
    public string interactionText = "interact";
    //object to do "getcomponent" on to get interaction script
    public GameObject scriptObject;

    void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.GetComponent<PlayerManager>())
        {
            _collision.GetComponent<PlayerManager>().AddInteract(scriptObject, type, interactionText);
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.GetComponent<PlayerManager>())
        {
            _collision.GetComponent<PlayerManager>().LoseInteract(scriptObject, type);
        }
    }
}
