using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStats : Stats {

	void Awake()
	{
		SetPlayerStats(100 + ((PlayerManager.Instance.GetLevel() - 1) * 5), 15 + ((PlayerManager.Instance.GetLevel() - 1) * 3), 10 + ((PlayerManager.Instance.GetLevel() - 1) * 2),
			10 + ((PlayerManager.Instance.GetLevel() - 1) * 2), 30, 50 + ((PlayerManager.Instance.GetLevel() - 1) * 7), 15 + ((PlayerManager.Instance.GetLevel() - 1) * 3));
		Debug.Log("Ninja Stats Set");
	}
}
