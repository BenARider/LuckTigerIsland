using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicPlaylist
{
    [SerializeField]
    private string m_playlistName;
    [SerializeField]
    private List<Music> m_music;

    public string GetPlaylistName()
    {
        return m_playlistName;
    }
    public List<Music> GetMusic()
    {
        List<Music> _m = m_music;
        return _m;
    }

    public void SetPlaylistName(string _name)
    {
        m_playlistName = _name;
    }
}