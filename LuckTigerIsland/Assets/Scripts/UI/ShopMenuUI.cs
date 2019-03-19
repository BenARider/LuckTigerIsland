using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ShopMenuUI : Selectable,ISelectHandler {
    BaseEventData m_baseEvent = null;
    EquipEntity m_equipEntity;
    public GameObject inventoryScreen;
    public GameObject PartyCanvas;
    public InventoryObject m_object;
    public Sprite Sprite;
    public TextMeshProUGUI ItemDescriptionText;
    public TextMeshProUGUI GoldAmountText;
    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI ItemBoughtText;
    public TextMeshProUGUI ItemNameText;
    public string PriceDescription;
    public string ItemDescription;
    private Image buttonHighlight;
    private Color m_buttonColorNonSelect;
    private Color m_buttonColorSelect;
    private Image m_itemImage;
    
    protected override void Start()
    {
        
       
        m_itemImage = GameObject.Find("Item_Image").GetComponent<Image>();
        GoldAmountText.text = "100";
        buttonHighlight = GetComponent<Image>();
        ItemBoughtText.text = "";
        m_buttonColorNonSelect = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        m_buttonColorSelect = new Color(0.2f, 0.2f, 0.2f, 0.2f);
        buttonHighlight.color = m_buttonColorNonSelect;
    }
    // Update is called once per frame
    void Update () {
        ItemNameText.text = m_object.name;
        if (IsHighlighted(m_baseEvent) == true)
        {
            buttonHighlight.color = m_buttonColorSelect;
            m_itemImage.sprite = m_object.Image;
            ItemDescriptionText.text = m_object.Description;
            PriceText.text = "Price: " + m_object.Price;
       
            if (Input.GetKeyDown(KeyCode.Return))//used to set the correct gold amount
            {
                PartyCanvas.SetActive(true);
                inventoryScreen.SetActive(true);
                
                m_equipEntity = GameObject.Find("InventoryScreen").GetComponent<EquipEntity>();
                m_equipEntity.AddItemToInventory(m_object);
              
                print("Added: " + m_object);
                ItemBoughtText.text = "You have bought a " + m_object.name;
                GoldAmountText.text = "50";
                StartCoroutine(Fader());
            
            }
        }
        else buttonHighlight.color = m_buttonColorNonSelect;
     
        inventoryScreen.SetActive(false);
        PartyCanvas.SetActive(false);
    }
   
    IEnumerator Fader()
    {
        yield return new  WaitForSeconds(0.8f);
        ItemBoughtText.text = "";
    }
}
