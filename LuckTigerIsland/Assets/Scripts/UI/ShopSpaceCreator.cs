using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpaceCreator : MonoBehaviour
{
    [SerializeField]
    Transform m_parentTransform;
    [SerializeField]
    ShopMenuUI m_shopUI;
    private Shop m_shop;
    private bool m_startIncrease;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnEnable()
    {
        m_shopUI = GameObject.Find("Item_Base").GetComponent<ShopMenuUI>();
        m_parentTransform = GameObject.Find(this.gameObject.name).GetComponent<Transform>(); m_shop = GameObject.Find("ShopNpc").GetComponent<Shop>();

        for (int i = 0; i < m_shop.shop.Count; ++i)
        {
            if (m_startIncrease == false)
            {
                m_shopUI.m_nameNumSet = 0;
            }
            m_shopUI = Instantiate(m_shopUI, new Vector2(m_parentTransform.transform.position.x, i * -50.0f), m_parentTransform.rotation);
            m_shopUI.transform.SetParent(m_parentTransform, false);
            if (m_startIncrease == true)
            {
                m_shopUI.m_nameNumSet += 1;
            }
            m_startIncrease = true;
            m_shopUI.ItemNameText.text = m_shop.shop[m_shopUI.m_nameNumSet].sItem.name;

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
      
    }
}
