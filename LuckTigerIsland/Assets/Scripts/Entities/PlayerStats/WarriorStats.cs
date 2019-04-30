using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorStats : Stats {
	void Awake()
	{
		SetPlayerStats(150 + ((PlayerManager.Instance.GetLevel() - 1) * 10), 20 + ((PlayerManager.Instance.GetLevel() - 1) * 3), 20 + ((PlayerManager.Instance.GetLevel() - 1) * 3),
			10 + ((PlayerManager.Instance.GetLevel() - 1) * 2), 45, 50 + ((PlayerManager.Instance.GetLevel() - 1) * 5), 5 + ((PlayerManager.Instance.GetLevel() - 1) * 2));
		Debug.Log("Warrior Stats Set");
	}
}