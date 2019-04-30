using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericStats : Stats {

	void Awake()
	{
		SetPlayerStats(125 + ((PlayerManager.Instance.GetLevel()-1) * 7), 10 + ((PlayerManager.Instance.GetLevel() - 1) * 2), 10 + ((PlayerManager.Instance.GetLevel() - 1) * 2),
			20 + ((PlayerManager.Instance.GetLevel() - 1) * 3), 50, 40 + ((PlayerManager.Instance.GetLevel() - 1) * 10), 15 + ((PlayerManager.Instance.GetLevel() - 1) * 3));
		Debug.Log("Cleric Stats Set");
	}
}