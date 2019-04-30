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
    public GameObject BattleUI;
    public GameObject ActionOneButton;
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
            m_eventSystem.SetSelectedGameObject(ResumeButton);
            Pause();
                BattleUI = GameObject.Find("Action_List_Holder");
                BattleUI.SetActive(false);
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
            m_eventSystem.SetSelectedGameObject(PartyButton);
            OpenPartyMenu();
                BattleUI = GameObject.Find("Action_List_Holder");
                BattleUI.SetActive(false);
            }
        }
    }
    public void OpenPartyMenu()
    {
        PartyMenu.SetActive(true);
        m_partyMenuOpen = true;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        PartyMenu.SetActive(false);
        BattleUI.SetActive(true);
        ActionOneButton = GameObject.Find("Action_One");
        m_eventSystem.SetSelectedGameObject(ActionOneButton);
        m_menuPauseOpen = false;
        m_partyMenuOpen = false;
    }
   void Pause()
    {
        PauseMenu.SetActive(true);
        m_menuPauseOpen = true;
    }

}
