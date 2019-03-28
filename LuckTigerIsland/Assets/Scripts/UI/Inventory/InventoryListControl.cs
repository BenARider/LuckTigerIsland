using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryListControl : MonoBehaviour {

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
    int index = -1;

 
    private void OnEnable()
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        try
        {
            if (Inventory.Instance.inventory.Count > 0)
            {
                ResetChildren();
                SetDefaults();
                for (int i = 0; i < Inventory.Instance.inventory.Count; i++)
                {
                    GameObject button = Instantiate(m_buttonTemplate) as GameObject;
                    button.SetActive(true);

                    if (Inventory.Instance.inventory[i].iObject != null)
                    {
                        button.GetComponent<InventoryListButton>().SetText(Inventory.Instance.inventory[i].iObject.name);
                        button.GetComponent<InventoryListButton>().SetDescription(Inventory.Instance.inventory[i].iObject.Description);
                        button.GetComponent<InventoryListButton>().SetImage(Inventory.Instance.inventory[i].iObject.Image);
                        button.GetComponent<InventoryListButton>().SetPrice(Inventory.Instance.inventory[i].iObject.Price);
                        button.GetComponent<InventoryListButton>().SetAmount(Inventory.Instance.inventory[i].amount);
                        button.GetComponent<InventoryListButton>().SetIndex(index);
                    }
                    else
                    {
                        button.GetComponent<InventoryListButton>().SetText("NullInvSlot");
                        button.GetComponent<InventoryListButton>().SetIsNull(true);
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
        m_image.sprite = Inventory.Instance.inventory[0].iObject.Image;
        m_name.text = Inventory.Instance.inventory[0].iObject.name; 
        m_description.text = Inventory.Instance.inventory[0].iObject.Description;
        m_price.text = "Price: " + Inventory.Instance.inventory[0].iObject.Price;
    }

    private void ResetChildren()
    {
        bool first = false;
        foreach(Transform child in m_buttonTemplate.transform.parent.transform)
        {
            if (!first)
            {
                first = true;
            } else
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public void ButtonClicked(Sprite _sprite, string _name, string _description, int _price)
    {
        m_image.sprite = _sprite;
        m_name.text = _name;
        m_description.text = _description;
        m_price.text = "Price: " + _price.ToString();
    }

}
