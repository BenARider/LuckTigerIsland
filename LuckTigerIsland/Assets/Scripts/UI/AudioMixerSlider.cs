using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioMixerSlider : MonoBehaviour {
    public AudioMixer masterMixer;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetSoundMaster(float _soundLevel)
    {
        masterMixer.SetFloat("masterVol", _soundLevel);
    }
    public void SetSoundMusic(float _soundLevel)
    {
        masterMixer.SetFloat("musicVol", _soundLevel);
    }
    public void SetSoundSounds(float _soundLevel)
    {
        masterMixer.SetFloat("soundsVol", _soundLevel);
    }
}
