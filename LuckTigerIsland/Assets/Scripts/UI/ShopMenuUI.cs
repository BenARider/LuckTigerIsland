using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ShopMenuUI : Selectable,ISelectHandler {
    BaseEventData m_baseEvent = null;
  
    public Sprite Sprite;
    public TextMeshProUGUI ItemDescriptionText;
    public TextMeshProUGUI GoldAmountText;
    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI ItemBoughtText;
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
		if(IsHighlighted(m_baseEvent) == true)
        {
            buttonHighlight.color = m_buttonColorSelect;
            m_itemImage.sprite = Sprite;
            ItemDescriptionText.text = ItemDescription;
            PriceText.text = "Price: " + PriceDescription;
            if(Input.GetKeyDown(KeyCode.Return))//used to set the correct gold amount
            {
                if(this.gameObject.name == "Health Potion")
                {
                    GoldAmountText.text = "50";
                    ItemBoughtText.text = "You have bought a " + this.gameObject.name;
                    StartCoroutine(Fader());
                }
                if(this.gameObject.name == "Bomb")
                {
                    GoldAmountText.text = "12";
                    ItemBoughtText.text = "You have bought a " + this.gameObject.name;
                    StartCoroutine(Fader());
                }
                if (this.gameObject.name == "Helmet")
                {
                    GoldAmountText.text = "70";
                    ItemBoughtText.text = "You have bought a " + this.gameObject.name;
                    StartCoroutine(Fader());
                }
                if (this.gameObject.name == "Hat")
                {
                    GoldAmountText.text = "12";
                    ItemBoughtText.text = "You have bought a " + this.gameObject.name;
                    StartCoroutine(Fader());
                }
                if (this.gameObject.name == "Axe")
                {
                    GoldAmountText.text = "27";
                    ItemBoughtText.text = "You have bought a " + this.gameObject.name;
                    StartCoroutine(Fader());
                }
                if (this.gameObject.name == "Breastplate")
                {
                    GoldAmountText.text = "8";
                    ItemBoughtText.text = "You have bought a " + this.gameObject.name;
                    StartCoroutine(Fader());
                }
                if (this.gameObject.name == "Shield")
                {
                    GoldAmountText.text = "40";
                    ItemBoughtText.text = "You have bought a " + this.gameObject.name;
                    StartCoroutine(Fader());
                }
            }
        }
        else buttonHighlight.color = m_buttonColorNonSelect;
    }
    IEnumerator Fader()
    {
        yield return new  WaitForSeconds(0.8f);
        ItemBoughtText.text = "";
    }
}
