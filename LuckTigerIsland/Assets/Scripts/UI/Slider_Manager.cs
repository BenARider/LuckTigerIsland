using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slider_Manager : MonoBehaviour {


    public Entity player;

	private bool hasSetSlider = false;
	[SerializeField]
	private int m_requiredEntityNumber;
	[SerializeField]
	private bool isEnemySlider;
	private BattleControl BC;

    [SerializeField] private Slider m_healthSlider = null;
    [SerializeField] private Slider m_speedSlider = null;
    [SerializeField] private Slider m_manaSlider = null;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		//enemies
		if(isEnemySlider && !hasSetSlider)
		{
			Debug.Log("Enemy slider done");
			player = BC.EnemiesInBattle[m_requiredEntityNumber].GetComponent<Entity>();
			hasSetSlider = true;
		}
		//players
		if(!isEnemySlider && !hasSetSlider)
		{
			Debug.Log("Player slider done");
			BC.PartyMembersInBattle[m_requiredEntityNumber].GetComponent<Entity>();
			hasSetSlider = true;
		}
        m_healthSlider.maxValue = player.GetMaxHealth();
        m_healthSlider.value = player.GetHealth();
		m_speedSlider.value = 100 * (player.GetCurrentSpeed() % player.GetRequiredSpeed() / player.GetRequiredSpeed());
        m_manaSlider.maxValue = player.GetMaxMana();
        m_manaSlider.value = player.GetMana();

	}
}
