using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopButtonOnClick : MonoBehaviour
{
    [SerializeField]
    private GameObject m_sellItem;
    [SerializeField]
    private GameObject m_buyItem;
    private GameObject m_shopEventSystem;
    private GameObject m_globalEventSystem;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void SwitchToBuy()
    {
        m_globalEventSystem = GameObject.Find("EventSystem");
        m_globalEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(m_buyItem);
        Debug.Log("Switching to buy");
    }
    public void SwitchToSell()
    {
        m_globalEventSystem = GameObject.Find("EventSystem");
        m_globalEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(m_sellItem);
        Debug.Log("Switching to sell");
    }
}
