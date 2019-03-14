using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;

public class AudioManagerWindow : EditorWindow
{
    private Rect soundPanel;
    private Rect playlistPanel;
    private Rect musicPanel;
    private Rect resizer;
    private Rect resizer2;

    private float sizeRatio1 = 0.33f;
    private float sizeRatio2 = 0.66f;
    private bool isResizing1;
    private bool isResizing2;

    //Playlist
    private string playlistName = "Playlist";

    //Sound
    protected string m_name = "Name";
    protected AudioClip m_audioClip;
    protected AudioMixerGroup m_audioMixer;
    protected float m_volume = 1;
    protected float m_pitch = 1;
    protected bool m_loop = false;
    protected bool m_mute = false;

    private GUIStyle resizerStyle;

    [MenuItem("Window/AudioManager")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        AudioManagerWindow window = (AudioManagerWindow)EditorWindow.GetWindow(typeof(AudioManagerWindow));
        window.Show();
    }

    private void OnEnable()
    {
        resizerStyle = new GUIStyle();
        resizerStyle.normal.background = EditorGUIUtility.Load("icons/d_AvatarBlendBackground.png") as Texture2D;
    }

    void OnGUI()
    {
        DrawSoundPanel();
        DrawPlaylistPanel();
        DrawMusicPanel();
        DrawResizer();

        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();
    }

    private void DrawSoundPanel()
    {
        soundPanel = new Rect(0, 0, position.width, position.height * 0.33f);
        GUILayout.BeginArea(soundPanel);
        GUILayout.Label("Create Sound");
        //VARIABLES
        m_audioClip = EditorGUILayout.PropertyField(m_audioClip);
        if (GUILayout.Button("Create Sound", GUILayout.MaxWidth(120), GUILayout.MaxHeight(15)))
        {
            AudioManager.Instance.CreateSound();
        }
        GUILayout.EndArea();
    }

    private void DrawPlaylistPanel()
    {
        playlistPanel = new Rect(0, position.height * sizeRatio1, position.width, position.height * sizeRatio1);
        GUILayout.BeginArea(playlistPanel);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Playlist Name");
        playlistName = GUILayout.TextField(playlistName);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Create Playlist", GUILayout.MaxWidth(120), GUILayout.MaxHeight(15)))
        {
            AudioManager.Instance.CreatePlaylist(playlistName);
        }
        GUILayout.EndArea();
    }

    private void DrawMusicPanel()
    {
        musicPanel = new Rect(0, position.height * sizeRatio2, position.width, position.height * sizeRatio2/2);
        GUILayout.BeginArea(musicPanel);
        GUILayout.Label("Create Music");
        GUILayout.EndArea();
    }

    private void DrawResizer()
    {
        resizer = new Rect(0, (position.height * sizeRatio1) - 5f, position.width, 10f);
        resizer2 = new Rect(0, (position.height * sizeRatio2) - 5f, position.width, 10f);

        GUILayout.BeginArea(new Rect(resizer.position + (Vector2.up * 5f), new Vector2(position.width, 2)), resizerStyle);
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(resizer2.position + (Vector2.up * 5f), new Vector2(position.width, 2)), resizerStyle);
        GUILayout.EndArea();

        EditorGUIUtility.AddCursorRect(resizer, MouseCursor.ResizeVertical);
        EditorGUIUtility.AddCursorRect(resizer2, MouseCursor.ResizeVertical);
    }

    private void ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0 && resizer.Contains(e.mousePosition))
                {
                    isResizing1 = true;
                }
                if (e.button == 0 && resizer2.Contains(e.mousePosition))
                {
                    isResizing2 = true;
                }
                break;

            case EventType.MouseUp:
                isResizing1 = false;
                isResizing2 = false;
                break;
        }

        Resize(e);
    }

    private void Resize(Event e)
    {
        if (isResizing1)
        {
            sizeRatio1 = e.mousePosition.y / position.height;
            Repaint();
        }
        if (isResizing2)
        {
            sizeRatio2 = e.mousePosition.y / position.height;
            Repaint();
        }
    }
}