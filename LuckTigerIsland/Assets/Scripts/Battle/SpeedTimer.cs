using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTimer : MonoBehaviour {

    public static float m_speedCounter = 0.0f;
    public static bool isPaused;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void FixedUpdate()
    {
        if (isPaused == false)
        {
            m_speedCounter += 0.25f;

        }
        else
        {
            Debug.Log("Timer Paused");
        }
	}
}
