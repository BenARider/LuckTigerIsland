using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : LTI.Singleton<GameMaster>{

    private void Start()
    {
        instance = this;// as GameMaster;
    }
}