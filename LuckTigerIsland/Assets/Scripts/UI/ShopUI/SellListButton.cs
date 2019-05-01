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
    [SerializeField]
    Shop m_shop;
    Inventory m_inventory;
    InventoryObject m_inventoryObject;
    [SerializeField]
    TextMeshProUGUI m_soldText;
    [SerializeField]
    GameObject m_closeButton;
    EventSystem m_eventSystem;

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
        Debug.Log("Object Set: " + _object);
        m_inventoryObject = _object;
    }
    public void SetIsNull(bool _b)
    {
        isNull = _b;
    }
    public void SellItem()
    {
        m_eventSystem = EventSystem.current;
        if (Inventory.Instance.inventory.Find(x => x.iObject == m_inventoryObject).amount > 0)
        {
            if (m_inventoryObject.sellable)
            {
                m_shop.SellItem(m_inventoryObject);
                m_soldText.text = "Just sold: " + m_inventoryObject.objectName;
                StartCoroutine(FadeText());
                m_sellControl.UpdateInventoryUI();
                m_eventSystem.SetSelectedGameObject(m_closeButton);
            } else
            {
                Debug.Log("This item cannot be sold.");
            }
        }
    }

    public void OnSelect(BaseEventData _data)
    {
        if (!isNull)
        {
            m_sellControl.ButtonClicked(m_image, m_name, m_description, m_price,m_amount);
        }
        else
        {
            Debug.LogError("Inventory Slot has nothing in it!");
        }
    }
    IEnumerator FadeText()
    {
        yield return new WaitForSeconds(2);
        m_soldText.text = "";
    }
}
