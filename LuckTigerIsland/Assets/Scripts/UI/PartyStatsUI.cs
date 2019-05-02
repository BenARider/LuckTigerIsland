using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PartyStatsUI : Entity {
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
        expBar.maxValue = m_playerManager.GetLevel() * 100 + (m_playerManager.GetLevel() * 10);
        expBar.value = m_playerManager.GetXP();
        m_partyLevelTotal.text = "" + m_playerManager.GetLevel();
        statText.text = " Name: Luck" + "\n " + "Class: Cleric" + "\n " + "Health: " + m_playerManager.cleric.GetMaxHealth() + "\n Mana: " + m_playerManager.cleric.GetMaxMana() + "\n Physical Damage: " + m_playerManager.cleric.GetStrength() + "\n Magical Damage: " + m_playerManager.cleric.GetMagicPower() +
        "\n Physical Defence: " + m_playerManager.cleric.GetDefence() + "\n Magical Defence: " + m_playerManager.cleric.GetMagicDefence() + "\n Speed: " + m_playerManager.cleric.GetSpeed();
        m_skillTitle.text = attacks[0].attackName;
        skillImage.sprite = attacks[0].attackImage;
        m_skillDescription.text = attacks[0].attackDescription;
        m_skillDamage.text = "Damage: " + attacks[0].attackDamage;
        m_skillCost.text = "Mana Cost: " + attacks[0].attackCost;
    }
	
	// Update is called once per frame
	void Update () {
        m_partyLevelTotal.text = "" + m_playerManager.GetLevel();
        expBar.maxValue = m_playerManager.GetLevel() * 100 + (m_playerManager.GetLevel() * 10);
        expBar.value = m_playerManager.GetXP();
        Debug.Log("Exp bar" + expBar.value);
        Debug.Log("Exp " + m_playerManager.GetXP());
    }
    public int GetPlayerStatID()
    {
        return m_playerIDStats;
    }
    private void OnEnable()
    {
        m_playerIDSkills = 0;
        m_playerIDStats = 0;
        m_skillTitle.text = attacks[0].attackName;
        skillImage.sprite = attacks[0].attackImage;
        m_skillDescription.text = attacks[0].attackDescription;
        m_skillDamage.text = "Damage: " + attacks[0].attackDamage;
        m_skillCost.text = "Mana Cost: " + attacks[0].attackCost;
        statText.text = " Name: Luck" + "\n " + "Class: Cleric" + "\n " + "Health: " + m_playerManager.cleric.GetMaxHealth() + "\n Mana: " + m_playerManager.cleric.GetMaxMana() + "\n Physical Damage: " + m_playerManager.cleric.GetStrength() + "\n Magical Damage: " + m_playerManager.cleric.GetMagicPower() +
        "\n Physical Defence: " + m_playerManager.cleric.GetDefence() + "\n Magical Defence: " + m_playerManager.cleric.GetMagicDefence() + "\n Speed: " + m_playerManager.cleric.GetSpeed();
    }
    //Used to control the party stat menu by setting and finding all the text values/objects
    public void IncreaseSkillID()
    {
        m_playerIDSkills += 1;
     
        if (m_playerIDStats == 0)//Cleric
        {
            if (m_playerIDSkills == 0)
            {
                m_skillTitle.text = attacks[0].attackName;
                skillImage.sprite = attacks[0].attackImage;
                m_skillDescription.text = attacks[0].attackDescription;
                m_skillDamage.text = "Damage: " + attacks[0].attackDamage;
                m_skillCost.text = "Mana Cost: " + attacks[0].attackCost;
            }
            if (m_playerIDSkills == 1)
            {
                m_skillTitle.text = attacks[1].attackName;
                skillImage.sprite = attacks[1].attackImage;
                m_skillDescription.text = attacks[1].attackDescription;
                m_skillDamage.text = "Damage: " + attacks[1].attackDamage;
                m_skillCost.text = "Mana Cost: " + attacks[1].attackCost;
            }
            if (m_playerIDSkills == 2)
            {
                m_skillTitle.text = attacks[2].attackName;
                skillImage.sprite = attacks[2].attackImage;
                m_skillDescription.text = attacks[2].attackDescription;
                m_skillDamage.text = "Damage: " + attacks[2].attackDamage;
                m_skillCost.text = "Mana Cost: " + attacks[2].attackCost;
            }
            if (m_playerIDSkills == 3)
            {
                m_skillTitle.text =  attacks[3].attackName;
                skillImage.sprite =  attacks[3].attackImage;
                m_skillDescription.text =  attacks[3].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[3].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[3].attackCost;
            }
            if (m_playerIDSkills == 4)
            {
                m_skillTitle.text =  attacks[4].attackName;
                skillImage.sprite =  attacks[4].attackImage;
                m_skillDescription.text =  attacks[4].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[4].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[4].attackCost;
            }
            if (m_playerIDSkills == 5)
            {
                m_skillTitle.text =  attacks[5].attackName;
                skillImage.sprite =  attacks[5].attackImage;
                m_skillDescription.text =  attacks[5].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[5].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[5].attackCost;
            }
            if (m_playerIDSkills == 6)
            {
                m_skillTitle.text =  attacks[6].attackName;
                skillImage.sprite =  attacks[6].attackImage;
                m_skillDescription.text =  attacks[6].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[6].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[6].attackCost;
            }
            if (m_playerIDSkills == 6)
            {
                m_playerIDSkills = 0;
            }

        }
        if (m_playerIDStats == 1)//Warrior
        {
            if (m_playerIDSkills == 0)
            {
                m_skillTitle.text =  attacks[0].attackName;
                skillImage.sprite =  attacks[0].attackImage;
                m_skillDescription.text =  attacks[0].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[0].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[0].attackCost;
            }
            if (m_playerIDSkills == 1)
            {
                m_skillTitle.text =  attacks[1].attackName;
                skillImage.sprite =  attacks[1].attackImage;
                m_skillDescription.text =  attacks[1].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[1].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[1].attackCost;
            }
            if (m_playerIDSkills == 2)
            {
                m_skillTitle.text =  attacks[7].attackName;
                skillImage.sprite =  attacks[7].attackImage;
                m_skillDescription.text =  attacks[7].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[7].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[7].attackCost;
            }
            if (m_playerIDSkills == 3)
            {
                m_skillTitle.text =  attacks[8].attackName;
                skillImage.sprite =  attacks[8].attackImage;
                m_skillDescription.text =  attacks[8].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[8].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[8].attackCost;
            }
            if (m_playerIDSkills == 4)
            {
                m_skillTitle.text =  attacks[9].attackName;
                skillImage.sprite =  attacks[9].attackImage;
                m_skillDescription.text =  attacks[9].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[9].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[9].attackCost;
            }
            if (m_playerIDSkills == 5)
            {
                m_skillTitle.text =  attacks[10].attackName;
                skillImage.sprite =  attacks[10].attackImage;
                m_skillDescription.text =  attacks[10].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[10].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[10].attackCost;
            }
            if (m_playerIDSkills == 6)
            {
                m_playerIDSkills = 0;
            }
        }
        if (m_playerIDStats==2)
        {
            if (m_playerIDSkills == 0)
            {
                m_skillTitle.text =  attacks[0].attackName;
                skillImage.sprite =  attacks[0].attackImage;
                m_skillDescription.text =  attacks[0].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[0].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[0].attackCost;
            }
            if (m_playerIDSkills == 1)
            {
                m_skillTitle.text =  attacks[1].attackName;
                skillImage.sprite =  attacks[1].attackImage;
                m_skillDescription.text =  attacks[1].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[1].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[1].attackCost;
            }
            if (m_playerIDSkills == 2)
            {
                m_skillTitle.text =  attacks[11].attackName;
                skillImage.sprite =  attacks[11].attackImage;
                m_skillDescription.text =  attacks[11].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[11].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[11].attackCost;
            }
            if (m_playerIDSkills == 3)
            {
                m_skillTitle.text =  attacks[12].attackName;
                skillImage.sprite =  attacks[12].attackImage;
                m_skillDescription.text =  attacks[12].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[12].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[12].attackCost;
            }
            if (m_playerIDSkills == 4)
            {
                m_skillTitle.text =  attacks[13].attackName;
                skillImage.sprite =  attacks[13].attackImage;
                m_skillDescription.text =  attacks[13].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[13].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[13].attackCost;
            }
            if (m_playerIDSkills == 5)
            {
                m_skillTitle.text =  attacks[14].attackName;
                skillImage.sprite =  attacks[14].attackImage;
                m_skillDescription.text =  attacks[14].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[14].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[14].attackCost;
            }
            if (m_playerIDSkills == 6)
            {
                m_playerIDSkills = 0;
            }
        }
        if (m_playerIDStats==3)
        {
            if (m_playerIDSkills == 0)
            {
                m_skillTitle.text =  attacks[0].attackName;
                skillImage.sprite =  attacks[0].attackImage;
                m_skillDescription.text =  attacks[0].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[0].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[0].attackCost;
            }
            if (m_playerIDSkills == 1)
            {
                m_skillTitle.text =  attacks[1].attackName;
                skillImage.sprite =  attacks[1].attackImage;
                m_skillDescription.text =  attacks[1].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[1].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[1].attackCost;
            }
            if (m_playerIDSkills == 2)
            {
                m_skillTitle.text =  attacks[15].attackName;
                skillImage.sprite =  attacks[15].attackImage;
                m_skillDescription.text =  attacks[15].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[15].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[15].attackCost;
            }
            if (m_playerIDSkills == 3)
            {
                m_skillTitle.text =  attacks[16].attackName;
                skillImage.sprite =  attacks[16].attackImage;
                m_skillDescription.text =  attacks[16].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[16].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[16].attackCost;
            }
            if (m_playerIDSkills == 4)
            {
                m_skillTitle.text =  attacks[17].attackName;
                skillImage.sprite =  attacks[17].attackImage;
                m_skillDescription.text =  attacks[17].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[17].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[17].attackCost;
            }
            if (m_playerIDSkills == 5)
            {
                m_skillTitle.text =  attacks[18].attackName;
                skillImage.sprite =  attacks[18].attackImage;
                m_skillDescription.text =  attacks[18].attackDescription;
                m_skillDamage.text = "Damage: " +  attacks[18].attackDamage;
                m_skillCost.text = "Mana Cost: " +  attacks[18].attackCost;
            }
            if (m_playerIDSkills == 6)
            {
                m_playerIDSkills = 0;
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
