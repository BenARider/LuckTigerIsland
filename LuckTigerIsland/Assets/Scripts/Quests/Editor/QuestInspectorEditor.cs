using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Quest))]
public class QuestInspectorEditor : Editor
{
    //Quest Information
    SerializedProperty title;
    SerializedProperty description;
    SerializedProperty exp;
    SerializedProperty gold;
    SerializedProperty itemReward;
    SerializedProperty killObjectives;
    SerializedProperty locationObjectives;
    SerializedProperty inventoryObjectives;
    

    //Objective Types
    SerializedProperty objectiveType;
    SerializedProperty location;
    SerializedProperty enemy;
    SerializedProperty enemyAmount;
    SerializedProperty item;
    SerializedProperty itemAmount;

    void OnEnable()
    {
        //Get the variable info from the path
        title = serializedObject.FindProperty("m_title");
        description = serializedObject.FindProperty("m_description");
        exp = serializedObject.FindProperty("m_expReward");
        gold = serializedObject.FindProperty("m_goldReward");
        itemReward = serializedObject.FindProperty("m_itemReward");
        killObjectives = serializedObject.FindProperty("m_killObjectives");
        locationObjectives = serializedObject.FindProperty("m_locationObjectives");
        inventoryObjectives = serializedObject.FindProperty("m_inventoryObjectives");

        objectiveType = serializedObject.FindProperty("o_type");
        location = serializedObject.FindProperty("o_location");
        enemy = serializedObject.FindProperty("o_enemy");
        enemyAmount = serializedObject.FindProperty("o_enemyAmount");
        item = serializedObject.FindProperty("o_item");
        itemAmount = serializedObject.FindProperty("o_itemAmount");

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
        GUIContent itemLabel = new GUIContent("Item Type");
        GUIContent itemAmountLabel = new GUIContent("Item Amount");


        //Start of Inspector GUI
        serializedObject.Update();

        //Quest Variables
        GUILayout.Label("Quest Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(title);
        EditorGUILayout.PropertyField(description);
        EditorGUILayout.PropertyField(exp);
        EditorGUILayout.PropertyField(gold);
        EditorGUILayout.PropertyField(itemReward);    

        //Create Objectives
        GUILayout.Label("");
        GUILayout.Label("Add Objectives", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(objectiveType, objectiveTypeLabel);

        //Change what the inspector displays based on the value of the enum variable.
        EObjectiveType ot = (EObjectiveType)objectiveType.enumValueIndex;
        switch (ot)
        {
            //Location Objective
            case EObjectiveType.LocationObjective:
                EditorGUILayout.PropertyField(location, locationLabel);
                if(GUILayout.Button("Add Location Objective"))
                {
                    myScript.AddLocationObjective((ELocations)location.enumValueIndex);
                }
                break;

            //Kill Objective
            case EObjectiveType.KillObjective:
                EditorGUILayout.PropertyField(enemy, enemyLabel);
                EditorGUILayout.PropertyField(enemyAmount, enemyAmountLabel);
                if (GUILayout.Button("Add Kill Objective"))
                {
                    myScript.AddKillObjective((EEnemies)enemy.enumValueIndex, enemyAmount.intValue);
                }
                break;

            //Inventory Objective
            case EObjectiveType.InventoryObjective:
                EditorGUILayout.PropertyField(item, itemLabel);
                EditorGUILayout.PropertyField(itemAmount, itemAmountLabel);
                if (GUILayout.Button("Add Inventory Objective"))
                {
                    myScript.AddnventoryObjective((InventoryObject)item.objectReferenceValue, itemAmount.intValue);
                }
                break;
        }

        //Display the current objectives.
        GUILayout.Label("");
        GUILayout.Label("Objectives", EditorStyles.boldLabel);

        //Inventory Objectives
        if (inventoryObjectives.arraySize != 0)
        {
            GUILayout.Label("Inventory Objectives", EditorStyles.boldLabel);
            //Display all location objective objects
            for (int i = 0; i < inventoryObjectives.arraySize; i++)
            {
                SerializedProperty inventoryObjectivesRef = inventoryObjectives.GetArrayElementAtIndex(i);
                SerializedProperty inventory = inventoryObjectivesRef.FindPropertyRelative("m_object");
                SerializedProperty totalAmount = inventoryObjectivesRef.FindPropertyRelative("m_totalAmount");

                EditorGUILayout.PropertyField(inventory);
                EditorGUILayout.PropertyField(totalAmount);

                //Remove objecttive button
                if (GUILayout.Button("Remove Objective", GUILayout.MaxWidth(150), GUILayout.MaxHeight(15)))
                {
                    inventoryObjectives.DeleteArrayElementAtIndex(i);
                }
            }
            GUILayout.Label("");
        }

        //Kill Objectives.
        if (killObjectives.arraySize != 0)
        {
            GUILayout.Label("Kill Objectives", EditorStyles.boldLabel);
            //Display all kill objective objects
            for (int i = 0; i < killObjectives.arraySize; i++)
            {
                SerializedProperty killObjectivesRef = killObjectives.GetArrayElementAtIndex(i);
                SerializedProperty enemy = killObjectivesRef.FindPropertyRelative("m_enemy");
                SerializedProperty amount = killObjectivesRef.FindPropertyRelative("m_amount");

                EditorGUILayout.PropertyField(enemy);
                EditorGUILayout.PropertyField(amount);

                //Remove objecttive button
                if (GUILayout.Button("Remove Objective", GUILayout.MaxWidth(150), GUILayout.MaxHeight(15)))
                {
                    killObjectives.DeleteArrayElementAtIndex(i);
                }
            }
            GUILayout.Label("");

        }

        //Location Objectives
        if (locationObjectives.arraySize != 0)
        {
            GUILayout.Label("Location Objectives", EditorStyles.boldLabel);
            //Display all location objective objects
            for (int i = 0; i < locationObjectives.arraySize; i++)
            {
                SerializedProperty locationObjectivesRef = locationObjectives.GetArrayElementAtIndex(i);
                SerializedProperty location = locationObjectivesRef.FindPropertyRelative("m_location");

                EditorGUILayout.PropertyField(location);

                //Remove objecttive button
                if (GUILayout.Button("Remove Objective", GUILayout.MaxWidth(150), GUILayout.MaxHeight(15)))
                {
                    locationObjectives.DeleteArrayElementAtIndex(i);
                }
            }
            GUILayout.Label("");
        }
  

        //End of Inspector GUI
        serializedObject.ApplyModifiedProperties();         
    }
}

