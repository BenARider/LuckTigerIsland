using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Quest))]
public class LevelScriptEditor : Editor
{
    //Quest Information
    SerializedProperty title;
    SerializedProperty description;
    SerializedProperty exp;
    SerializedProperty gold;
    SerializedProperty objectives;

    //Objective Types
    SerializedProperty objectiveType;
    SerializedProperty location;
    SerializedProperty enemy;
    SerializedProperty enemyAmount;

    void OnEnable()
    {
        //Get the variable info from the path
        title = serializedObject.FindProperty("m_title");
        description = serializedObject.FindProperty("m_description");
        exp = serializedObject.FindProperty("m_expReward");
        gold = serializedObject.FindProperty("m_goldReward");
        objectives = serializedObject.FindProperty("m_objectives");

        objectiveType = serializedObject.FindProperty("o_type");
        location = serializedObject.FindProperty("o_location");
        enemy = serializedObject.FindProperty("o_enemy");
        enemyAmount = serializedObject.FindProperty("o_enemyAmount");
    }

    public override void OnInspectorGUI()
    {
        //Store a reference to the quest script.
        Quest myScript = (Quest)target;

        //Make labels to show rename inspector varibales.
        GUIContent objectiveTypeLabel = new GUIContent("Objective Type");
        GUIContent locationLabel = new GUIContent("Location");
        GUIContent enemyLabel = new GUIContent("Enemy Type");
        GUIContent enemyAmountLabel = new GUIContent("Enemy Amount");

        //Start of Inspector GUI
        serializedObject.Update();

        //Quest Variables
        GUILayout.Label("Quest Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(title);
        EditorGUILayout.PropertyField(description);
        EditorGUILayout.PropertyField(exp);
        EditorGUILayout.PropertyField(gold);

        //Create Objectives
        GUILayout.Label("");
        GUILayout.Label("Add Objectives", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(objectiveType, objectiveTypeLabel);

        //Change what the inspector displays based on the value of the enum variable.
        EObjectiveType ot = (EObjectiveType)objectiveType.enumValueIndex;
        switch (ot)
        {
            case EObjectiveType.LocationObjective:
                EditorGUILayout.PropertyField(location, locationLabel);
                break;

            case EObjectiveType.KillObjective:
                EditorGUILayout.PropertyField(enemy, enemyLabel);
                EditorGUILayout.PropertyField(enemyAmount, enemyAmountLabel);
                break;

        }

        //Add Objective based on input info.
        if (GUILayout.Button("Add Objective"))
        {
            switch (ot)
            {
                case EObjectiveType.LocationObjective:
                    myScript.AddLocationObjective((ELocations)location.enumValueIndex);
                    break;

                case EObjectiveType.KillObjective:
                    myScript.AddKillObjective((EEnemies)enemy.enumValueIndex, enemyAmount.intValue);
                    break;
                    
            }            
        }
        EditorGUILayout.PropertyField(objectives, true);

        //Button to remove last objective. TODO: add an index option?
        if (GUILayout.Button("Remove Last Objective"))
        {
            myScript.RemoveLastFromList();
        }


        //End of Inspector GUI
        serializedObject.ApplyModifiedProperties();         
    }
}

