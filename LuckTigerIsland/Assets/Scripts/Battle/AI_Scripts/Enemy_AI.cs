using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/*public enum EnemyState //Think of mentality of the enemies, this state will change on different values changing i.e low health = fleeing.  Will add to this when i can
{ 
    eAttcking,
    eDefending, //Will code in when defending is working, Ditto for thje other three
    eHealing,
    eFleeing
}*/

/*public enum Command
{
    eAttack,
    eDefend, //Will code in when defending is working, Ditto for thje other three
    eHeal,
    eFlee
}*/

public class Enemy_AI : EnemChara
{

    /*public void StateTransition(EnemyState currentState, Command command)
    {
        CurrentState = currentState;
        Command = command;
    }*/
    // Use this for initialization
    void Start ()
    {
        
    }

	//this function can be changed depending on the enemy type. It will return the state to EnemChara for use there
    void decideState()
    {
		//if (GetHealth() < GetHealth() / 2) //would have a && healskill not on cool down later for eHeal
		//{
		//	StateTransition(1);
		//	Debug.Log("Enemy Defending");
		//}
		//else if (GetHealth() < GetHealth()/4)
  //      {
		//	StateTransition(2);
		//	Debug.Log("The enemy is fleeing");
  //      }
        
			StateTransition(0);
			//Debug.Log("The enemy is going to attack" + m_Target);
        
    }

    // Update is called once per frame
    void Update()
    {
		if (SpeedTimer.m_speedCounter % requiredSpeedForTurn == 0 && SpeedTimer.isPaused == false || SpeedTimer.m_speedCounter % requiredSpeedForTurn == 0.5 && m_attackedAlready == false && SpeedTimer.m_speedCounter > 1.0f)
		{
            SpeedTimer.isPaused = true;

            decideState();
        }
    }
}
