using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyItem : MonoBehaviour {
   public ShopItem m_item;
   public Shop m_shop;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void BuyItem()
    {
        m_shop.BuyItem(m_item);
    }
}
