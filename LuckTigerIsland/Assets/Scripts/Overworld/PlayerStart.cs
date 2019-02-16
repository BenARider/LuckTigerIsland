using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SceneManager.LoadScene("TestScene", LoadSceneMode.Additive);
		Destroy(this);
	}
}
