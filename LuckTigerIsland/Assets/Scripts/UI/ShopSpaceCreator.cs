using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpaceCreator : MonoBehaviour {
    [SerializeField]
    Transform m_parentTransform;
    [SerializeField]
    ShopMenuUI m_shopUI;
    private Shop m_shop;
    private bool m_startIncrease;
    private void Awake()
    {
        m_parentTransform = GameObject.Find(this.gameObject.name).GetComponent<Transform>();
    }
    // Use this for initialization
    void Start () {
        m_shop = GameObject.Find("ShopNpc").GetComponent<Shop>();
      
        for (int i =0; i<m_shop.shop.Count;++i)
        {
            if(m_startIncrease == false)
            {
                m_shopUI.m_nameNumSet = 0;
            }
           
            m_shopUI = Instantiate(m_shopUI,new Vector2(m_parentTransform.transform.position.x, i * -50.0f), m_parentTransform.rotation);
            m_shopUI.transform.SetParent(m_parentTransform, false);
          //  m_parentTransform.position = new Vector2(m_parentTransform.transform.position.x, m_parentTransform.transform.position.y - 45);
            if(m_startIncrease == true)
            {
                m_shopUI.m_nameNumSet += 1;
            }
            m_startIncrease = true;
            m_shopUI.ItemNameText.text = m_shop.shop[m_shopUI.m_nameNumSet].sItem.name;

            print("SetNameNum");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
