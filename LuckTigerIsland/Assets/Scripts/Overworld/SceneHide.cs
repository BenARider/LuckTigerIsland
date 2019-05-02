using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHide : MonoBehaviour {
    public bool hidescene= true;
    private void Awake()
    {
        if (hidescene)
        {
            gameObject.SetActive(false);
        }
    }

}
