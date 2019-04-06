using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class TitleFade : MonoBehaviour {
    EventSystem m_eventSystem;
    public TextMeshProUGUI titleScreenText;
    private Color m_fadeColour;
    public GameObject mainMenu;
    public GameObject background;
    public GameObject mainTitle;
    public GameObject title;
    public GameObject startButton;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("Fade");
        m_eventSystem = EventSystem.current;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator Fade()
    {
        titleScreenText.CrossFadeColor(m_fadeColour, 3.0f, false, true);
        yield return new WaitForSeconds(3.0f);
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
        background.SetActive(true);
        mainTitle.SetActive(true);
        title.SetActive(true);
        m_eventSystem.SetSelectedGameObject(startButton);

    }
}
