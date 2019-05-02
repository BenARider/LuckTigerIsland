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

	}

	// Update is called once per frame
	void Update ()
    {
        if(isEnemySlider == true)
        {
            if (player.GetEntityNo() == 1)
            {
                sliderTransform = GameObject.Find("Enemy_1").GetComponent<Transform>();
                transform.position = new Vector2(sliderTransform.position.x+1993, sliderTransform.position.y +473 + (1*(player.GetEntityNo())));//x 78 //y 8 or //x 1983 //y 473
            }
            if (player.GetEntityNo() == 2)
            {
                sliderTransform = GameObject.Find("Enemy_2").GetComponent<Transform>();
                transform.position = new Vector2(sliderTransform.position.x+78, sliderTransform.position.y + (4 * (player.GetEntityNo())));
            }
            if (player.GetEntityNo() == 3)
            {
                sliderTransform = GameObject.Find("Enemy_3").GetComponent<Transform>();
                transform.position = new Vector2(sliderTransform.position.x +78, sliderTransform.position.y + 8 + (1 * (player.GetEntityNo())));
            }
            if (player.GetEntityNo() == 4)
            {
                sliderTransform = GameObject.Find("Enemy_4").GetComponent<Transform>();
                transform.position = new Vector2(sliderTransform.position.x + 78, sliderTransform.position.y + (2 * (player.GetEntityNo())));
            }
           	//can get the values of specificly tagged gameObjects.

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
