using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour {

	Tilemap encounterMap;
	Vector3Int lastPos;
	PlayerManager player;

	public bool isDesert = false;
	public float encounterChance = 0f;

    ScreenTransition fade;

	IEnumerator enumerator;
	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerManager>();
		lastPos = new Vector3Int(0,0,0);
        fade = Camera.main.GetComponent<ScreenTransition>();
    }
	
	// Update is called once per frame
	void Update () {
		if(encounterMap == null)
		{
			if (GameObject.FindGameObjectWithTag("EncounterMap"))
			{
				encounterMap = GameObject.FindGameObjectWithTag("EncounterMap").GetComponent<Tilemap>();
			}
		}
		else if(player.currentSceneName.Equals("Overworld") && player.playerMove.doMove)
		{
			Vector3Int currentPos = new Vector3Int((int)(transform.position.x - 0.5f), (int)(transform.position.y-1f),0);
			
			if(currentPos != lastPos && encounterMap.GetTile(currentPos))
			{
				Tile t = (Tile)encounterMap.GetTile(new Vector3Int(currentPos.x,currentPos.y,0));
				
				if(t.color.g > 0.5f)
				{
					isDesert = true;
				}
				else
				{
					isDesert = false;
				}
				encounterChance = t.color.r;

				if(Random.Range(0f,10f) < encounterChance)
				{
                    fade.flashWhite(0.1f);
                    if (isDesert)
                    {
                        enumerator = doEncounter(2);
                        StartCoroutine(enumerator);
                    }
                    if (!isDesert)
                    {
                        enumerator = doEncounter(1);
                        StartCoroutine(enumerator);
                    }

                    //print("encounter: " + (isDesert? "desert" : "grassland"));


                }

				lastPos = currentPos;
			}
		}
	}

	IEnumerator doEncounter(int sceneNo)
	{
		player.playerMove.doMove = false;
		yield return new WaitForSeconds(0.5f);
		fade.toBlack();
		yield return new WaitForSeconds(2f);
		encounterMap.transform.root.gameObject.SetActive(false);
		Camera.main.gameObject.SetActive(false);
        string scenename = "GrasslandBattle";
        switch (sceneNo)
        {
            case 1:
                scenename = "GrasslandBattle";
                break;
            case 2:
                scenename = "DesertBattle";
                break;

        }
        SceneManager.LoadScene(scenename, LoadSceneMode.Additive);
        player.currentSceneName = scenename;

        fade.fromBlack();
	}
}
