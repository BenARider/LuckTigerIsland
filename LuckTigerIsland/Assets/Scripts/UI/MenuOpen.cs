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
    private GameObject QuestAddText;
    [SerializeField]
    private GameObject m_ShopUI;
    [SerializeField]
    private GameObject m_closeShopButton;
    [SerializeField]
    private GameObject m_targetButton;
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
                Debug.Log("Last button was" + m_lastButton);
                m_eventSystem.SetSelectedGameObject(ResumeButton);
            Pause();
                
                //BattleUI.SetActive(false);
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
                Debug.Log("Last button was" + m_lastButton);
                m_eventSystem.SetSelectedGameObject(PartyButton);
            OpenPartyMenu();
           // BattleUI.SetActive(false);
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
        /*
        BattleUI = GameObject.Find("Battle_UI");
        m_targetButton = GameObject.Find("Target_One");
        ActionOneButton = GameObject.Find("Action_One");

        if (BattleUI.activeSelf == true)
        {
            m_eventSystem.SetSelectedGameObject(ActionOneButton);
            PauseMenu.SetActive(false);
            PartyMenu.SetActive(false);

        }
        if(m_targetButton.activeSelf == true)
        { 
            m_eventSystem.SetSelectedGameObject(m_targetButton);
            PauseMenu.SetActive(false);
            PartyMenu.SetActive(false);
        }
        m_ShopUI = GameObject.Find("Shop_UI");
        if(m_ShopUI.activeSelf == true)
        {
            m_closeShopButton = GameObject.Find("Close");
            m_eventSystem.SetSelectedGameObject(m_closeShopButton);
            PauseMenu.SetActive(false);
            PartyMenu.SetActive(false);
        }
        */
        PauseMenu.SetActive(false);
        PartyMenu.SetActive(false);
        //BattleUI.SetActive(true);
        QuestAddText.SetActive(true);
   
    }
   void Pause()
    {

        PauseMenu.SetActive(true);
        m_menuPauseOpen = true;
    }

}
