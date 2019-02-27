using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slider_Manager : MonoBehaviour {


    public Entity player;


    [SerializeField] private Slider m_healthSlider = null;
    [SerializeField] private Slider m_speedSlider = null;
    [SerializeField] private Slider m_manaSlider = null;
   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		m_healthSlider.value =  (player.GetHealth() / player.GetMaxHealth()) * 100;
		m_speedSlider.value = 100 * (player.GetCurrentSpeed() % player.GetRequiredSpeed() / player.GetRequiredSpeed());
		m_manaSlider.value = player.GetMana() / player.GetMaxMana() * 100;

	}
}
