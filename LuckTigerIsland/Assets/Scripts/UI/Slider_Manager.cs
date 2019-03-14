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
    public Transform sliderTransform;
    [SerializeField] private Slider m_healthSlider = null;
    [SerializeField] private Slider m_speedSlider = null;
    [SerializeField] private Slider m_manaSlider = null;
	// Use this for initialization
	void Start () {
        sliderTransform = GameObject.Find("Enemy_1").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isEnemySlider == true)
        {
        this.transform.position = new Vector2(sliderTransform.position.x-47,sliderTransform.position.y+4-(78*(player.GetEntityNo()-1)));		//can get the values of specificly tagged gameObjects. 

        }
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
