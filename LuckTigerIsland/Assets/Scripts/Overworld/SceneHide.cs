using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHide : MonoBehaviour {
    public bool flag = true;
    private void Awake()
    {
        if (flag)
        {
            gameObject.SetActive(false);
        }
    }

}
