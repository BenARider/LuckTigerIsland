using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShopSpaceCreator : MonoBehaviour
{
    [SerializeField]
    EventSystem m_eventSystem;
    [SerializeField]
    Transform m_parentTransform;
    [SerializeField]
    Transform m_parentTransform2;
    [SerializeField]
    ShopMenuUI m_shopUI;
    private Shop m_shop;
    private bool m_startIncrease;
    private float m_yPosColumn2;
    [SerializeField]
    GameObject m_closeObject;
    // Use this for initialization
    void Start()
    {
        m_eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnEnable()
    {
        m_yPosColumn2 = 0;
        m_eventSystem.SetSelectedGameObject(m_closeObject);
        m_shopUI = GameObject.Find("Item_Base").GetComponent<ShopMenuUI>();
        m_parentTransform = GameObject.Find("SpawnerTransform").GetComponent<Transform>();
        m_parentTransform2 = GameObject.Find("SpawnerTransform2").GetComponent<Transform>();
        m_shop = GameObject.Find("ShopNpc").GetComponent<Shop>();
        for (int i = 0; i < m_shop.shop.Count; ++i)
        {
            if (m_startIncrease == false)
            {
                m_shopUI.SetNameNumSet(0);
            }
            if (i < 20)
            {
                m_shopUI = Instantiate(m_shopUI, new Vector2(m_parentTransform.transform.position.x, i * -50.0f), m_parentTransform.rotation);
                m_shopUI.transform.SetParent(m_parentTransform, false);
                m_shopUI.m_itemButton.interactable = true;
            }
            if (i > 20)
            {
                m_yPosColumn2 -= 50;
                m_shopUI = Instantiate(m_shopUI, new Vector2(m_parentTransform2.transform.position.x, m_parentTransform2.transform.position.y), m_parentTransform2.rotation);
                m_shopUI.transform.position = new Vector2(m_parentTransform2.transform.position.x, m_yPosColumn2);
                m_shopUI.transform.SetParent(m_parentTransform2, false);
                m_shopUI.m_itemButton.interactable = true;
            }
            if (m_startIncrease == true)
            {
                m_shopUI.SetNameNumSet(1);
            }
            m_startIncrease = true;
            m_shopUI.ItemNameText.text = m_shop.shop[m_shopUI.GetNameNumSet()].sItem.name;

            print("SetNameNum");
        }
    }
    private void OnDisable()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
            m_startIncrease = false;
        }
        foreach (Transform child in m_parentTransform2.transform)
        {
            Destroy(child.gameObject);
            m_startIncrease = false;

        }
        m_shopUI.m_itemButton.interactable = false;

    }
}
