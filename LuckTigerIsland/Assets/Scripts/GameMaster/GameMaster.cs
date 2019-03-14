using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameMaster : LTI.Singleton<GameMaster>{
    public float time = 0f;
    public bool isPaused = false;
    public float nightAlpha;

    string lastScene;
    public string currentScene;

    Tilemap currentNight;


    private void Start()
    {
        instance = this;// as GameMaster;
    }

    private void Update()
    {
        if (!isPaused)
        {
            time += Time.deltaTime * 0.1f;
            nightAlpha = Mathf.Sin(time);



            currentScene = PlayerManager.instance.currentSceneName;

            if (currentScene != lastScene)
            {
                

                GameObject[] sceneobjects = SceneManager.GetSceneByName(currentScene).GetRootGameObjects();
                foreach (GameObject go in sceneobjects)
                {
                    if (go.name.Equals("Level"))
                    {
                        

                        currentNight = go.transform.GetChild(0).GetChild(0).Find("Lights - Night").GetComponent<Tilemap>();
                        lastScene = currentScene;
                        break;
                    }
                }

            }
            if (currentNight)
            {
                currentNight.color = new Color(1, 1, 1, nightAlpha);
            }


            
        }
    }
}