using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateNight : MonoBehaviour
{

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

		Generate();
	}

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
					/*for (int xoff = -4; xoff < 4; xoff++)
					{
						for(int yoff = -4; yoff <4; yoff++)
						{
							if(Mathf.Abs(yoff) + Mathf.Abs(xoff) <= 1)
							{
								LightTo(0, new Vector3Int(x + xoff, y+ yoff, 0));
							}
							else if (Mathf.Abs(yoff) + Mathf.Abs(xoff) <= 2)
							{
								LightTo(1, new Vector3Int(x + xoff, y + yoff, 0));
							}
							else if (Mathf.Abs(yoff) + Mathf.Abs(xoff) <= 3)
							{
								LightTo(2, new Vector3Int(x + xoff, y + yoff, 0));
							}
							else if (Mathf.Abs(yoff) + Mathf.Abs(xoff) <= 4)
							{
								LightTo(3, new Vector3Int(x + xoff, y + yoff, 0));
							}
						}
					}*/
					
					for(int xoff = -range; xoff <= range; xoff++)
					{
						for (int yoff = -range; yoff <= range; yoff++)
						{
							float lightval = Vector3.Magnitude(new Vector3(xoff , yoff,0))/range;
							lightval = (lightval * 4);
							print(lightval);
							LightTo((int)lightval, new Vector3Int(x + xoff, y + yoff, 0));
						}
					}

				}
				
			}
		}

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
