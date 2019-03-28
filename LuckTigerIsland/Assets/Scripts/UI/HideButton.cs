using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class HideButton : MonoBehaviour, IDeselectHandler
{
    public GameObject gameobject;
    [SerializeField]
    private bool m_hideSelf;
    public int m_hideCount;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnDeselect(BaseEventData eventData)
    {
        if(m_hideSelf == true)
        {
            this.gameObject.SetActive(false);
        }
        m_hideCount += 1;
        if (m_hideCount == 2)
        {
            gameobject.SetActive(false);
            m_hideCount = 0;
        }

    }

}
