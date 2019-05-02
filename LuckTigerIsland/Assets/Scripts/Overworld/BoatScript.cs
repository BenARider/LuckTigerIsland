using System.Collections;
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

	bool isInBoat = false;
    int boatTransition = 0; // 1 = entering, 2 = exiting, 0 = nothing
    bool fadingTo = false;
    bool fadingFrom = false;

    float transitionTimer = 0f;

	[SerializeField]
	public BoatPath[] paths;
    public int pathID = 0;

    //public int pathID = 0;
    //int nodeID = 0;
    float timer;
    

    ScreenTransition fade;
	PlayerManager player;
	PlayerWorldMove playerMove;

	// Use this for initialization
	void Start () {
        fade = Camera.main.GetComponent<ScreenTransition>();
       
    }

	public override void OnInteract(PlayerManager _player)
	{
        boatTransition = 1;
        transitionTimer = 0f;
        //isInBoat = true;
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

    private void Update()
    {
        if(boatTransition != 0)
        {
            playerMove.doMove = false;
            if (!fadingTo)
            {
                fade.toBlack();
                fadingTo = true;
            }
            if(transitionTimer > 1f && !fadingFrom)
            {
                if (boatTransition == 1)
                {
                    player.transform.position = transform.position;
                    //player.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                    foreach(SpriteRenderer sr in PlayerManager.Instance.playerSprites)
                    {
                        sr.enabled = false;
                    }
                }
                else
                {
                    player.transform.position = paths[pathID].destination;
                    //player.transform.localScale = Vector3.one;
                    foreach (SpriteRenderer sr in PlayerManager.Instance.playerSprites)
                    {
                        sr.enabled = true;
                    }
                    transform.position = paths[pathID].node[0];
                }
                Camera.main.GetComponent<CameraController>().OnReset();
                fade.fromBlack();
                fadingFrom = true;
            }

            

            if (boatTransition == 1 && transitionTimer >2f)
            {
                //get on boat when done
                isInBoat = true;
                PlayerManager.Instance.OnListChanged();
            }


            if (boatTransition == 2 && transitionTimer > 2f)
            {
                //get off boat when done
                
                boatTransition = 0;
            }

            transitionTimer += Time.deltaTime;
        }
        

    }

    // Update is called once per frame
    void FixedUpdate () {
		if (isInBoat && playerMove !=null)
		{
			if (timer < paths[pathID].duration)
			{
				timer += Time.fixedDeltaTime;
				transform.position = positionOnPath(pathID, timer / paths[pathID].duration);
				//boat stuff
				player.transform.position = transform.position;
			}
			else
			{
                //isInBoat = false;
                isInBoat = false;
                boatTransition = 2;
                transitionTimer = 0f;
                fadingTo = false;
                fadingFrom = false;
            }
		}
		else if (boatTransition == 0 &&playerMove !=null)
		{
			
			//player.transform.position = paths[0].destination;
			//reset player
			playerMove.doMove = true;
			playerMove = null;

            isInBoat = false;
            boatTransition = 0;
            fadingTo = false;
            fadingFrom = false;

            transitionTimer = 0f;
            
        }
	}
}
