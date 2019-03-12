using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Music
{
    [HideInInspector]
    private float m_trackLength;

    [SerializeField]
    protected string m_name;
    [SerializeField]
    protected AudioClip m_audioClip;
    [SerializeField]
    protected AudioMixerGroup m_audioMixer;

    [Range(0f, 1f), SerializeField]
    protected float m_volume = 1;

    [Range(-3f, 3f), SerializeField]
    protected float m_pitch = 1;

    [SerializeField]
    protected bool m_loop = false;
    [SerializeField]
    protected bool m_mute = false;

    protected AudioSource m_audioSource;

    public void Stop()
    {
        m_audioSource.Stop();
    }

    //Mute, Change Volume, and Change Pitch are used to change sound settings during runtime.
    public void Mute(bool _mute)
    {
        m_audioSource.mute = _mute;
    }

    public void ChangeVolume(float _volume)
    {
        m_audioSource.volume = _volume;
    }

    public void ChangePitch(float _pitch)
    {
        m_audioSource.pitch = _pitch;
    }

    public void Loop(bool _loop)
    {
        m_loop = _loop;
        m_audioSource.loop = _loop;
    }

    public void SetAudioSource(AudioSource _source)
    {
        m_audioSource = _source;
        m_audioSource.clip = m_audioClip;
    }

    public string GetName()
    {
        return m_name;
    }
    public bool GetIsLooped()
    {
        return m_loop;
    }

    //Overloaded from sound to update trackLength.
    public void Play()
    {
        m_trackLength = m_audioSource.clip.length;
        m_volume = 1f;
        m_pitch = 1f;
        m_audioSource.outputAudioMixerGroup = m_audioMixer;
        m_audioSource.volume = m_volume;
        m_audioSource.pitch = m_pitch;
        m_audioSource.loop = m_loop;
        m_audioSource.mute = m_mute;
        m_audioSource.Play();
    }

    public float GetTrackLength()
    {
        return m_trackLength;
    }
}