﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct BoatPath
{
	[SerializeField]
	public Vector3[] node;
	public float duration;
	public Vector3 destination;
}

public class BoatScript : GenericInteract
{

	public bool isInBoat = false;
	[SerializeField]
	public BoatPath[] paths;

	//public int pathID = 0;
	//int nodeID = 0;
	float timer;


	PlayerManager player;
	PlayerWorldMove playerMove;

	// Use this for initialization
	void Start () {
		
	}

	public override void OnInteract(PlayerManager _player)
	{
		
		isInBoat = true;
		player = _player;
		playerMove = player.GetComponent<PlayerWorldMove>();
		playerMove.doMove = false;
		timer = 0;
		//nodeID = 0;
	}

	Vector3 positionOnPath(int _path, float t)
	{
		return Vector3.Lerp(Vector3.Lerp(paths[_path].node[0], paths[_path].node[1], t), Vector3.Lerp(paths[_path].node[1], paths[_path].node[2], t), t);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (isInBoat && playerMove !=null)
		{
			if (timer < paths[0].duration)
			{
				timer += Time.fixedDeltaTime;
				transform.position = positionOnPath(0, timer / paths[0].duration);
				//boat stuff
				player.transform.position = transform.position;
			}
			else
			{
				isInBoat = false;
			}
		}
		else if (playerMove !=null)
		{
			transform.position = paths[0].node[0];
			player.transform.position = paths[0].destination;
			//reset player
			playerMove.doMove = true;
			playerMove = null;
		}
	}
}
