using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleControl : MonoBehaviour {

    public static int currentTurn;
	public static string willDamage; //state n for no damage taken, state y for damage taken. Alternate state could indicate invulnerable or other in future.
	public static int currentDamage;
    public static int currentHealValue;
	public static int currentTarget;
	public static string side;
	public static int totalFighters = 8; //will be used to check when to reset the turn timer and stuff.

    // Use this for initialization
    void Start () {
		currentTurn = 1;
		willDamage = "n";
		currentDamage = 0;
        currentHealValue = 0;
		currentTarget = 0;
		Debug.Log("Setup complete");
	}
	void Update ()
	{
		//Debug.Log(willDamage + " " + currentDamage + "On target: " + side + " " + currentTarget);
	}

}