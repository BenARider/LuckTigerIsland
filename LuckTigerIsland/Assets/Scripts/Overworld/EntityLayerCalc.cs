using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows sprites to have the correct layer automaticaly based on y position
public class EntityLayerCalc : MonoBehaviour {

    public int screenOffset;
    private SpriteRenderer[] m_sprite;
    //private Transform m_cameraTransform;
    public bool isPlayer = false;

	// Use this for initialization
	void OnEnable () {
        m_sprite = GetComponentsInChildren<SpriteRenderer>(); 
       // m_cameraTransform = ;
    }
	
	// Update is called once per frame
	void Update () {
        screenOffset = (int)((Camera.main.transform.position.y - transform.position.y)*10) + 100;
 
        screenOffset = Mathf.Clamp(screenOffset, 10, 190);

        if(isPlayer && PlayerManager.Instance.currentSceneName.Contains("Battle"))
        {
            screenOffset = -1000;
        }


        foreach (SpriteRenderer sr in m_sprite)
        {
            sr.sortingOrder = screenOffset;
        }
	}
}
