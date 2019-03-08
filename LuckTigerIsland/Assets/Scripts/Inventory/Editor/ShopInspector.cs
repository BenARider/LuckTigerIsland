using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Shop))]
public class ShopInspector : Editor
{
    //Inventory
    SerializedProperty shopInventory;

    //Adding Items
    SerializedProperty item;
    SerializedProperty price;

    void OnEnable()
    {
        //Get the variable info from the path
        shopInventory = serializedObject.FindProperty("shopInventory");

        item = serializedObject.FindProperty("o_item");
        price = serializedObject.FindProperty("o_price");
    }

    public override void OnInspectorGUI()
    {
        //Store reference to script target.
        Shop myScript = (Shop)target;

        //Make labels to show rename inspector varibales.
        GUIContent itemLabel = new GUIContent("Item");
        GUIContent priceLabel = new GUIContent("Price");

        //Start of Inspector GUI
        serializedObject.Update();

        //Add an item
        GUILayout.Label("");
        GUILayout.Label("Add Item", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(item, itemLabel);
        EditorGUILayout.PropertyField(price, priceLabel);
        if (GUILayout.Button("Add Item"))
        {
            myScript.AddItem((InventoryObject)item.objectReferenceValue, price.intValue);
        }

        //Display Items
        GUILayout.Label("");
        GUILayout.Label("Shop Items", EditorStyles.boldLabel);
        if (shopInventory.arraySize != 0)
        {            
            //Display all items in the shop inventory.
            for (int i = 0; i < shopInventory.arraySize; i++)
            {
                SerializedProperty shopInventoryRef = shopInventory.GetArrayElementAtIndex(i);
                SerializedProperty shopItem = shopInventoryRef.FindPropertyRelative("Item");
                SerializedProperty shopPrice = shopInventoryRef.FindPropertyRelative("Price");

                EditorGUILayout.PropertyField(shopItem);
                EditorGUILayout.PropertyField(shopPrice);

                //Remove objecttive button
                if (GUILayout.Button("Remove Item", GUILayout.MaxWidth(150), GUILayout.MaxHeight(15)))
                {
                    shopInventory.DeleteArrayElementAtIndex(i);
                }
            }
            GUILayout.Label("");
        } else
        {
            GUILayout.Label("No items in the shop");
        }

        //End of Inspector GUI
        serializedObject.ApplyModifiedProperties();
    }
}
