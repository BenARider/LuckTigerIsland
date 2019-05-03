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
    [SerializeField]
    private GameObject m_battleUI;
    [SerializeField]
    private GameObject m_shopUI;
    [SerializeField]
    private Button[] m_BattleUIButtons;
    EventSystem m_eventSystem;
    [SerializeField]
    private bool m_menuPauseOpen = false;
    [SerializeField]
    private bool m_partyMenuOpen = false;

	// Use this for initialization
	void Start () {
        m_eventSystem = EventSystem.current;
        m_BattleUIButtons = new Button[9];
  
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
        m_shopUI = GameObject.Find("Shop_UI");
        if(m_shopUI.activeSelf == true)
        {
            m_shopUI.SetActive(false);
        }
        m_battleUI = GameObject.Find("Battle_UI");
        if (m_battleUI.activeSelf == true)
        {
            m_battleUIOpen = true;
            m_BattleUIButtons[0] = GameObject.Find("Action_One").GetComponent<Button>();
            m_BattleUIButtons[1] = GameObject.Find("Action_Two").GetComponent<Button>();
            m_BattleUIButtons[2] = GameObject.Find("Action_Three").GetComponent<Button>();
            m_BattleUIButtons[3] = GameObject.Find("Action_Four").GetComponent<Button>();
            m_BattleUIButtons[4] = GameObject.Find("Action_Five").GetComponent<Button>();
            m_BattleUIButtons[5] = GameObject.Find("Action_Six").GetComponent<Button>();
            m_BattleUIButtons[6] = GameObject.Find("Action_Seven").GetComponent<Button>();
            m_BattleUIButtons[7] = GameObject.Find("Action_Eight").GetComponent<Button>();
            m_BattleUIButtons[8] = GameObject.Find("Action_Nine").GetComponent<Button>();
            m_BattleUIButtons[0].interactable = false;
            m_BattleUIButtons[1].interactable = false;
            m_BattleUIButtons[2].interactable = false;
            m_BattleUIButtons[3].interactable = false;
            m_BattleUIButtons[4].interactable = false;
            m_BattleUIButtons[5].interactable = false;
            m_BattleUIButtons[6].interactable = false;
            m_BattleUIButtons[7].interactable = false;
            m_BattleUIButtons[8].interactable = false;
        }
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
        if (m_battleUIOpen == true)
        {
            m_BattleUIButtons[0].interactable = true;
            m_BattleUIButtons[1].interactable = true;
            m_BattleUIButtons[2].interactable = true;
            m_BattleUIButtons[3].interactable = true;
            m_BattleUIButtons[4].interactable = true;
            m_BattleUIButtons[5].interactable = true;
            m_BattleUIButtons[6].interactable = true;
            m_BattleUIButtons[7].interactable = true;
            m_BattleUIButtons[8].interactable = true;
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
        m_battleUI = GameObject.Find("Battle_UI");
        m_shopUI = GameObject.Find("Shop_UI");
        if (m_shopUI.activeSelf == true)
        {
            m_shopUI.SetActive(false);
        }
        if (m_battleUI.activeSelf == true)
        {
            m_battleUIOpen = true;
            m_BattleUIButtons[0] = GameObject.Find("Action_One").GetComponent<Button>();
            m_BattleUIButtons[1] = GameObject.Find("Action_Two").GetComponent<Button>();
            m_BattleUIButtons[2] = GameObject.Find("Action_Three").GetComponent<Button>();
            m_BattleUIButtons[3] = GameObject.Find("Action_Four").GetComponent<Button>();
            m_BattleUIButtons[4] = GameObject.Find("Action_Five").GetComponent<Button>();
            m_BattleUIButtons[5] = GameObject.Find("Action_Six").GetComponent<Button>();
            m_BattleUIButtons[6] = GameObject.Find("Action_Seven").GetComponent<Button>();
            m_BattleUIButtons[7] = GameObject.Find("Action_Eight").GetComponent<Button>();
            m_BattleUIButtons[8] = GameObject.Find("Action_Nine").GetComponent<Button>();
            m_BattleUIButtons[0].interactable = false;
            m_BattleUIButtons[1].interactable = false;
            m_BattleUIButtons[2].interactable = false;
            m_BattleUIButtons[3].interactable = false;
            m_BattleUIButtons[4].interactable = false;
            m_BattleUIButtons[5].interactable = false;
            m_BattleUIButtons[6].interactable = false;
            m_BattleUIButtons[7].interactable = false;
            m_BattleUIButtons[8].interactable = false;
        }
    }

}
