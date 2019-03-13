using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	[SerializeField]
	private Transform enemySpawnLocation;
	[SerializeField]
	private GameObject[] enemies;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < 4; i++)
		{
			Instantiate(enemies[Random.Range(0, enemies.Length)], enemySpawnLocation.position, enemySpawnLocation.rotation);
		}
		Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
