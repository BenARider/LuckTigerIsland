using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TitleFade : MonoBehaviour {
    public TextMeshProUGUI titleScreenText;
    private Color m_fadeColour;
    public GameObject mainMenu;
    public GameObject background;
    public GameObject title;
    public GameObject maintitle;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("Fade");
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
        maintitle.SetActive(true);
        background.SetActive(true);
        title.SetActive(true);

    }
}
