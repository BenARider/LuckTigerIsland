using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
public class ButtonSoundSelector : MonoBehaviour,ISelectHandler {
    public string buttonSelectSound;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnSelect(BaseEventData _data)
    {
        AudioManager.Instance.PlaySound(buttonSelectSound);
    }
}
