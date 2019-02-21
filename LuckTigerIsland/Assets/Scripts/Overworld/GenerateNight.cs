using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateNight : MonoBehaviour
{

    Transform playerTransform;
    Vector3Int playerPos;
    Vector3Int lastPlayerPos;

    int[,] previousLights = new int[5,5];


	public Tilemap dayMap;
	public Tilemap nightMap;
	public Vector2Int bound = new Vector2Int(100, 100);
	public int range = 4;
	public Tile torch;
	public Tile darkness1;
	public Tile darkness2;
	public Tile darkness3;
	public Tile darkness4;

	List<Vector3Int> lightPositions;


	void Start()
	{
        playerTransform = PlayerManager.Instance.transform;

        playerPos = Vector3Int.RoundToInt(playerTransform.position);

        for (int x = 0; x < 4; x++)
        {
            for(int y = 0; y < 4; y++)
            {
                previousLights[x, y] = TileToStep(nightMap.GetTile<Tile>(new Vector3Int(playerPos.x +(x-2), playerPos.y + (y - 2),0)));
                //print (TileToStep(nightMap.GetTile<Tile>(new Vector3Int(playerPos.x + (x - 2), playerPos.y + (y - 2), 0))));
            }
        }


    }


    void Update()
    {
        lastPlayerPos = playerPos;
        playerPos = Vector3Int.RoundToInt(playerTransform.position);
        
        if(playerPos != lastPlayerPos)
        {
            updateLights(playerPos - lastPlayerPos);
        }

    }

    void updateLights(Vector3Int delta)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                nightMap.SetTile(new Vector3Int(lastPlayerPos.x + (x - 2), lastPlayerPos.y + (y - 2), 0), StepToTile(previousLights[x, y]));
                previousLights[x, y] = TileToStep(nightMap.GetTile<Tile>(new Vector3Int(playerPos.x + (x - 2), playerPos.y + (y - 2), 0)));
                print(nightMap.GetTile<Tile>(new Vector3Int(playerPos.x + (x - 2), playerPos.y + (y - 2), 0)));
                //nightMap.SetTile(new Vector3Int(lastPlayerPos.x + (x - 2), lastPlayerPos.y + (y - 2), 0), StepToTile(Mathf.Clamp(Mathf.Abs(x-2)+Mathf.Abs(y-2),0,4)));
                LightTo(1, new Vector3Int(lastPlayerPos.x + (x - 2), lastPlayerPos.y + (y - 2), 0));

                
            }
        }


    }

    [ContextMenu("Generate")]
	public void Generate()
	{
		nightMap.FloodFill(Vector3Int.zero, darkness4);
		//BoundsInt bounds = dayMap.cellBounds;
		//TileBase[] allTiles = dayMap.GetTilesBlock(bounds);

		for (int x = -bound.x; x < bound.x; x++)
		{
			for (int y = -bound.y; y < bound.y; y++)
			{
				//TileBase tile = allTiles[x + y * bounds.size.x];
				TileBase tile = dayMap.GetTile(new Vector3Int(x, y, 0));
				if (tile != null)
				{
					nightMap.SetTile(new Vector3Int(x, y, 0), torch);
					
					
					for(int xoff = -range; xoff <= range; xoff++)
					{
						for (int yoff = -range; yoff <= range; yoff++)
						{
							float lightval = Vector3.Magnitude(new Vector3(xoff , yoff,0))/range;
							lightval = (lightval * 4);

							LightTo((int)lightval, new Vector3Int(x + xoff, y + yoff, 0));

						}
					}

				}
				
			}
		}

	}

    int TileToStep(Tile tile)
    {
        if(tile == null)
        {
            return 0;
        }
        if(tile.name == darkness4.name)
        {
            return 4;
        }
        if (tile.name == darkness3.name)
        {
            return 3;
        }
        if (tile.name == darkness2.name)
        {
            return 2;
        }
        if (tile.name == darkness1.name)
        {
            return 1;
        }
        return 0;
    }

    TileBase StepToTile(int step)
    {
        switch (step)
        {
            case 0: return null;
            case 1: return darkness1;
            case 2: return darkness2;
            case 3: return darkness3;
            case 4: return darkness4;
        }
        return null;
    }

    public void LightTo(int step, Vector3Int pos)
	{
		Tile tileAt = nightMap.GetTile<Tile>(pos);

		if (tileAt == null)
		{
			return;
		}

		Tile targetTile;
		switch (step)
		{
			case 0:
				targetTile = null;
				break;
			case 1:
				targetTile = darkness1;
				break;
			case 2:
				targetTile = darkness2;
				break;
			case 3:
				targetTile = darkness3;
				break;
			default:
				return;
		}

		if(tileAt.name == darkness4.name || tileAt.name == darkness3.name)
		{
			nightMap.SetTile(pos,targetTile);
			return;
		}
		if (tileAt.name == darkness2.name)
		{
			if (step < 2)
			{
				nightMap.SetTile(pos, targetTile);
			}
			return;
		}
		if (tileAt.name == darkness1.name)
		{
			if (step < 1)
			{
				nightMap.SetTile(pos, null);
			}
			return;
		}
	}
}
