using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bar_Testing : MonoBehaviour {
    public PlayerChara player;

    [SerializeField] private Slider m_healthBar;
	// Use this for initialization
	private void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        m_healthBar.value = player.GetHealth();
        Debug.Log(m_healthBar.value);
      
    }
}
