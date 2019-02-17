using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterSubLevel : GenericInteract {
    public string sceneName;
	public Vector3 exitPos;
	public bool fromOverworld = false;
	public bool toOverworld = false;
     

    public override void OnInteract(PlayerManager player)
    {
        Debug.Log("Entering "+ sceneName);

		Camera.main.GetComponent<ScreenTransition>().fromBlack();

		if (!toOverworld)
		{
			SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
		}
		else
		{
			GameObject[] sceneobjects = SceneManager.GetSceneByName(sceneName).GetRootGameObjects();
			sceneobjects[sceneobjects.Length - 1].SetActive(true);
		}
		player.transform.position = exitPos;
		if (!fromOverworld) //always unload when travelling from a subscene, not from the overworld
		{
			SceneManager.UnloadSceneAsync(player.currentSceneName);
		}
		else
		{
			transform.root.gameObject.SetActive(false);
		}
		player.currentSceneName = sceneName;

		Camera.main.GetComponent<CameraController>().OnReset();
	}
}
