﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ShopMenuUI : Selectable, ISelectHandler
{
    BaseEventData m_baseEvent = null;
    EquipEntity m_equipEntity;
    public GameObject inventoryScreen;
    public GameObject PartyCanvas;
    public InventoryObject m_object;
    private Sprite Sprite;
    private TextMeshProUGUI ItemDescriptionText;
    private TextMeshProUGUI GoldAmountText;
    private TextMeshProUGUI PriceText;
    private TextMeshProUGUI ItemBoughtText;
    public TextMeshProUGUI ItemNameText;
    private Image buttonHighlight;
    private Color m_buttonColorNonSelect;
    private Color m_buttonColorSelect;
    private Image m_itemImage;
    private Shop m_shop;
    private int m_nameNum;
    [SerializeField]
    public int m_nameNumSet;
    private bool m_canOpenInventory;
    protected override void Start()
    {
        m_shop = GameObject.Find("ShopNpc").GetComponent<Shop>();
        ItemDescriptionText = GameObject.Find("Item_Description").GetComponent<TextMeshProUGUI>();
        ItemBoughtText = GameObject.Find("Item_Bought_Text").GetComponent<TextMeshProUGUI>();
        PriceText = GameObject.Find("Item_Cost").GetComponent<TextMeshProUGUI>();
        GoldAmountText = GameObject.Find("Gold_Amount_Text").GetComponent<TextMeshProUGUI>();
        m_itemImage = GameObject.Find("Item_Image").GetComponent<Image>();
        Sprite = GameObject.Find(this.gameObject.name).GetComponent<Sprite>();

        GoldAmountText.text = "100";
        buttonHighlight = GetComponent<Image>();
        m_itemImage.sprite = m_shop.shop[0].sItem.Image;
        ItemDescriptionText.text = m_shop.shop[0].sItem.Description;
        PriceText.text = "Price: " + m_shop.shop[0].sPrice;
        m_itemImage.sprite = m_shop.shop[0].sItem.Image;
        m_buttonColorNonSelect = new Color(0, 0, 0, 0);
        m_buttonColorSelect = new Color(0.8f, 0.8f, 0.8f, 0.8f);
        buttonHighlight.color = m_buttonColorNonSelect;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_canOpenInventory == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))//used to set the correct gold amount
            {
                PartyCanvas.SetActive(true);
                inventoryScreen.SetActive(true);

                m_equipEntity = GameObject.Find("InventoryScreen").GetComponent<EquipEntity>();
                m_equipEntity.AddItemToInventory(m_object);
                print("nameNum: " + m_nameNum);
                print("Added: " + m_object);
                ItemBoughtText.text = "You have bought a " + m_object.name;
                GoldAmountText.text = "50";
                StartCoroutine(Fader());
            }
            PartyCanvas.SetActive(false);
            inventoryScreen.SetActive(false);
        }
    }
    public void SetNumName(int _numName)
    {
        m_nameNum += _numName;
    }
    public override void OnSelect(BaseEventData eventData)
    {
        buttonHighlight.color = m_buttonColorSelect;
        m_nameNum = m_nameNumSet;
        m_itemImage.sprite = m_shop.shop[m_nameNum].sItem.Image;
        ItemDescriptionText.text = m_shop.shop[m_nameNum].sItem.Description;
        PriceText.text = "Price: " + m_shop.shop[m_nameNum].sPrice;
        m_canOpenInventory = true;
    }
    public override void OnDeselect(BaseEventData eventData)
    {
        buttonHighlight.color = m_buttonColorNonSelect;
        m_canOpenInventory = false;
    }
    IEnumerator Fader()
    {
        yield return new WaitForSeconds(0.8f);
        ItemBoughtText.text = "";
    }
}
