using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slider_Manager : MonoBehaviour {


    public Entity player;
	public string stringname;
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
		//can get the values of specificly tagged gameObjects. 
		//if (stringname == "player1")
		//{
		//	player = GameObject.FindGameObjectWithTag("Wizard").GetComponent<EnemEntity>();
		//}
		m_healthSlider.maxValue = player.GetMaxHealth();
        m_healthSlider.value = player.GetHealth();
		m_speedSlider.value = 100 * (player.GetCurrentSpeed() % player.GetRequiredSpeed() / player.GetRequiredSpeed());
        m_manaSlider.maxValue = player.GetMaxMana();
        m_manaSlider.value = player.GetMana();

	}
}
