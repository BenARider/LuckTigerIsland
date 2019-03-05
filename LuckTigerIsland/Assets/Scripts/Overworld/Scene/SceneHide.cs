using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHide : MonoBehaviour {

    private void Awake()
    {
        gameObject.SetActive(false);
    }

}
