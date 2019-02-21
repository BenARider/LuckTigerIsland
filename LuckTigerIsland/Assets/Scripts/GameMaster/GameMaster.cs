using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour{

    public static GameMaster instance;

    void Awake()
    {
        if (instance != null)
        {            
            Debug.Log("There were two " + gameObject.name + "s present.");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

}
