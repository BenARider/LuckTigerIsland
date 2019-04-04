using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FixMouseSelect : MonoBehaviour {
    GameObject m_lastSelect;
	// Use this for initialization
	void Start () {
        m_lastSelect = new GameObject();
	}
	
	// Update is called once per frame
	void Update () {
		if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(m_lastSelect);
        }
        else
        {
            m_lastSelect = EventSystem.current.currentSelectedGameObject;
        }
	}
}
