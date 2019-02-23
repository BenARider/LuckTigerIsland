using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MenuManager : MonoBehaviour
{
    public GameObject startButton;
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
        LoadAsyncScene();
        Debug.Log("Starting");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Closing");
    }
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation m_asyncLoad = SceneManager.LoadSceneAsync("Scene1");
        while (!m_asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
