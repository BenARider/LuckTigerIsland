using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScreenTransition : MonoBehaviour {
    
	public Image fadeBox;
	float fadeTransitionValue = 0;
	float fadeAlphaAmount = 0;

    public Image whiteBox;
    float whiteTransitionValue = 0;
    float whiteAlphaAmount = 0;
    float whiteDuration = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        fadeAlphaAmount += fadeTransitionValue * Time.deltaTime;
        fadeAlphaAmount = Mathf.Clamp(fadeAlphaAmount, 0, 1);
		fadeBox.color = new Color(0, 0, 0, fadeAlphaAmount);

        if (whiteDuration <= 0)
        {
            whiteAlphaAmount += whiteTransitionValue * Time.deltaTime;
        }
        else
        {
            whiteDuration -= Time.deltaTime;
        }
        whiteAlphaAmount = Mathf.Clamp(whiteAlphaAmount, 0, 1);
        whiteBox.color = new Color(1, 1, 1, whiteAlphaAmount);

    }

	public void toBlack()
	{
		fadeTransitionValue = 1;
		fadeAlphaAmount = 0;
	}
	public void fromBlack()
	{
        fadeTransitionValue = -1;
        fadeAlphaAmount = 1;
	}

    public void flashWhite(float _duration)
    {
        whiteAlphaAmount = 1;
        whiteTransitionValue = -1;
        whiteDuration = _duration;
    }

}
