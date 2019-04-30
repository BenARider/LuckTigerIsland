using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStats : Stats {

	void Start()
	{
		SetPlayerStats(100 + ((PM.GetLevel() - 1) * 5), 10 + ((PM.GetLevel() - 1) * 2), 5 + ((PM.GetLevel() - 1) * 2),
			15 + ((PM.GetLevel() - 1) * 3), 40, 50 + ((PM.GetLevel() - 1) * 10), 20 + ((PM.GetLevel() - 1) * 3));
		Debug.Log("Wizard Stats Set");
	}
}
