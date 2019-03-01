using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class MenuOpen : MonoBehaviour {
    public GameObject Menu;
    public GameObject Buttons;
    public GameObject ResumeButton;
    EventSystem m_eventSystem;
    [SerializeField]
    private bool m_menuOpen = false;

	// Use this for initialization
	void Start () {
        m_eventSystem = EventSystem.current;
      //  m_eventSystem.SetSelectedGameObject(ResumeButton);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !m_menuOpen)
        {
            m_eventSystem.SetSelectedGameObject(ResumeButton);
                Pause();
        }
    }
    public void Resume()
    {
        Menu.SetActive(false);
        Buttons.SetActive(false);
        m_menuOpen = false;
    }
   void Pause()
    {
        Menu.SetActive(true);
        Buttons.SetActive(true);
        m_menuOpen = true;
    }

}
