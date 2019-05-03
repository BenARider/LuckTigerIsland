using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class MenuOpen : MonoBehaviour {
    public GameObject PauseMenu;
    public GameObject PartyMenu;
    public GameObject ResumeButton;
    public GameObject PartyButton;
    private GameObject QuestAddText;
    [SerializeField]
    private bool m_battleUIOpen;
    [SerializeField]
    private GameObject m_lastButton;
    Transform m_actionListHolder;
    [SerializeField]
    private GameObject m_shopUI;
    [SerializeField]
    private GameObject m_ActionListHolder;
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
                m_actionListHolder = GameObject.Find("Action_List_Holder").transform;
                for (int i = 0; i < m_actionListHolder.childCount; ++i)
                {
                    m_actionListHolder.GetChild(i).gameObject.SetActive(false);

                }
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

                m_actionListHolder = GameObject.Find("Action_List_Holder").transform;
                for (int i = 0; i < m_actionListHolder.childCount; ++i)
                {
                    m_actionListHolder.GetChild(i).gameObject.SetActive(false);

                }
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
        m_shopUI = GameObject.Find("Shop_UI");
        m_ActionListHolder = GameObject.Find("Action_List_Holder");
        if (m_shopUI.activeSelf == true)
        {
            m_shopUI.SetActive(false);
        }

        m_ActionListHolder.SetActive(false);



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
        m_actionListHolder = GameObject.Find("Action_List_Holder").transform;
        for (int i = 0; i < m_actionListHolder.childCount; ++i)
        {
            m_actionListHolder.GetChild(i).gameObject.SetActive(true);

        }
        m_menuPauseOpen = false;
        m_partyMenuOpen = false;
        m_eventSystem.SetSelectedGameObject(m_lastButton);
        PauseMenu.SetActive(false);
        PartyMenu.SetActive(false);
        QuestAddText.SetActive(true);
        m_shopUI.SetActive(true);

    }
   void Pause()
    {
        PauseMenu.SetActive(true);
        m_menuPauseOpen = true;
        m_shopUI = GameObject.Find("Shop_UI");
        if (m_shopUI.activeSelf == true)
        {
            m_shopUI.SetActive(false);
        }


    }

}
