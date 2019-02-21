using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MenuOpen : MonoBehaviour {
    public GameObject Menu;
    public GameObject Buttons;
    public GameObject arrow;
    EventSystem m_eventSystem;
	// Use this for initialization
	void Start () {
      
        m_eventSystem = EventSystem.current;
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
        {
            Menu.SetActive(true);
            Buttons.SetActive(true);
            m_eventSystem.SetSelectedGameObject(arrow);
        }
	}
}
