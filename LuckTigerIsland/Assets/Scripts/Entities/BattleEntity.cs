using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : MonoBehaviour {

	/// <summary>
	/// NOW DEPRECEATED. KEEPING FOR THE TIME BEING, BUT WILL BE DELETED SOON
	/// </summary>
    [SerializeField]
    protected int m_Health;
    [SerializeField]
    protected int m_Damage;
    [SerializeField]
    protected int m_Speed;
    [SerializeField]
    protected int m_HealValue;
    
    
    
    
    
    
    

    public float GetSpeed() { return m_Speed; }

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    { 
       
    }
   

    void HealTarget(int HealValue)
    {
        m_Health += HealValue;
    }
}

