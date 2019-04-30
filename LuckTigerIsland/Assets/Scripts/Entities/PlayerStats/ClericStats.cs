using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericStats : Stats {

	void Start()
	{
		SetPlayerStats(125 + ((PM.GetLevel()-1) * 7), 10 + ((PM.GetLevel() - 1) * 2), 10 + ((PM.GetLevel() - 1) * 2),
			20 + ((PM.GetLevel() - 1) * 3), 50, 40 + ((PM.GetLevel() - 1) * 10), 15 + ((PM.GetLevel() - 1) * 3));
		Debug.Log("Cleric Stats Set");
	}
}