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
    PlayerManager m_playerManager;
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

        m_partyMemberName = GameObject.Find("PartyMemberName").GetComponent<TextMeshProUGUI>();
        m_partyMemberName.text = "Luck";
        m_itemFadeColour.a = 0.0f;
        tempDefence = new int[4];
        tempAttack = new int[4];
        tempDefence[0] = m_playerManager.cleric.GetDefence();
        tempAttack[0] = m_playerManager.cleric.GetStrength();
        tempDefence[1] = m_playerManager.warrior.GetDefence();
        tempAttack[1] = m_playerManager.warrior.GetStrength();
        tempDefence[2] = m_playerManager.wizard.GetDefence();
        tempAttack[2] = m_playerManager.wizard.GetStrength();
        tempDefence[3] = m_playerManager.ninja.GetDefence();
        tempAttack[3] = m_playerManager.ninja.GetStrength();
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
                m_playerManager.cleric.SetDefence(tempDefence[0]);
                m_equipImages[0].GetComponent<Image>().sprite = m_baseSlotImages[0].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 1)
            {
                m_playerManager.warrior.SetDefence(tempDefence[1]);
                m_equipImages[2].GetComponent<Image>().sprite = m_baseSlotImages[0].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 2)
            {
                m_playerManager.wizard.SetDefence(tempDefence[2]);
                m_equipImages[4].GetComponent<Image>().sprite = m_baseSlotImages[0].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 3)
            {
                m_playerManager.ninja.SetDefence(tempDefence[3]);
                m_equipImages[6].GetComponent<Image>().sprite = m_baseSlotImages[0].GetComponent<Image>().sprite;
            }

        }
        if (_slotName == "Weapon")
        {
            m_currentWeapon = null;

            if (m_partyImageIndex == 0)
            {
                m_playerManager.cleric.SetStrength(tempAttack[0]);
                m_equipImages[1].GetComponent<Image>().sprite = m_baseSlotImages[1].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 1)
            {
                m_playerManager.warrior.SetStrength(tempAttack[1]);
                m_equipImages[3].GetComponent<Image>().sprite = m_baseSlotImages[1].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 2)
            {
                m_playerManager.wizard.SetStrength(tempAttack[2]);
                m_equipImages[5].GetComponent<Image>().sprite = m_baseSlotImages[1].GetComponent<Image>().sprite;
            }
            if (m_partyImageIndex == 3)
            {
                m_playerManager.ninja.SetStrength(tempAttack[3]);
                m_equipImages[7].GetComponent<Image>().sprite = m_baseSlotImages[1].GetComponent<Image>().sprite;
            }

        }
    }
    public void EquipWeapon(Weapon _object)
    {
        m_currentWeapon = _object;
        if (m_partyImageIndex == 0)
        {
            if (m_equipImages[1].GetComponent<Image>().sprite.name == "equipment_preview_10")
            {
                m_playerManager.cleric.SetStrength(_object.attack);
                m_equipImages[1].GetComponent<Image>().sprite = _object.Image;
                m_justEquippedText.text = "Just equipped: " + _object.objectName;
                StartCoroutine(HideText());
            }
        }
        if (m_partyImageIndex == 1)
        {
            if (m_equipImages[3].GetComponent<Image>().sprite.name == "equipment_preview_10")
            {
                m_playerManager.warrior.SetStrength(_object.attack);
                m_equipImages[3].GetComponent<Image>().sprite = _object.Image;
                m_justEquippedText.text = "Just equipped: " + _object.objectName;
                StartCoroutine(HideText());
            }
        }
        if (m_partyImageIndex == 2)
        {
            if (m_equipImages[5].GetComponent<Image>().sprite.name == "equipment_preview_10")
            {
                m_playerManager.wizard.SetStrength(_object.attack);
                m_equipImages[5].GetComponent<Image>().sprite = _object.Image;
                m_justEquippedText.text = "Just equipped: " + _object.objectName;
                StartCoroutine(HideText());
            }
        }
        if (m_partyImageIndex == 3)
        {
            if (m_equipImages[7].GetComponent<Image>().sprite.name == "equipment_preview_10")
            {      
                m_playerManager.ninja.SetStrength(_object.attack);
                m_equipImages[7].GetComponent<Image>().sprite = _object.Image;
                m_justEquippedText.text = "Just equipped: " + _object.objectName;
                StartCoroutine(HideText());
            }
        }

    }
    public void EquipArmour(Armour _object)
    {

        m_currentArmour = _object;
        if (m_partyImageIndex == 0)
        {
            if (m_equipImages[0].GetComponent<Image>().sprite.name == "equipment_preview_1")
            {
                m_justEquippedText.text = "Just equipped a : " + _object.objectName;
                StartCoroutine(HideText());
                m_equipImages[0].GetComponent<Image>().sprite = _object.Image;
                m_playerManager.cleric.SetDefence(_object.defence);

            }
        }
        if (m_partyImageIndex == 1)
        {
            if (m_equipImages[2].GetComponent<Image>().sprite.name == "equipment_preview_1")
            {

                m_equipImages[2].GetComponent<Image>().sprite = _object.Image;
                m_playerManager.warrior.SetDefence(_object.defence);
                m_justEquippedText.text = "Just equipped a : " + _object.objectName;
                StartCoroutine(HideText());
            }
        }
        if (m_partyImageIndex == 2)
        {
            if (m_equipImages[4].GetComponent<Image>().sprite.name == "equipment_preview_1")
            {
                m_equipImages[4].GetComponent<Image>().sprite = _object.Image;
                m_playerManager.wizard.SetDefence(_object.defence);
                m_justEquippedText.text = "Just equipped a : " + _object.objectName;
                StartCoroutine(HideText());
            }
        }
        if (m_partyImageIndex == 3)
        {
            if (m_equipImages[6].GetComponent<Image>().sprite.name == "equipment_preview_1")
            {
                m_equipImages[6].GetComponent<Image>().sprite = _object.Image;
                m_playerManager.ninja.SetDefence(_object.defence);
                m_justEquippedText.text = "Just equipped a : " + _object.objectName;
                StartCoroutine(HideText());
            }
        }

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
