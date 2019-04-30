using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStats : Stats {

	void Awake()
	{
		SetPlayerStats(100 + ((PlayerManager.Instance.GetLevel() - 1) * 5), 10 + ((PlayerManager.Instance.GetLevel() - 1) * 2), 5 + ((PlayerManager.Instance.GetLevel() - 1) * 2),
			15 + ((PlayerManager.Instance.GetLevel() - 1) * 3), 40, 50 + ((PlayerManager.Instance.GetLevel() - 1) * 10), 20 + ((PlayerManager.Instance.GetLevel() - 1) * 3));
		Debug.Log("Wizard Stats Set");
	}
}
