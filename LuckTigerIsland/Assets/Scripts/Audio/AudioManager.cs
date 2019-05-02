using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : LTI.Singleton<AudioManager>{       

    //Identify which playlist to use.
    [SerializeField]
    private int currentPlaylist = 0;

    [SerializeField]
    private List<Sound> m_sounds;
    [SerializeField]
    private List<MusicPlaylist> m_playlists;

    //To store which track is currently playing and how long it has left.
    private int m_currentMusicTrack = 0;
    private float m_currentTrackTimeRemaining = 0;

    void ChangePlaylist(int _index)
    {
        if (_index < 0 && _index > 9)
            Debug.LogError("Enter a playlist value between 0 and 9");
        else
            currentPlaylist = _index;
    }

    void Start()
    {
        instance = this;

        //Sounds
        for(int i = 0; i < m_sounds.Count; i++) { 
            //To stop duplicates in sound name.
            for(int j = i+1; j < m_sounds.Count; j++)
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
        if (currentPlaylist >= m_playlists.Count)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            throw new System.IndexOutOfRangeException(sceneName + ": 'currentPlaylist' int is higher than the amount of playlists.");
        }
        
        for (int i = 0; i < m_playlists[currentPlaylist].GetMusic().Count; i++)
        {
            //To stop duplicates in music name.
            for (int j = i + 1; j < m_playlists[currentPlaylist].GetMusic().Count; j++)
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
        if(m_playlists[currentPlaylist].GetMusic().Count == 0)
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
        if (m_currentMusicTrack == m_playlists[currentPlaylist].GetMusic().Count - 1)
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
        for (int i = 0; i < m_sounds.Count; i++)
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
        for (int i = 0; i < m_sounds.Count; i++)
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
        for (int i = 0; i < m_sounds.Count; i++)
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
        for (int i = 0; i < m_sounds.Count; i++)
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
        for (int i = 0; i < m_sounds.Count; i++)
        {
            if (m_sounds[i].GetName() == _name)
            {
                m_sounds[i].ChangePitch(Mathf.Clamp(_pitch, -3f, 3f));
                return;
            }
        }
        Debug.LogError(_name + " sound does not exist!");
    }

    public void CreatePlaylist(string _name)
    {
        MusicPlaylist _mp = new MusicPlaylist();
        _mp.SetPlaylistName(_name);
        m_playlists.Add(_mp);
    }

    public void CreateSound(string _name, AudioClip _audioClip, AudioMixerGroup _mixerGroup, float _volume, float _pitch, bool _loop, bool _mute)
    {
        Sound _so = new Sound(_name, _audioClip, _mixerGroup, _volume, _pitch, _loop, _mute);
        m_sounds.Add(_so);
    }

    public void CreateMusic(string _playlistName, string _name, AudioClip _audioClip, AudioMixerGroup _mixerGroup, float _volume, float _pitch, bool _loop, bool _mute)
    {
        Music _mo = new Music(_name, _audioClip, _mixerGroup, _volume, _pitch, _loop, _mute);
        if (m_playlists.Exists(x => x.GetPlaylistName() == _playlistName))
        {
            m_playlists.Find(x => x.GetPlaylistName() == _playlistName).AddMusic(_mo);
        } else
        {
            Debug.LogError("No playlist has that name! Check for spelling mistakes?");
        }

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