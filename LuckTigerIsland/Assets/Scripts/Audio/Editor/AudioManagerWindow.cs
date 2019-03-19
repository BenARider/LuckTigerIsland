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
    private float sizeRatio2 = 0.44f;
    private bool isResizing1;
    private bool isResizing2;

    private AudioManager m_audioManager;
    SerializedObject so;

    //Playlist
    [SerializeField]
    private string m_playlistName = "Playlist";

    //Sound
    [SerializeField]
    string m_soundName = "Name";
    [SerializeField]
    AudioClip m_soundAudioClip;
    [SerializeField]
    AudioMixerGroup m_soundAudioMixer;
    [SerializeField]
    float m_soundVolume = 1;
    [SerializeField]
    float m_soundPitch = 1;
    [SerializeField]
    bool m_soundLoop = false;
    [SerializeField]
    bool m_soundMute = false;

    //Music
    [SerializeField]
    string m_playlist = "Playlist Name";
    [SerializeField]
    string m_musicName = "Name";
    [SerializeField]
    AudioClip m_musicAudioClip;
    [SerializeField]
    AudioMixerGroup m_musicAudioMixer;
    [SerializeField]
    float m_musicVolume = 1;
    [SerializeField]
    float m_musicPitch = 1;
    [SerializeField]
    bool m_musicLoop = false;
    [SerializeField]
    bool m_musicMute = false;

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
        ScriptableObject target = this;
        so = new SerializedObject(target);

        m_audioManager = (AudioManager)FindObjectOfType(typeof(AudioManager));

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
        SerializedProperty soundName = so.FindProperty("m_soundName");
        SerializedProperty soundAudioClip = so.FindProperty("m_soundAudioClip");
        SerializedProperty soundAudioMixer = so.FindProperty("m_soundAudioMixer");
        SerializedProperty soundVolume = so.FindProperty("m_soundVolume");
        SerializedProperty soundPitch = so.FindProperty("m_soundPitch");
        SerializedProperty soundLoop = so.FindProperty("m_soundLoop");
        SerializedProperty soundMute = so.FindProperty("m_soundMute");

        EditorGUILayout.PropertyField(soundName);
        EditorGUILayout.PropertyField(soundAudioClip);
        EditorGUILayout.PropertyField(soundAudioMixer);
        EditorGUILayout.PropertyField(soundVolume);
        EditorGUILayout.PropertyField(soundPitch);
        EditorGUILayout.PropertyField(soundLoop);
        EditorGUILayout.PropertyField(soundMute);

        //m_audioClip = EditorGUILayout.PropertyField(m_audioClip);
        if (GUILayout.Button("Create Sound", GUILayout.MaxWidth(120), GUILayout.MaxHeight(15)))
        {
            m_audioManager.CreateSound(m_soundName, m_soundAudioClip, m_soundAudioMixer, m_soundVolume, m_soundPitch, m_soundLoop, m_soundMute);
        }
        GUILayout.EndArea();
        so.ApplyModifiedProperties();
    }

    private void DrawPlaylistPanel()
    {
        playlistPanel = new Rect(0, position.height * sizeRatio1, position.width, position.height * sizeRatio1);
        GUILayout.BeginArea(playlistPanel);

        SerializedProperty playlistName = so.FindProperty("m_playlistName");
        EditorGUILayout.PropertyField(playlistName);

        if (GUILayout.Button("Create Playlist", GUILayout.MaxWidth(120), GUILayout.MaxHeight(15)))
        {
            m_audioManager.CreatePlaylist(m_playlistName);
        }
        GUILayout.EndArea();
    }

    private void DrawMusicPanel()
    {
        musicPanel = new Rect(0, position.height * sizeRatio2, position.width, position.height * sizeRatio2);
        GUILayout.BeginArea(musicPanel);
        GUILayout.Label("Create Music");

        //VARIABLES
        SerializedProperty musicPlaylist = so.FindProperty("m_playlist");
        SerializedProperty musicName = so.FindProperty("m_musicName");
        SerializedProperty musicAudioClip = so.FindProperty("m_musicAudioClip");
        SerializedProperty musicAudioMixer = so.FindProperty("m_musicAudioMixer");
        SerializedProperty musicVolume = so.FindProperty("m_musicVolume");
        SerializedProperty musicPitch = so.FindProperty("m_musicPitch");
        SerializedProperty musicLoop = so.FindProperty("m_musicLoop");
        SerializedProperty musicMute = so.FindProperty("m_musicMute");

        EditorGUILayout.PropertyField(musicPlaylist);
        EditorGUILayout.PropertyField(musicName);
        EditorGUILayout.PropertyField(musicAudioClip);
        EditorGUILayout.PropertyField(musicAudioMixer);
        EditorGUILayout.PropertyField(musicVolume);
        EditorGUILayout.PropertyField(musicPitch);
        EditorGUILayout.PropertyField(musicLoop);
        EditorGUILayout.PropertyField(musicMute);

        if (GUILayout.Button("Create Music Track", GUILayout.MaxWidth(120), GUILayout.MaxHeight(15)))
        {
            m_audioManager.CreateMusic(m_playlist, m_musicName, m_musicAudioClip, m_musicAudioMixer, m_musicVolume, m_musicPitch, m_musicLoop, m_musicMute);
        }

        GUILayout.EndArea();
        so.ApplyModifiedProperties();
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
        if (isResizing1 && !isResizing2)
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