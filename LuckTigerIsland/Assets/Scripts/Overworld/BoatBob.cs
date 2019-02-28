using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBob : MonoBehaviour {
	public Transform boat;


	// Update is called once per frame
	void Update () {
		boat.position = boat.position + new Vector3(0.0f, Mathf.Sin((Time.time + transform.position.x)*2) /500, 0.0f); ;

		
	}
}
