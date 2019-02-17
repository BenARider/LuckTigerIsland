using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour {

	public Image fadeBox;
	float transitionValue = 0;
	float alphaAmount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		alphaAmount += transitionValue * Time.deltaTime;
		alphaAmount = Mathf.Clamp(alphaAmount, 0, 1);

		fadeBox.color = new Color(0, 0, 0, alphaAmount);
	}

	public void toBlack()
	{
		transitionValue = 1;
		alphaAmount = 0;
	}
	public void fromBlack()
	{
		transitionValue = -1;
		alphaAmount = 1;
	}
}
