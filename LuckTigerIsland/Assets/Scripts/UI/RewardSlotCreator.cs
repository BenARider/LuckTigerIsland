using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class RewardSlotCreator : MonoBehaviour {
    [SerializeField]
    Transform m_parentTransform;
    [SerializeField]
    RewardUI m_rewardUI;
    [SerializeField]
    EventSystem m_eventSystem;
    [SerializeField]
    GameObject m_closeObject;
    Rewards m_rewards;
    private bool m_startIncrease;
    // Use this for initialization
    void Start () {
        m_eventSystem = EventSystem.current;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnEnable()
    {
        m_eventSystem.SetSelectedGameObject(m_closeObject);
        m_rewardUI = GameObject.Find("Slot1").GetComponent<RewardUI>();
        m_rewards = GameObject.Find("RewardUI").GetComponent<Rewards>();
        for (int i = 0; i < 4; ++i)
        {
            if (m_startIncrease == false)
            {
                m_rewardUI.SetNameNumSet(0);
            }
            m_rewardUI = Instantiate(m_rewardUI, new Vector2(i * + 250, m_parentTransform.transform.position.y), m_parentTransform.rotation);
            m_rewardUI.transform.SetParent(m_parentTransform, false);
            m_rewardUI.m_rewardButton.interactable = true;
            if (m_startIncrease == true)
            {
                m_rewardUI.SetNameNumSet(1);
            }
            m_startIncrease = true;
            m_rewardUI.ItemTitle.text = m_rewards.m_rewards[m_rewardUI.GetNameNumSet()].objectName;
        }
    }
    private void OnDisable()
    {
        foreach (Transform child in m_parentTransform.transform)
        {
            Destroy(child.gameObject);
            m_startIncrease = false;
        }
        m_rewardUI.m_rewardButton.interactable = false;

    }
}
