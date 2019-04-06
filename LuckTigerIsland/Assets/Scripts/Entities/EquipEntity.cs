using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class EquipEntity : MonoBehaviour
{

    public PlayerEntity[] m_partyMembers;

    [SerializeField]
    Weapon m_currentWeapon;
    [SerializeField]
    Armour m_currentArmour;
    InventoryListButton m_inventoryListButton;
    protected bool equipped = false;
    private Color m_itemFadeColour;
    [SerializeField]
    private GameObject[] m_equipImages;
    [SerializeField]
    private GameObject[] m_baseSlotImages;


    Inventory m_inventory;

    private TextMeshProUGUI m_partyMemberName;
    private int m_partyImageIndex;
    private int[] tempDefence;
    private int[] tempAttack;
    private bool m_unEquip;
    [SerializeField]
    TextMeshProUGUI m_justEquippedText;

    // Use this for initialization

    void Start()
    {

        m_partyMembers = new PlayerEntity[4];
        m_partyMembers[0] = GameObject.Find("Luck").GetComponent<PlayerEntity>();
        m_partyMembers[1] = GameObject.Find("Duck").GetComponent<PlayerEntity>();
        m_partyMembers[2] = GameObject.Find("Buck").GetComponent<PlayerEntity>();
        m_partyMembers[3] = GameObject.Find("Phil").GetComponent<PlayerEntity>();

        m_partyMemberName = GameObject.Find("PartyMemberName").GetComponent<TextMeshProUGUI>();
        m_partyMemberName.text = "Luck";
        m_itemFadeColour.a = 0.0f;
        tempDefence = new int[4];
        tempAttack = new int[4];
        tempDefence[0] = m_partyMembers[0].GetDefence();
        tempAttack[0] = m_partyMembers[0].GetStrength();
        tempDefence[1] = m_partyMembers[1].GetDefence();
        tempAttack[1] = m_partyMembers[1].GetStrength();
        tempDefence[2] = m_partyMembers[2].GetDefence();
        tempAttack[2] = m_partyMembers[2].GetStrength();
        tempDefence[3] = m_partyMembers[3].GetDefence();
        tempAttack[3] = m_partyMembers[3].GetStrength();
    }
    // Update is called once per frame
    void Update()
    {

        if (m_partyImageIndex == 0)
        {
            m_partyMemberName.text = "Luck";
            m_equipImages[0].SetActive(true);
            m_equipImages[1].SetActive(true);
            m_equipImages[2].SetActive(false);
            m_equipImages[3].SetActive(false);
            m_equipImages[4].SetActive(false);
            m_equipImages[5].SetActive(false);
            m_equipImages[6].SetActive(false);
            m_equipImages[7].SetActive(false);

        }
        if (m_partyImageIndex == 1)
        {
            m_partyMemberName.text = "Duck";
            m_equipImages[0].SetActive(false);
            m_equipImages[1].SetActive(false);
            m_equipImages[2].SetActive(true);
            m_equipImages[3].SetActive(true);
            m_equipImages[4].SetActive(false);
            m_equipImages[5].SetActive(false);
            m_equipImages[6].SetActive(false);
            m_equipImages[7].SetActive(false);

        }
        if (m_partyImageIndex == 2)
        {
            m_partyMemberName.text = "Buck";
            m_equipImages[0].SetActive(false);
            m_equipImages[1].SetActive(false);
            m_equipImages[2].SetActive(false);
            m_equipImages[3].SetActive(false);
            m_equipImages[4].SetActive(true);
            m_equipImages[5].SetActive(true);
            m_equipImages[6].SetActive(false);
            m_equipImages[7].SetActive(false);

        }
        if (m_partyImageIndex == 3)
        {
            m_partyMemberName.text = "Phil";
            m_equipImages[0].SetActive(false);
            m_equipImages[1].SetActive(false);
            m_equipImages[2].SetActive(false);
            m_equipImages[3].SetActive(false);
            m_equipImages[4].SetActive(false);
            m_equipImages[5].SetActive(false);
            m_equipImages[6].SetActive(true);
            m_equipImages[7].SetActive(true);
        }
        if (m_partyImageIndex == 4)
        {
            m_partyImageIndex = 0;
        }


    }
    public void UnEquip(string _slotName)
    {
        if (_slotName == "Armour")
        {       
            m_currentArmour = null;
          
            if (m_partyImageIndex == 0)
            {
                m_partyMembers[0].SetDefence(tempDefence[0]);
                m_equipImages[0].GetComponent<Image>().sprite = m_baseSlotImages[0].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 1)
            {
                m_partyMembers[1].SetDefence(tempDefence[1]);
                m_equipImages[2].GetComponent<Image>().sprite = m_baseSlotImages[0].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 2)
            {
                m_partyMembers[2].SetDefence(tempDefence[2]);
                m_equipImages[4].GetComponent<Image>().sprite = m_baseSlotImages[0].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 3)
            {
                m_partyMembers[3].SetDefence(tempDefence[3]);
                m_equipImages[6].GetComponent<Image>().sprite = m_baseSlotImages[0].GetComponent<Image>().sprite;
            }

        }
        if (_slotName == "Weapon")
        {
            m_currentWeapon = null;
          
            if (m_partyImageIndex == 0)
            {
                m_partyMembers[0].SetStrength(tempAttack[0]);
                m_equipImages[1].GetComponent<Image>().sprite = m_baseSlotImages[1].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 1)
            {
                m_partyMembers[1].SetStrength(tempAttack[1]);
                m_equipImages[3].GetComponent<Image>().sprite = m_baseSlotImages[1].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 2)
            {
                m_partyMembers[2].SetStrength(tempAttack[2]);
                m_equipImages[5].GetComponent<Image>().sprite = m_baseSlotImages[1].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 3)
            {
                m_partyMembers[3].SetStrength(tempAttack[3]);
                m_equipImages[7].GetComponent<Image>().sprite = m_baseSlotImages[1].GetComponent<Image>().sprite;
            }

        }
    }
    public void EquipWeapon(Weapon _object)
    {
            m_currentWeapon = _object;
            if (m_equipImages[1].GetComponent<Image>().sprite.name == "equipment_preview_10")
            {
                if (m_partyImageIndex == 0)
                {
                    m_partyMembers[0].SetStrength(_object.attack);
                    m_equipImages[1].GetComponent<Image>().sprite = _object.Image;

                }
            }
            if (m_equipImages[3].GetComponent<Image>().sprite.name == "equipment_preview_10")
            {
                if (m_partyImageIndex == 1)
                {
                    m_partyMembers[1].SetStrength(_object.attack);
                    m_equipImages[3].GetComponent<Image>().sprite = _object.Image;

                }
            }
            if (m_equipImages[5].GetComponent<Image>().sprite.name == "equipment_preview_10")
            {
                if (m_partyImageIndex == 2)
                {
                    m_partyMembers[2].SetStrength(_object.attack);
                    m_equipImages[5].GetComponent<Image>().sprite = _object.Image;

                }
            }
            if (m_equipImages[7].GetComponent<Image>().sprite.name == "equipment_preview_10")
            {

                if (m_partyImageIndex == 3)
                {
                    m_partyMembers[3].SetStrength(_object.attack);
                    m_equipImages[7].GetComponent<Image>().sprite = _object.Image;
                }
            }
            m_justEquippedText.text = "Just equipped: " + _object.objectName;
            StartCoroutine(HideText());
    }
    public void EquipArmour(Armour _object)
    {

        m_currentArmour = _object;
        if (m_equipImages[0].GetComponent<Image>().sprite.name == "equipment_preview_1")
        {

            if (m_partyImageIndex == 0)
            {
                m_equipImages[0].GetComponent<Image>().sprite = _object.Image;
                m_partyMembers[0].SetDefence(_object.defence);
            }
        }
        if (m_equipImages[2].GetComponent<Image>().sprite.name == "equipment_preview_1")
        {

            if (m_partyImageIndex == 1)
            {
                m_equipImages[2].GetComponent<Image>().sprite = _object.Image;
                m_partyMembers[1].SetDefence(_object.defence);
            }
        }
        if (m_equipImages[4].GetComponent<Image>().sprite.name == "equipment_preview_1")
        {

            if (m_partyImageIndex == 2)
            {
                m_equipImages[4].GetComponent<Image>().sprite = _object.Image;
                m_partyMembers[2].SetDefence(_object.defence);
            }
        }
        if (m_equipImages[6].GetComponent<Image>().sprite.name == "equipment_preview_1")
        {
            if (m_partyImageIndex == 3)
            {
                m_equipImages[6].GetComponent<Image>().sprite = _object.Image;
                m_partyMembers[3].SetDefence(_object.defence);
            }
        }
        m_justEquippedText.text = "Just equipped a : " + _object.objectName;
        StartCoroutine(HideText());
    }
    IEnumerator HideText()
    {
        yield return new WaitForSeconds(2.0f);
        m_justEquippedText.text = "";
    }
    public void SetPlayerImageId(int _index)
    {
        m_partyImageIndex += _index;
    }
}
