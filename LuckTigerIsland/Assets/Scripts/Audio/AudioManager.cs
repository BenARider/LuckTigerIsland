/*
Have one audio master object per scene using the prefab. Create sounds in the audio master object inspector. 
Reference the audio manager script using the audio manager object in other objects to access and play sounds.
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
    public AudioMixerGroup audioMixer;

    [Range(0f, 1f)]
    public float volume;

    [Range(-3f, 3f)]
    public float pitch;

    public bool loop = false;
    public bool mute = false;

    protected AudioSource m_audioSource;

    //Functions
    public void Play()
    {
        volume = 1f;
        pitch = 1f;
        m_audioSource.outputAudioMixerGroup = audioMixer;
        m_audioSource.volume = volume;
        m_audioSource.pitch = pitch;
        m_audioSource.loop = loop;
        m_audioSource.mute = mute;
        m_audioSource.Play();
    }

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
        loop = _loop;
        m_audioSource.loop = _loop;
    }

    public void SetAudioSource(AudioSource _source)
    {
        m_audioSource = _source;
        m_audioSource.clip = audioClip;
    }
}


//Possibly have Music and Sound both inherit from an abstract "Audio" class instead of Music Inheriting from Sound.
[System.Serializable]
public class Music : Sound
{
    [HideInInspector]
    public float trackLength;

    //Overloaded from sound to update trackLength.
    public new void Play()
    {
        trackLength = m_audioSource.clip.length;
        volume = 1f;
        pitch = 1f;
        m_audioSource.outputAudioMixerGroup = audioMixer;
        m_audioSource.volume = volume;
        m_audioSource.pitch = pitch;
        m_audioSource.loop = loop;
        m_audioSource.mute = mute;
        m_audioSource.Play();
    }
}

[System.Serializable]
public class MusicPlaylist
{
    [SerializeField]
    public string playlistName;
    [SerializeField]
    public Music[] m_music;
}

public class AudioManager : LTI.Singleton<AudioManager>{       

    //Identify which playlist to use.
    [SerializeField]
    public int currentPlaylist = 0;

    [SerializeField]
    Sound[] m_sounds;
    [SerializeField]
    MusicPlaylist[] m_playlists;
    
    //To store which track is currently playing and how long it has left.
    int m_currentMusicTrack = 0;
    float m_currentTrackTimeRemaining = 0;

    void Start()
    {
        instance = this;
        //Sounds
        for(int i = 0; i < m_sounds.Length; i++) { 
            //To stop duplicates in sound name.
            for(int j = i+1; j < m_sounds.Length; j++)
            {
                if(m_sounds[i].name == m_sounds[j].name)
                {
                    throw new System.Exception("Error! Sound " + i + " and sound " + j + " have the same name.");
                }
            }

            //Create Sound Objects from array.
            GameObject _so = new GameObject("Sound " + i + ": " + m_sounds[i].name);
            _so.transform.SetParent(this.transform);
            m_sounds[i].SetAudioSource(_so.AddComponent<AudioSource>());
        }

        //Music
        if (currentPlaylist >= m_playlists.Length)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            throw new System.IndexOutOfRangeException(sceneName + ": 'currentPlaylist' int is higher than the amount of playlists.");
        }
        
        for (int i = 0; i < m_playlists[currentPlaylist].m_music.Length; i++)
        {
            //To stop duplicates in music name.
            for (int j = i + 1; j < m_playlists[currentPlaylist].m_music.Length; j++)
            {
                if (m_playlists[currentPlaylist].m_music[i].name == m_playlists[currentPlaylist].m_music[j].name)
                {
                    throw new System.Exception("Error! Music file " + i + " and music " + j + " have the same name.");
                }
            }


            //Create Sound Objects from array.
            GameObject _mo = new GameObject("Music " + i + ": " + m_playlists[currentPlaylist].m_music[i].name);
            _mo.transform.SetParent(this.transform);
            m_playlists[currentPlaylist].m_music[i].SetAudioSource(_mo.AddComponent<AudioSource>());
        }
        
        PlayMusic();
    }

    //Music Functions
    //Using a couroutine to constantly check remaining time so the audio manager can still do other things at the same time.
    IEnumerator RemainingTrack()
    {
        //Lower remaning track time and log it until it hits 0.
        while (m_currentTrackTimeRemaining > 0)
        {
            m_currentTrackTimeRemaining -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }        
        NextTrack();        
    }

    public void PlayMusic()
    {
        //Error checking
        if(m_playlists[currentPlaylist].m_music.Length == 0)
        {
            Debug.LogError("Music array is empty!");
            return;
        }

        //Play the first track in the array and get its track length.
        m_playlists[currentPlaylist].m_music[m_currentMusicTrack].Play();
        m_currentTrackTimeRemaining = m_playlists[currentPlaylist].m_music[m_currentMusicTrack].trackLength;

        //To countdown until the track ends.
        StartCoroutine("RemainingTrack");
    }    

    public void NextTrack()
    {
        //Tidy up previous track by stopping the coroutine and the current track.
        StopCoroutine("RemainingTrack");
        m_playlists[currentPlaylist].m_music[m_currentMusicTrack].Stop();

        //If the current track is set to loop, play it again. If not, go to the next track. If there is no next track in the array, go back to the first track.
        if (m_playlists[currentPlaylist].m_music[m_currentMusicTrack].loop)
        {
            PlayMusic();
            return;
        }
        if (m_currentMusicTrack == m_playlists[currentPlaylist].m_music.Length -1)
        {
            m_currentMusicTrack = 0;
        } else
        {
            m_currentMusicTrack++;
        }
        //Play Next track.
        PlayMusic();
    }

    //Sound Functions
    public void PlaySound (string _name) 
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if(m_sounds[i].name == _name)
            {
                m_sounds[i].Play();
                return;
            }
        }
        //Reaching here means no sound with the name was found.
        Debug.LogError(_name + " sound does not exist!");
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].name == _name)
            {
                m_sounds[i].Stop();
                return;
            }
        }
        Debug.LogError(_name + " sound does not exist!");
    }

    public void MuteSound(string _name, bool _mute)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].name == _name)
            {
                m_sounds[i].Mute(_mute);
                return;
            }
        }
        Debug.LogError(_name + " sound does not exist!");
    }

    public void ChangeSoundVolume(string _name, float _volume)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].name == _name)
            {
                m_sounds[i].ChangeVolume(Mathf.Clamp(_volume, 0f, 1f));

                return;
            }
        }
        Debug.LogError(_name + " sound does not exist!");
    }

    public void ChangeSoundPitch(string _name, float _pitch)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].name == _name)
            {
                m_sounds[i].ChangePitch(Mathf.Clamp(_pitch, -3f, 3f));
                return;
            }
        }
        Debug.LogError(_name + " sound does not exist!");
    }
}

/*Info on Sound Import Settings
 * Music / Ambiance:
 *  Load Type: Use either "Streaming" or "Compressed in Memory"
 *  Streaming uses less memory but requires higher CPU power and disk I/O.
 *  Compressed in Memory uses less disk I/O but higher memory. If using this option, change sound quality to around 70.
 *  Compression Format: Vorbis
 * 
 * Sound Effects:
 *  Frequently Played and short clips should use Load Type "Decompress on Load" and Compression Format "PCM".
 *  Frequently Played and medium clips should use Load Type "Compressed in Memory" and Compression Format "ADPCM".
 *  Rarely Played and short clips should use Load Type "Compressed in Memory" and Compression Format "ADPCM".
 *  Rarely Played and medium clips should use Load Type "Compressed In Memeory" and Compression Format "Vorbis".
*/