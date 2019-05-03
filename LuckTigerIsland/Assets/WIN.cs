using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WIN : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Camera.main.GetComponent<ScreenTransition>().toBlack();
        IEnumerator enumerator = doDestroy();
        StartCoroutine(enumerator);
    }

    



IEnumerator doDestroy()
{
    yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("END");
    }

}
