using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : LTI.Singleton<GameMaster>{
    public float time = 0f;
    public float DayDuration = 10f;

    private void Start()
    {
        instance = this;// as GameMaster;
    }


}