using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryListButton : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI m_text;
    [SerializeField]
    private InventoryListControl m_invControl;

    private string m_name;
    private Sprite m_image;
    private string m_description;
    private int m_price;
    private int m_amount;

    private bool isNull = false;

	public void SetText(string _text)
    {
        m_text.text = _text;
        m_name = _text;
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
    public void SetIsNull(bool _b)
    {
        isNull = _b;
    }


    public void OnClick()
    {
        if (!isNull)
        {
            m_invControl.ButtonClicked(m_image, m_name, m_description, m_price);
        }
        else
        {
            Debug.LogError("Inventory Slot has nothing in it!");
        }
    }
}
