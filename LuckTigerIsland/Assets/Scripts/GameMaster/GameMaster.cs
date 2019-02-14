using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster instance;

    //To make sure there is only one AudioManager per scene.
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (instance != null)
        {
            Debug.LogError("Only one game master can be present.");
        }
        else
        {
            instance = this;
        }
    }
}
