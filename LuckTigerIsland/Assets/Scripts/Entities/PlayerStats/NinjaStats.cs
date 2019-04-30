using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStats : Stats {

	void Start()
	{
		SetPlayerStats(100 + ((PM.GetLevel() - 1) * 5), 15 + ((PM.GetLevel() - 1) * 3), 10 + ((PM.GetLevel() - 1) * 2),
			10 + ((PM.GetLevel() - 1) * 2), 30, 50 + ((PM.GetLevel() - 1) * 7), 15 + ((PM.GetLevel() - 1) * 3));
		Debug.Log("Ninja Stats Set");
	}
}
