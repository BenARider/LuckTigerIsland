using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterSubLevel : GenericInteract {
    public string sceneName;
     

    public override void OnInteract(PlayerManager player)
    {
        Debug.Log("Entering "+ sceneName);
        
        //TODO: SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
