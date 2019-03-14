using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MenuManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject MainMenuCanvas;
    EventSystem m_eventSystem;
    // Use this for initialization
    void Start()
    {
        m_eventSystem = EventSystem.current;
        //m_eventSystem.SetSelectedGameObject(startButton);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
        MainMenuCanvas.SetActive(false);
        Debug.Log("Starting");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Closing");
    }
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation m_asyncLoad = SceneManager.LoadSceneAsync("Overworld");
        while (!m_asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
