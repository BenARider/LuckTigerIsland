using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Bar_Test : MonoBehaviour {

	[SerializeField]
    private Transform m_bar;
	// Use this for initialization
	private void Start () {

        m_bar = transform.Find("Health_Bar");
	}
	public void SetBarSize(float _sizeNormalized)
    {
        m_bar.localScale = new Vector2(_sizeNormalized, 1.0f);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
