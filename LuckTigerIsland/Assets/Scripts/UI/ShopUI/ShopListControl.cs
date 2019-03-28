using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopListControl : MonoBehaviour
{

    [SerializeField]
    private GameObject m_buttonTemplate;

    [SerializeField]
    private Image m_image;
    [SerializeField]
    private TextMeshProUGUI m_name;
    [SerializeField]
    private TextMeshProUGUI m_description;
    [SerializeField]
    private TextMeshProUGUI m_price;
    Shop m_shop;
    int index =-1;
    private void Start()
    {
        m_shop = GameObject.Find("ShopNpc").GetComponent<Shop>();
        UpdateShopUI();
    }
    public void OnEnable()
    { 
      
    }
    public void UpdateShopUI()
    {
        try
        {
            if (m_shop.shop.Count > 0)
            {
                SetDefaults();
                for (int i = 0; i < m_shop.shop.Count; i++)
                {
                    GameObject button = Instantiate(m_buttonTemplate) as GameObject;
                  
                    button.SetActive(true);
                    index++;
                    if (m_shop.shop[i].sItem != null)
                    {
                        button.GetComponent<ShopListButton>().SetText(m_shop.shop[i].sItem.name);
                        button.GetComponent<ShopListButton>().SetDescription(m_shop.shop[i].sItem.Description);
                        button.GetComponent<ShopListButton>().SetImage(m_shop.shop[i].sItem.Image);
                        button.GetComponent<ShopListButton>().SetPrice(m_shop.shop[i].sItem.Price);
                        button.GetComponent<ShopListButton>().SetIndex(index);
                    }
                    else
                    {
                        button.GetComponent<ShopListButton>().SetText("NullInvSlot");
                        button.GetComponent<ShopListButton>().SetIsNull(true);
                    }
                    
                    button.transform.SetParent(m_buttonTemplate.transform.parent, false);
                }
                
            }
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("An Inventory does not exist in the scene! Is there a player?");
        }
    }
 

    private void SetDefaults()
    {
        m_image.sprite = m_shop.shop[0].sItem.Image;
        m_name.text = m_shop.shop[0].sItem.name;
        m_description.text = m_shop.shop[0].sItem.Description;
        m_price.text = "Price: " + m_shop.shop[0].sItem.Price;
    }

    public void ButtonClicked(Sprite _sprite, string _name, string _description, int _price)
    {
        m_image.sprite = _sprite;
        m_name.text = _name;
        m_description.text = _description;
        m_price.text = "Price: " + _price.ToString();
    }

}
