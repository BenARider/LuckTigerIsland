using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorStats : Stats {
	void Start()
	{
		SetPlayerStats(150 + ((PM.GetLevel() - 1) * 10), 20 + ((PM.GetLevel() - 1) * 3), 20 + ((PM.GetLevel() - 1) * 3),
			10 + ((PM.GetLevel() - 1) * 2), 45, 50 + ((PM.GetLevel() - 1) * 5), 5 + ((PM.GetLevel() - 1) * 2));
		Debug.Log("Warrior Stats Set");
	}
}