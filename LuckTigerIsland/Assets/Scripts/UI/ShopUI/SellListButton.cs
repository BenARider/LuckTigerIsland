using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class SellListButton : MonoBehaviour, ISelectHandler
{

    [SerializeField]
    private TextMeshProUGUI m_text;
    [SerializeField]
    private SellListControl m_sellControl;
    Shop m_shop;
    Inventory m_inventory;
    InventoryObject m_inventoryObject;
    private string m_name;
    private Sprite m_image;
    private string m_description;
    private int m_price;
    private int m_amount;
    private int m_index;
    private bool isNull = false;

    public void SetText(string _text)
    {
        m_text.text = _text;
        m_name = _text;
    }
    public void SetIndex(int _index)
    {
        m_index += _index;
    }
    public int GetIndex()
    {
        return m_index;
    }
    public void SetImage(Sprite _image)
    {
        m_image = _image;
    }
    public void SetDescription(string _desc)
    {
        m_description = _desc;
    }
    public void SetPrice(int _price)
    {
        m_price = _price;
    }
    public void SetAmount(int _amount)
    {
        m_amount = _amount;
    }
    public void SetObject(InventoryObject _object)
    {
        m_inventoryObject = _object;
    }
    public void SetIsNull(bool _b)
    {
        isNull = _b;
    }
    public void SellItem()
    {
        m_shop.SellItem(m_inventoryObject);
        m_sellControl.UpdateInventoryUI();
    }

    public void OnSelect(BaseEventData _data)
    {
        if (!isNull)
        {
            m_sellControl.ButtonClicked(m_image, m_name, m_description, m_price);
        }
        else
        {
            Debug.LogError("Inventory Slot has nothing in it!");
        }
    }
}
