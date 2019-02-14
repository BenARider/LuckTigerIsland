using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MenuOpen : MonoBehaviour {
    public GameObject inventoryMenu;
    public GameObject arrow;
    EventSystem m_eventSystem;
	// Use this for initialization
	void Start () {
        inventoryMenu.SetActive(false);
        m_eventSystem = EventSystem.current;
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
        {
            inventoryMenu.SetActive(true);
            m_eventSystem.SetSelectedGameObject(arrow);
        }
	}
}
