using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(QuestManager))]
public class QuestManagerInspector : Editor
{
    //Objective Types
    SerializedProperty quests;
    SerializedProperty title;
    SerializedProperty desc;
    SerializedProperty exp;
    SerializedProperty gold;
    int index;

    void OnEnable()
    {
        //Get the variable info from the path
        quests = serializedObject.FindProperty("m_quests");
        title = serializedObject.FindProperty("m_title");
        desc = serializedObject.FindProperty("m_description");
        exp = serializedObject.FindProperty("m_exp");
        gold = serializedObject.FindProperty("m_gold");
    }

    public override void OnInspectorGUI()
    {
        //Store a reference to the quest script.
        QuestManager myScript = (QuestManager)target;


        //Start of Inspector GUI
        serializedObject.Update();

        GUILayout.Label("");
        GUILayout.Label("Create a Quest", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(title);
        EditorGUILayout.PropertyField(desc);
        EditorGUILayout.PropertyField(exp);
        EditorGUILayout.PropertyField(gold);

        

        if (GUILayout.Button("Create Quest"))
        {
            myScript.CreateQuest();          
        }
        EditorStyles.label.wordWrap = true;
        GUILayout.Label("To Add Quest Objectives, click on the");
        GUILayout.Label("individual quest game objects and");
        GUILayout.Label("add them through the inspector.");
        GUILayout.Label("");
        EditorGUILayout.PropertyField(quests, true);

        GUILayout.BeginHorizontal();
        index = EditorGUILayout.IntField("Deletion Index", index);

        if (GUILayout.Button("Delete Quest", GUILayout.MaxWidth(150), GUILayout.MaxHeight(15)))
        {
            myScript.DeleteQuest(index);
        }
        GUILayout.EndHorizontal();
        //End of Inspector GUI
        serializedObject.ApplyModifiedProperties();
    }
}
