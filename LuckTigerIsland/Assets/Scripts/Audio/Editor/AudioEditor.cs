using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(AudioManager))]
public class AudioEditor : Editor
{
    //Quest Information
    SerializedProperty soundList;
    SerializedProperty playlists;

    void OnEnable()
    {
        //Get the variable info from the path
        soundList = serializedObject.FindProperty("m_sounds");
        playlists = serializedObject.FindProperty("m_playlists");
    }

    public override void OnInspectorGUI()
    {
        AudioManager myScript = (AudioManager)target;

        //Start of Inspector GUI
        serializedObject.Update();

        //Sounds
        if (soundList.arraySize != 0)
        {
            GUILayout.Label("Sounds", EditorStyles.boldLabel);
            //Display all location objective objects
            for (int i = 0; i < soundList.arraySize; i++)
            {
                SerializedProperty soundListRef = soundList.GetArrayElementAtIndex(i);
                SerializedProperty soundName = soundListRef.FindPropertyRelative("m_name");
                SerializedProperty soundClip = soundListRef.FindPropertyRelative("m_audioClip");
                SerializedProperty soundMixer = soundListRef.FindPropertyRelative("m_audioMixer");
                SerializedProperty soundVolume = soundListRef.FindPropertyRelative("m_volume");
                SerializedProperty soundPitch = soundListRef.FindPropertyRelative("m_pitch");
                SerializedProperty soundLoop = soundListRef.FindPropertyRelative("m_loop");
                SerializedProperty soundMute = soundListRef.FindPropertyRelative("m_mute");

                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(soundName);
                //Remove objecttive button
                if (GUILayout.Button("Remove Sound", GUILayout.MaxWidth(110), GUILayout.MaxHeight(15)))
                {
                    soundList.DeleteArrayElementAtIndex(i);
                }
                GUILayout.EndHorizontal();

                EditorGUILayout.PropertyField(soundClip);
                EditorGUILayout.PropertyField(soundMixer);
                EditorGUILayout.PropertyField(soundVolume);
                EditorGUILayout.PropertyField(soundPitch);
                EditorGUILayout.PropertyField(soundLoop);
                EditorGUILayout.PropertyField(soundMute);

                
            }
            GUILayout.Label("");
        }

        //Music Playlists
        if (playlists.arraySize != 0)
        {
            EditorGUILayout.LabelField("Playlists", EditorStyles.boldLabel);
            //Display all location objective objects
            for (int i = 0; i < playlists.arraySize; i++)
            {
                SerializedProperty playlistsRef = playlists.GetArrayElementAtIndex(i);                
                SerializedProperty playlistName = playlistsRef.FindPropertyRelative("m_playlistName");
                SerializedProperty musicList = playlistsRef.FindPropertyRelative("m_music");

                EditorGUILayout.PropertyField(playlistName);

                //Music
                if (musicList.arraySize != 0)
                {                    
                    EditorGUILayout.LabelField("Music", EditorStyles.boldLabel);
                    EditorGUI.indentLevel++;
                    //Display all location objective objects
                    for (int j = 0; j < musicList.arraySize; j++)
                    {
                        SerializedProperty musicListRef = musicList.GetArrayElementAtIndex(j);
                        SerializedProperty musicName = musicListRef.FindPropertyRelative("m_name");

                        SerializedProperty musicClip = musicListRef.FindPropertyRelative("m_audioClip");
                        SerializedProperty musicMixer = musicListRef.FindPropertyRelative("m_audioMixer");
                        SerializedProperty musicVolume = musicListRef.FindPropertyRelative("m_volume");
                        SerializedProperty musicPitch = musicListRef.FindPropertyRelative("m_pitch");
                        SerializedProperty musicLoop = musicListRef.FindPropertyRelative("m_loop");
                        SerializedProperty musicMute = musicListRef.FindPropertyRelative("m_mute");

                        GUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(musicName);
                        //Remove objecttive button
                        if (GUILayout.Button("Remove Music", GUILayout.MaxWidth(110), GUILayout.MaxHeight(15)))
                        {
                            musicList.DeleteArrayElementAtIndex(j);
                        }
                        GUILayout.EndHorizontal();
                        
                        EditorGUILayout.PropertyField(musicClip);
                        EditorGUILayout.PropertyField(musicMixer);
                        EditorGUILayout.PropertyField(musicVolume);
                        EditorGUILayout.PropertyField(musicPitch);
                        EditorGUILayout.PropertyField(musicLoop);
                        EditorGUILayout.PropertyField(musicMute);

                        
                    }
                    EditorGUI.indentLevel--;
                }
                //Remove playlist button
                if (GUILayout.Button("Remove Playlist", GUILayout.MaxWidth(120), GUILayout.MaxHeight(15)))
                {
                    playlists.DeleteArrayElementAtIndex(i);
                }
                GUILayout.Label("");
            }
            GUILayout.Label("");
        }
        //End of Inspector GUI.
        serializedObject.ApplyModifiedProperties();
    }
}

