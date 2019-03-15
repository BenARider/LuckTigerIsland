using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour {

	public string mainName = "Overworld";
	public Vector3 lastOverworldPos;

	// Use this for initialization
	void Start () {
		
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
				if (PlayerManager.Instance.currentSceneName == "BattleScene"){
					SceneManager.UnloadSceneAsync(PlayerManager.Instance.currentSceneName);
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

				PlayerManager.Instance.playerMove.doMove = true;
				PlayerManager.Instance.transform.position = lastOverworldPos;
				PlayerManager.Instance.currentSceneName = mainName;
				PlayerManager.Instance.gameObject.transform.root.GetChild(0).gameObject.SetActive(true);

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
	}
}
