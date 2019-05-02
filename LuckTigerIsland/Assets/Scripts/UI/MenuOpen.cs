using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class MenuOpen : MonoBehaviour {
    public GameObject PauseMenu;
    public GameObject PartyMenu;
    public GameObject ResumeButton;
    public GameObject PartyButton;
    private GameObject QuestAddText;

    [SerializeField]
    private GameObject m_lastButton;
    EventSystem m_eventSystem;
    [SerializeField]
    private bool m_menuPauseOpen = false;
    [SerializeField]
    private bool m_partyMenuOpen = false;

	// Use this for initialization
	void Start () {
        m_eventSystem = EventSystem.current;
      
    }
    // Update is called once per frame
    private void OnEnable()
    {
       
        QuestAddText = GameObject.Find("QuestCompleteText");
    
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& !m_partyMenuOpen)
        {
            if(m_menuPauseOpen)
            {
                Resume();

            }
            else
            {
                m_lastButton = EventSystem.current.currentSelectedGameObject;
                m_eventSystem.SetSelectedGameObject(ResumeButton);
            Pause();
                
            }
        }
        if(Input.GetKeyDown(KeyCode.I) && !m_menuPauseOpen)
        {
            if(m_partyMenuOpen)
            {
                OpenPartyMenu();
            }
            else
            {
            
                m_lastButton = EventSystem.current.currentSelectedGameObject;
                m_eventSystem.SetSelectedGameObject(PartyButton);
            OpenPartyMenu();
            }
        }
    }
    public void OpenPartyMenu()
    {
        PartyMenu.SetActive(true);
        m_partyMenuOpen = true;
    }
    public bool GetPartyMenuState()
    {
        return m_partyMenuOpen;

    }
    public bool GetPauseMenuState()
    {
        return m_menuPauseOpen;

    }
    public void Resume()
    {
        m_menuPauseOpen = false;
        m_partyMenuOpen = false;
        m_eventSystem.SetSelectedGameObject(m_lastButton);
        PauseMenu.SetActive(false);
        PartyMenu.SetActive(false);
        QuestAddText.SetActive(true);
   
    }
   void Pause()
    {
        PauseMenu.SetActive(true);
        m_menuPauseOpen = true;
    }

}
