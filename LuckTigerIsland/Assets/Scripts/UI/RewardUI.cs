using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class RewardUI : MonoBehaviour, ISelectHandler,IDeselectHandler
{
    BaseEventData m_baseEvent = null;
    public TextMeshProUGUI ItemTitle;
    public TextMeshProUGUI ItemDescription;
    public Button m_rewardButton;
    [SerializeField]
    private int m_nameNumSet;//Could be used in combo with random number for rewards
    Inventory m_inventory;
    [SerializeField]
    InventoryObject m_item;
    [SerializeField]
    Image m_image;
    Rewards m_rewards;
    // Use this for initialization
    void Start()
    {
        m_inventory = GameObject.Find("Player").GetComponent<Inventory>();
        m_image = GetComponent<Image>();
        m_rewards = GameObject.Find("RewardUI").GetComponent<Rewards>();
      
     
    }
    public int GetNameNumSet()
    {
        return m_nameNumSet;
    }
    public void SetNameNumSet(int _nameNum)
    {
        m_nameNumSet += _nameNum;
    }
    // Update is called once per frame
    void Update()
    {

        m_item = m_rewards.rewards[m_nameNumSet];
        m_image.sprite = m_rewards.rewards[m_nameNumSet].Image;
    }
    public void OnSelect(BaseEventData _data)
    {
        ItemDescription.text = m_rewards.rewards[m_nameNumSet].Description;
    }
    public void OnDeselect(BaseEventData _data)
    {
        ItemDescription.text = "";
    }
    public void AddToInventory()
    {
       for(int i = 0; i< m_rewards.rewards.Count; ++i)
        {
            m_inventory.AddToInventory(m_rewards.rewards[i]);
        }
    
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
