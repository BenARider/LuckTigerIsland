using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour {

	public string mainName = "Overworld";
	public Vector3 lastOverworldPos;
    public Vector3 lastDungeonPos;
    public GameObject lastDungeon;
    public string lastDungeonName;

	// Use this for initialization
	void Start () {
		
	}
	
    public void Return()
    {
        if (PlayerManager.Instance.currentSceneName.Contains("Battle"))
        {
            SceneManager.UnloadSceneAsync(PlayerManager.Instance.currentSceneName);
            Debug.Log("Leaving Battle");
        }
        else
        {
            foreach (GameObject go in SceneManager.GetSceneByName(PlayerManager.Instance.currentSceneName).GetRootGameObjects())
            {
                if (go.name.Equals("Level"))
                {
                    go.SetActive(false);
                }
            }
        }

        bool isDungeon = false;
        if (PlayerManager.Instance.currentSceneName.Contains("CaveBattle"))
        {
            isDungeon = true;
            
            PlayerManager.Instance.transform.position = lastDungeonPos;
            PlayerManager.Instance.currentSceneName = lastDungeonName;
        }
        else
        {
            
            PlayerManager.Instance.transform.position = lastOverworldPos;
            PlayerManager.Instance.currentSceneName = mainName;
        }

        PlayerManager.Instance.playerMove.doMove = true;
        PlayerManager.Instance.gameObject.transform.root.GetChild(0).gameObject.SetActive(true);

        if (isDungeon)
        {
            lastDungeon.SetActive(true);



        }
        else
        {
            Scene mainScene = SceneManager.GetSceneByName(mainName);
            SceneManager.SetActiveScene(mainScene);
            foreach (GameObject go in mainScene.GetRootGameObjects())
            {
                if (go.name.Equals("Level"))
                {
                    go.SetActive(true);
                }
            }
        }
    }


	// Update is called once per frame
	void Update () {
		if (PlayerManager.Instance) {
			if (PlayerManager.Instance.currentSceneName == mainName)
			{
				lastOverworldPos = PlayerManager.Instance.transform.position;
			}
		}


		if (Input.GetKeyDown(KeyCode.R))
		{
			if(PlayerManager.Instance.currentSceneName != mainName)
			{
                Return();

            }
		}
	}
}
