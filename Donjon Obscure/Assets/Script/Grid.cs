using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    [SerializeField]
    protected Transform tile;
    public int Width = 10;
    public int Height = 10;

    [SerializeField]
    protected GameObject listTiles;
    public Tile[,] tiles;

    [SerializeField]
    protected ElementsGenerator elementGenerator;

    
    public Tile Entry;
    public Tile Exit;

    
    public Tile[] GetTiles()
    {
        return listTiles.transform.GetComponentsInChildren<Tile>();
    }

    public Tile getTile(Vector2Int vector2)
    {
        foreach (var tile in this.GetTiles())
        {
            if(tile.Position.x == vector2.x && tile.Position.y == vector2.y )
            {
                return tile;
            }
        }
        return null;
    }

    public void createGrid()
    {
        if(this.listTiles.transform.childCount != 0) return;
        
        tiles = new Tile[Width, Height];
        

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject obj = Instantiate(Resources.Load("Prefabs/Tile"), new Vector3(x,0,y), transform.rotation, listTiles.transform) as GameObject;
                Tile tile = obj.GetComponent<Tile>();
                tile.Position = new Vector2Int(x,y);
                tiles[x,y] = obj.GetComponent<Tile>();

            }
        }
        elementGenerator.GenerateElement(tiles, this);
        
    }

    public void cleanGrid()
    {
        
    }
    
    
    
}
