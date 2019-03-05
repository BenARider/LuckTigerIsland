using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    public string[] loadAsyncScenes;

	// Use this for initialization
	void Start () {
		
        foreach(string sce in loadAsyncScenes)
        {
            SceneManager.LoadSceneAsync(sce, LoadSceneMode.Additive);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
