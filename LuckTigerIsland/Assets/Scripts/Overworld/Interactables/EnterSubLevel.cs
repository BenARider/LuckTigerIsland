using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterSubLevel : GenericInteract
{
    public string sceneName;
    public Vector3 exitPos;
    public bool fromOverworld = false;
    public bool toOverworld = false;

    float transitionTimer = 0f;
    bool doTransition = false;


    private void OnEnable()
    {
        transitionTimer = 0f;
    }

    private void Update()
    {
        if (doTransition)
        {
            transitionTimer += Time.deltaTime;
            PlayerManager.Instance.playerMove.doMove = false;
            if (transitionTimer >= 1f)
            {
                if (SceneManager.GetSceneByName(sceneName).isLoaded)
                {
                    GameObject[] sceneobjects = SceneManager.GetSceneByName(sceneName).GetRootGameObjects();
                    foreach (GameObject go in sceneobjects)
                    {
                        if (go.name.Equals("Level"))
                        {
                            go.SetActive(true);
                        }
                    }
                    PlayerManager.Instance.playerMove.doMove = true;
                    PlayerManager.Instance.transform.position = exitPos;
                    PlayerManager.Instance.currentSceneName = sceneName;

                    Camera.main.GetComponent<ScreenTransition>().fromBlack();
                    Camera.main.GetComponent<CameraController>().OnReset();

                    doTransition = false;
                    transitionTimer = 0;
                    transform.root.gameObject.SetActive(false);
                }
            }
        }
    }

    public override void OnInteract(PlayerManager player)
    {
        Debug.Log("Entering " + sceneName);

        Camera.main.GetComponent<ScreenTransition>().toBlack();

        doTransition = true;
    }
}
 
