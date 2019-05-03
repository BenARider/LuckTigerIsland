using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideActions : MonoBehaviour {
    MenuOpen m_menuOpen;
	// Use this for initialization
	void Start () {
        m_menuOpen = GameObject.Find("PauseSystemHolder").GetComponent<MenuOpen>();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_menuOpen.GetPauseMenuState() == true && m_menuOpen.GetPartyMenuState() == false)
        {
           
            for (int i = 0; i < this.transform.childCount; ++i)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);

            }


        }
        if (m_menuOpen.GetPauseMenuState() == false && m_menuOpen.GetPartyMenuState() == true)
        {

            for (int i = 0; i < this.transform.childCount; ++i)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);

            }


        }
        if (m_menuOpen.GetPauseMenuState() == false && m_menuOpen.GetPartyMenuState() == false)
        {

            for (int i = 0; i < this.transform.childCount; ++i)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);

            }


        }
    }
}
