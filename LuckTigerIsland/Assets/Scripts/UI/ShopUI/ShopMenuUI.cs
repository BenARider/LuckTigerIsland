using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ShopMenuUI : MonoBehaviour,ISelectHandler, IDeselectHandler
{
    BaseEventData m_baseEvent = null;
    Inventory m_inventoryEntity;
    public EquipEntity equipEntity;
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
    private Color m_buttonColourNonSelect;
    private Color m_buttonColourSelect;
    private Image m_itemImage;
    private Shop m_shop;
    private int m_nameNum;
    private bool m_stopMultiBuy;
    [SerializeField]
    private int m_nameNumSet;
    public Button m_itemButton;
    void Start()
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
        m_buttonColourNonSelect = new Color(0, 0, 0, 0);
        m_buttonColourSelect = new Color(0.8f, 0.8f, 0.8f, 0.8f);
        buttonHighlight.color = m_buttonColourNonSelect;
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void Buy()
    {
        PartyCanvas.SetActive(true);
        inventoryScreen.SetActive(true);
        m_stopMultiBuy = true;
       // m_equipEntity = GameObject.Find("InventoryScreen").GetComponent<EquipEntity>();
       // this.m_equipEntity.AddItemToInventory(m_object);
        print("nameNum: " + m_nameNum);
        print("Added: " + m_object);
        ItemBoughtText.text = "You have bought a " + m_object.name;
        GoldAmountText.text = "50";
        StartCoroutine(Fader());
        PartyCanvas.SetActive(false);
        inventoryScreen.SetActive(false);
    }
    public void SetNumName(int _numName)
    {
        m_nameNum += _numName;
    }
    public void OnSelect(BaseEventData eventData)
    { 
        buttonHighlight.color = m_buttonColourSelect;
        m_nameNum = m_nameNumSet;
        m_itemImage.sprite = m_shop.shop[m_nameNum].sItem.Image;
        ItemDescriptionText.text = m_shop.shop[m_nameNum].sItem.Description;
        PriceText.text = "Price: " + m_shop.shop[m_nameNum].sPrice;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        buttonHighlight.color = m_buttonColourNonSelect;
    }
    public int GetNameNumSet()
    {
        return m_nameNumSet;
    }
    public void SetNameNumSet(int _nameNum)
    {
      m_nameNumSet += _nameNum;
    }
    IEnumerator Fader()
    {
        yield return new WaitForSeconds(0.8f);
        ItemBoughtText.text = "";
    }
  
}
