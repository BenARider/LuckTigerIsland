using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PartyStatsUI : MonoBehaviour {
    public TextMeshProUGUI statText;
    [SerializeField]
    PlayerManager m_playerManager;
    [SerializeField]
    PlayerEntity[] m_playerEntity;
    private int m_playerIDStats = 0;
    private int m_playerIDSkills = 0;
    [SerializeField]
    private bool m_findTextGameObjects;
    public Slider expBar;
    public Button playerStatButton;
    public Button playerSkillButton;
    public Image skillImage;
    public GameObject playerStatMenu;
    [SerializeField]
    TextMeshProUGUI m_partyLevelTotal;
    [SerializeField]
    TextMeshProUGUI m_skillTitle;
    [SerializeField]
    TextMeshProUGUI m_skillDescription;
    [SerializeField]
    TextMeshProUGUI m_skillDamage;
    [SerializeField]
    TextMeshProUGUI m_skillCost;
    // Use this for initialization
    
    void Start () {
        m_playerEntity = new PlayerEntity[4];
        m_playerManager = PlayerManager.Instance;
        m_playerEntity[0] = GameObject.Find("Player1").GetComponent<PlayerEntity>();
        m_playerEntity[1] = GameObject.Find("Player2").GetComponent<PlayerEntity>();
        m_playerEntity[2] = GameObject.Find("Player3").GetComponent<PlayerEntity>();
        m_playerEntity[3] = GameObject.Find("Player4").GetComponent<PlayerEntity>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public int GetPlayerStatID()
    {
        return m_playerIDStats;
    }
    private void OnEnable()
    {
        IncreasePlayerStatID();
        m_playerEntity[0] = GameObject.Find("Player1").GetComponent<PlayerEntity>();
        m_playerEntity[1] = GameObject.Find("Player2").GetComponent<PlayerEntity>();
        m_playerEntity[2] = GameObject.Find("Player3").GetComponent<PlayerEntity>();
        m_playerEntity[3] = GameObject.Find("Player4").GetComponent<PlayerEntity>();
        expBar.value = m_playerManager.GetXP();
        m_partyLevelTotal.text = "" + m_playerManager.GetLevel();
        statText.text = " Name: Luck" + "\n " + "Class: Cleric" + "\n " + "Health: " + m_playerManager.cleric.GetMaxHealth() + "\n Mana: " + m_playerManager.cleric.GetMaxMana() + "\n Physical Damage: " + m_playerManager.cleric.GetStrength() + "\n Magical Damage: " + m_playerManager.cleric.GetMagicPower() +
        "\n Physical Defence: " + m_playerManager.cleric.GetDefence() + "\n Magical Defence: " + m_playerManager.cleric.GetMagicDefence() + "\n Speed: " + m_playerManager.cleric.GetSpeed();
    }
    //Used to control the party stat menu by setting and finding all the text values/objects
    public void IncreaseSkillID()
    {
        m_playerIDSkills += 1;
        if (m_playerIDSkills == 7)
        {
            m_playerIDSkills = 0;
        }
        if (m_playerIDStats == 0)
        {
            if (m_playerIDSkills == 0)
            {
                m_skillTitle.text = m_playerEntity[0].attacks[0].attackName;
                skillImage.sprite = m_playerEntity[0].attacks[0].attackImage;
                m_skillDescription.text = m_playerEntity[0].attacks[0].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[0].attacks[0].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[0].attacks[0].attackCost;
            }
            if (m_playerIDSkills == 1)
            {
                m_skillTitle.text = m_playerEntity[0].attacks[1].attackName;
                skillImage.sprite = m_playerEntity[0].attacks[1].attackImage;
                m_skillDescription.text = m_playerEntity[0].attacks[1].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[0].attacks[1].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[0].attacks[1].attackCost;
            }
            if (m_playerIDSkills == 2)
            {
                m_skillTitle.text = m_playerEntity[0].attacks[2].attackName;
                skillImage.sprite = m_playerEntity[0].attacks[2].attackImage;
                m_skillDescription.text = m_playerEntity[0].attacks[2].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[0].attacks[2].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[0].attacks[2].attackCost;
            }
            if (m_playerIDSkills == 3)
            {
                m_skillTitle.text = m_playerEntity[0].attacks[3].attackName;
                skillImage.sprite = m_playerEntity[0].attacks[3].attackImage;
                m_skillDescription.text = m_playerEntity[0].attacks[3].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[0].attacks[3].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[0].attacks[3].attackCost;
            }
            if (m_playerIDSkills == 4)
            {
                m_skillTitle.text = m_playerEntity[0].attacks[4].attackName;
                skillImage.sprite = m_playerEntity[0].attacks[4].attackImage;
                m_skillDescription.text = m_playerEntity[0].attacks[4].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[0].attacks[4].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[0].attacks[4].attackCost;
            }
            if (m_playerIDSkills == 5)
            {
                m_skillTitle.text = m_playerEntity[0].attacks[5].attackName;
                skillImage.sprite = m_playerEntity[0].attacks[5].attackImage;
                m_skillDescription.text = m_playerEntity[0].attacks[5].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[0].attacks[5].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[0].attacks[5].attackCost;
            }
            if (m_playerIDSkills == 6)
            {
                m_skillTitle.text = m_playerEntity[0].attacks[6].attackName;
                skillImage.sprite = m_playerEntity[0].attacks[6].attackImage;
                m_skillDescription.text = m_playerEntity[0].attacks[6].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[0].attacks[6].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[0].attacks[6].attackCost;
            }
            if (m_playerIDSkills == 7)
            {
                m_skillTitle.text = m_playerEntity[0].attacks[7].attackName;
                skillImage.sprite = m_playerEntity[0].attacks[7].attackImage;
                m_skillDescription.text = m_playerEntity[0].attacks[7].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[0].attacks[7].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[0].attacks[7].attackCost;
            }
        }
        if (m_playerIDStats == 1)
        {
            if (m_playerIDSkills == 0)
            {
                m_skillTitle.text = m_playerEntity[1].attacks[0].attackName;
                skillImage.sprite = m_playerEntity[1].attacks[0].attackImage;
                m_skillDescription.text = m_playerEntity[1].attacks[0].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[1].attacks[0].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[1].attacks[0].attackCost;
            }
            if (m_playerIDSkills == 1)
            {
                m_skillTitle.text = m_playerEntity[1].attacks[1].attackName;
                skillImage.sprite = m_playerEntity[1].attacks[1].attackImage;
                m_skillDescription.text = m_playerEntity[1].attacks[1].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[1].attacks[1].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[1].attacks[1].attackCost;
            }
            if (m_playerIDSkills == 2)
            {
                m_skillTitle.text = m_playerEntity[1].attacks[2].attackName;
                skillImage.sprite = m_playerEntity[1].attacks[2].attackImage;
                m_skillDescription.text = m_playerEntity[1].attacks[2].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[1].attacks[2].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[1].attacks[2].attackCost;
            }
            if (m_playerIDSkills == 3)
            {
                m_skillTitle.text = m_playerEntity[1].attacks[3].attackName;
                skillImage.sprite = m_playerEntity[1].attacks[3].attackImage;
                m_skillDescription.text = m_playerEntity[1].attacks[3].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[1].attacks[3].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[1].attacks[3].attackCost;
            }
            if (m_playerIDSkills == 4)
            {
                m_skillTitle.text = m_playerEntity[1].attacks[4].attackName;
                skillImage.sprite = m_playerEntity[1].attacks[4].attackImage;
                m_skillDescription.text = m_playerEntity[1].attacks[4].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[1].attacks[4].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[1].attacks[4].attackCost;
            }
            if (m_playerIDSkills == 5)
            {
                m_skillTitle.text = m_playerEntity[1].attacks[5].attackName;
                skillImage.sprite = m_playerEntity[1].attacks[5].attackImage;
                m_skillDescription.text = m_playerEntity[1].attacks[5].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[1].attacks[5].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[1].attacks[5].attackCost;
            }
            if (m_playerIDSkills == 6)
            {
                m_skillTitle.text = m_playerEntity[1].attacks[6].attackName;
                skillImage.sprite = m_playerEntity[1].attacks[6].attackImage;
                m_skillDescription.text = m_playerEntity[1].attacks[6].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[1].attacks[6].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[1].attacks[6].attackCost;
            }
            if (m_playerIDSkills == 7)
            {
                m_skillTitle.text = m_playerEntity[1].attacks[7].attackName;
                skillImage.sprite = m_playerEntity[1].attacks[7].attackImage;
                m_skillDescription.text = m_playerEntity[1].attacks[7].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[1].attacks[7].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[1].attacks[7].attackCost;
            }
        }
        if (m_playerIDStats==2)
        {
            if (m_playerIDSkills == 0)
            {
                m_skillTitle.text = m_playerEntity[2].attacks[0].attackName;
                skillImage.sprite = m_playerEntity[2].attacks[0].attackImage;
                m_skillDescription.text = m_playerEntity[2].attacks[0].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[2].attacks[0].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[2].attacks[0].attackCost;
            }
            if (m_playerIDSkills == 1)
            {
                m_skillTitle.text = m_playerEntity[2].attacks[1].attackName;
                skillImage.sprite = m_playerEntity[2].attacks[1].attackImage;
                m_skillDescription.text = m_playerEntity[2].attacks[1].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[2].attacks[1].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[2].attacks[1].attackCost;
            }
            if (m_playerIDSkills == 2)
            {
                m_skillTitle.text = m_playerEntity[2].attacks[2].attackName;
                skillImage.sprite = m_playerEntity[2].attacks[2].attackImage;
                m_skillDescription.text = m_playerEntity[2].attacks[2].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[2].attacks[2].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[2].attacks[2].attackCost;
            }
            if (m_playerIDSkills == 3)
            {
                m_skillTitle.text = m_playerEntity[2].attacks[3].attackName;
                skillImage.sprite = m_playerEntity[2].attacks[3].attackImage;
                m_skillDescription.text = m_playerEntity[2].attacks[3].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[2].attacks[3].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[2].attacks[3].attackCost;
            }
            if (m_playerIDSkills == 4)
            {
                m_skillTitle.text = m_playerEntity[2].attacks[4].attackName;
                skillImage.sprite = m_playerEntity[2].attacks[4].attackImage;
                m_skillDescription.text = m_playerEntity[2].attacks[4].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[2].attacks[4].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[2].attacks[4].attackCost;
            }
            if (m_playerIDSkills == 5)
            {
                m_skillTitle.text = m_playerEntity[2].attacks[5].attackName;
                skillImage.sprite = m_playerEntity[2].attacks[5].attackImage;
                m_skillDescription.text = m_playerEntity[2].attacks[5].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[2].attacks[5].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[2].attacks[5].attackCost;
            }
            if (m_playerIDSkills == 6)
            {
                m_skillTitle.text = m_playerEntity[2].attacks[6].attackName;
                skillImage.sprite = m_playerEntity[2].attacks[6].attackImage;
                m_skillDescription.text = m_playerEntity[2].attacks[6].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[2].attacks[6].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[2].attacks[6].attackCost;
            }
            if (m_playerIDSkills == 7)
            {
                m_skillTitle.text = m_playerEntity[2].attacks[7].attackName;
                skillImage.sprite = m_playerEntity[2].attacks[7].attackImage;
                m_skillDescription.text = m_playerEntity[2].attacks[7].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[2].attacks[7].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[2].attacks[7].attackCost;
            }
        }
        if (m_playerIDStats==3)
        {
            if (m_playerIDSkills == 0)
            {
                m_skillTitle.text = m_playerEntity[3].attacks[0].attackName;
                skillImage.sprite = m_playerEntity[3].attacks[0].attackImage;
                m_skillDescription.text = m_playerEntity[3].attacks[0].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[3].attacks[0].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[3].attacks[0].attackCost;
            }
            if (m_playerIDSkills == 1)
            {
                m_skillTitle.text = m_playerEntity[3].attacks[1].attackName;
                skillImage.sprite = m_playerEntity[3].attacks[1].attackImage;
                m_skillDescription.text = m_playerEntity[3].attacks[1].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[3].attacks[1].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[3].attacks[1].attackCost;
            }
            if (m_playerIDSkills == 2)
            {
                m_skillTitle.text = m_playerEntity[3].attacks[2].attackName;
                skillImage.sprite = m_playerEntity[3].attacks[2].attackImage;
                m_skillDescription.text = m_playerEntity[3].attacks[2].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[3].attacks[2].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[3].attacks[2].attackCost;
            }
            if (m_playerIDSkills == 3)
            {
                m_skillTitle.text = m_playerEntity[3].attacks[3].attackName;
                skillImage.sprite = m_playerEntity[3].attacks[3].attackImage;
                m_skillDescription.text = m_playerEntity[3].attacks[3].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[3].attacks[3].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[3].attacks[3].attackCost;
            }
            if (m_playerIDSkills == 4)
            {
                m_skillTitle.text = m_playerEntity[3].attacks[4].attackName;
                skillImage.sprite = m_playerEntity[3].attacks[4].attackImage;
                m_skillDescription.text = m_playerEntity[3].attacks[4].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[3].attacks[4].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[3].attacks[4].attackCost;
            }
            if (m_playerIDSkills == 5)
            {
                m_skillTitle.text = m_playerEntity[3].attacks[5].attackName;
                skillImage.sprite = m_playerEntity[3].attacks[5].attackImage;
                m_skillDescription.text = m_playerEntity[3].attacks[5].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[3].attacks[5].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[3].attacks[5].attackCost;
            }
            if (m_playerIDSkills == 6)
            {
                m_skillTitle.text = m_playerEntity[3].attacks[6].attackName;
                skillImage.sprite = m_playerEntity[3].attacks[6].attackImage;
                m_skillDescription.text = m_playerEntity[3].attacks[6].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[3].attacks[6].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[3].attacks[6].attackCost;
            }
            if (m_playerIDSkills == 7)
            {
                m_skillTitle.text = m_playerEntity[3].attacks[7].attackName;
                skillImage.sprite = m_playerEntity[3].attacks[7].attackImage;
                m_skillDescription.text = m_playerEntity[3].attacks[7].attackDescription;
                m_skillDamage.text = "Damage: " + m_playerEntity[3].attacks[7].attackDamage;
                m_skillCost.text = "Mana Cost: " + m_playerEntity[3].attacks[7].attackCost;
            }
        }

    }
    public void IncreasePlayerStatID()
    {
       
        m_playerIDStats += 1;
        if (m_playerIDStats == 4)
        {
            m_playerIDStats = 0;
        }
        m_playerIDSkills = 0;
        if (playerStatMenu.activeInHierarchy == true)
        {
            //Makes sure the objects only need to be found once
            if (m_findTextGameObjects == false)
            {

                statText = GameObject.Find("Player_Stats_Values").GetComponent<TextMeshProUGUI>();

                m_findTextGameObjects = true;
            }
            if (m_playerIDStats == 0)
            {

                statText.text = " Name: Luck" + "\n " + "Class: Cleric" + "\n " + "Health: " + m_playerManager.cleric.GetMaxHealth() + "\n Mana: " + m_playerManager.cleric.GetMaxMana() + "\n Physical Damage: " + m_playerManager.cleric.GetStrength() + "\n Magical Damage: " + m_playerManager.cleric.GetMagicPower() +
                 "\n Physical Defence: " + m_playerManager.cleric.GetDefence() + "\n Magical Defence: " + m_playerManager.cleric.GetMagicDefence() + "\n Speed: " + m_playerManager.cleric.GetSpeed();
                //expBar.value = cleric.GetEXP();
        
                Debug.Log("Cleric");
            }
            if (m_playerIDStats == 1)
            {
                statText.text = " Name: Buck" + "\n " + "Class: Warrior" + "\n " + "Health: " + m_playerManager.warrior.GetMaxHealth() + "\n Mana: " + m_playerManager.warrior.GetMaxMana() + "\n Physical Damage: " + m_playerManager.warrior.GetStrength() + "\n Magical Damage: " + m_playerManager.warrior.GetMagicPower() +
                 "\n Physical Defence: " + m_playerManager.warrior.GetDefence() + "\n Magical Defence: " + m_playerManager.warrior.GetMagicDefence() + "\n Speed: " + m_playerManager.warrior.GetSpeed();
                //expBar.value = m_playerManager.warrior.
                Debug.Log("Warrior");
            }
            if (m_playerIDStats == 2)
            {
            
                statText.text = " Name: Duck" + "\n " + "Class: Wizard" + "\n " + "Health: " + m_playerManager.wizard.GetMaxHealth() + "\n Mana: " + m_playerManager.wizard.GetMaxMana() + "\n Physical Damage: " + m_playerManager.wizard.GetStrength() + "\n Magical Damage: " + m_playerManager.wizard.GetMagicPower() +
              "\n Physical Defence: " + m_playerManager.wizard.GetDefence() + "\n Magical Defence: " + m_playerManager.wizard.GetMagicDefence() + "\n Speed: " + m_playerManager.wizard.GetSpeed();
                //expBar.value = wizard.GetEXP();
                Debug.Log("Wizard");
            }
            if (m_playerIDStats == 3)
            { 
            
                statText.text = " Name: Phil" + "\n " + "Class: Ninja" + "\n " + "Health: " + m_playerManager.ninja.GetMaxHealth() + "\n Mana: " + m_playerManager.ninja.GetMaxMana() + "\n Physical Damage: " + m_playerManager.ninja.GetStrength() + "\n Magical Damage: " + m_playerManager.ninja.GetMagicPower() +
             "\n Physical Defence: " + m_playerManager.ninja.GetDefence() + "\n Magical Defence: " + m_playerManager.ninja.GetMagicDefence() + "\n Speed: " + m_playerManager.ninja.GetSpeed();
                // expBar.value = ninja.GetEXP();
                Debug.Log("Ninja");
            }

        }
        if (playerStatMenu.activeInHierarchy == false)
        {
            m_findTextGameObjects = false;
        }
    }
}
