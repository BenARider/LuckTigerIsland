using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
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

    //Functions
    public void Play()
    {
        m_audioSource.outputAudioMixerGroup = m_audioMixer;
        m_audioSource.volume = m_volume;
        m_audioSource.pitch = m_pitch;
        m_audioSource.loop = m_loop;
        m_audioSource.mute = m_mute;
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
}


//Possibly have Music and Sound both inherit from an abstract "Audio" class instead of Music Inheriting from Sound.
[System.Serializable]
public class Music : Sound
{
    [HideInInspector]
    private float m_trackLength;

    //Overloaded from sound to update trackLength.
    public new void Play()
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

[System.Serializable]
public class MusicPlaylist
{
    [SerializeField]
    private string m_playlistName;
    [SerializeField]
    private Music[] m_music;

    public string GetPlaylistName()
    {
        return m_playlistName;
    }
    public Music[] GetMusic()
    {
        Music[] _m = m_music;
        return _m;
    }

    public void SetPlaylistName(string _name)
    {
        m_playlistName = _name;
    }
}

public class AudioManager : LTI.Singleton<AudioManager>{       

    //Identify which playlist to use.
    [SerializeField]
    private int currentPlaylist = 0;

    [SerializeField]
    private Sound[] m_sounds;
    [SerializeField]
    private MusicPlaylist[] m_playlists;
    
    //To store which track is currently playing and how long it has left.
    private int m_currentMusicTrack = 0;
    private float m_currentTrackTimeRemaining = 0;

    void Start()
    {
        instance = this;

        //Sounds
        for(int i = 0; i < m_sounds.Length; i++) { 
            //To stop duplicates in sound name.
            for(int j = i+1; j < m_sounds.Length; j++)
            {
                if(m_sounds[i].GetName() == m_sounds[j].GetName())
                {
                    throw new System.Exception("Error! Sound " + i + " and sound " + j + " have the same name.");
                }
            }

            //Create Sound Objects from array.
            GameObject _so = new GameObject("Sound " + i + ": " + m_sounds[i].GetName());
            _so.transform.SetParent(this.transform);
            m_sounds[i].SetAudioSource(_so.AddComponent<AudioSource>());
        }

        //Music
        if (currentPlaylist >= m_playlists.Length)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            throw new System.IndexOutOfRangeException(sceneName + ": 'currentPlaylist' int is higher than the amount of playlists.");
        }
        
        for (int i = 0; i < m_playlists[currentPlaylist].GetMusic().Length; i++)
        {
            //To stop duplicates in music name.
            for (int j = i + 1; j < m_playlists[currentPlaylist].GetMusic().Length; j++)
            {
                if (m_playlists[currentPlaylist].GetMusic()[i].GetName() == m_playlists[currentPlaylist].GetMusic()[j].GetName())
                {
                    throw new System.Exception("Error! Music file " + i + " and music " + j + " have the same name.");
                }
            }


            //Create Sound Objects from array.
            GameObject _mo = new GameObject("Music " + i + ": " + m_playlists[currentPlaylist].GetMusic()[i].GetName());
            _mo.transform.SetParent(this.transform);
            m_playlists[currentPlaylist].GetMusic()[i].SetAudioSource(_mo.AddComponent<AudioSource>());
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
        if(m_playlists[currentPlaylist].GetMusic().Length == 0)
        {
            Debug.LogError("Music array is empty!");
            return;
        }

        //Play the first track in the array and get its track length.
        m_playlists[currentPlaylist].GetMusic()[m_currentMusicTrack].Play();
        m_currentTrackTimeRemaining = m_playlists[currentPlaylist].GetMusic()[m_currentMusicTrack].GetTrackLength();

        //To countdown until the track ends.
        StartCoroutine("RemainingTrack");
    }    

    public void NextTrack()
    {
        //Tidy up previous track by stopping the coroutine and the current track.
        StopCoroutine("RemainingTrack");
        m_playlists[currentPlaylist].GetMusic()[m_currentMusicTrack].Stop();

        //If the current track is set to loop, play it again. If not, go to the next track. If there is no next track in the array, go back to the first track.
        if (m_playlists[currentPlaylist].GetMusic()[m_currentMusicTrack].GetIsLooped())
        {
            PlayMusic();
            return;
        }
        if (m_currentMusicTrack == m_playlists[currentPlaylist].GetMusic().Length -1)
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
            if(m_sounds[i].GetName() == _name)
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
            if (m_sounds[i].GetName() == _name)
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
            if (m_sounds[i].GetName() == _name)
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
            if (m_sounds[i].GetName() == _name)
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
            if (m_sounds[i].GetName() == _name)
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