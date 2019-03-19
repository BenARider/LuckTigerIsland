using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour {

    public string[] loadAsyncScenes;
	public string startScene;
	public GameObject player;
	public TextMeshProUGUI text;
	public Image bar;
	public bool isLoaded = false;

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadScenes());
	}

	private void Update()
	{
		if (isLoaded)
		{
			Scene mainScene = SceneManager.GetSceneByName(startScene);
			SceneManager.SetActiveScene(mainScene);
			Instantiate(player);
			foreach (GameObject go in mainScene.GetRootGameObjects())
			{
				if (go.name.Equals("Level"))
				{
					go.SetActive(true);
				}
			}
			
			SceneManager.UnloadSceneAsync(gameObject.scene);
			Destroy(this);

		}
	}

	IEnumerator LoadScenes()
	{
		isLoaded = false;
		yield return null;
		float lastprogress = 0f;
		foreach (string sce in loadAsyncScenes)
		{
			
			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sce, LoadSceneMode.Additive);
			while (!asyncOperation.isDone)
			{
				//Output the current progress
				float progress = (asyncOperation.progress * 100 / loadAsyncScenes.Length) + lastprogress;
				text.text = progress + "%";
				bar.fillAmount = progress / 100;

				yield return null;
			}
			lastprogress += 100 / loadAsyncScenes.Length;
		}
		isLoaded = true;
	}
}
