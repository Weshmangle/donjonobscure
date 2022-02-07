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
    protected Tile[,] tiles;

    [SerializeField]
    protected ElementsGenerator elementGenerator;


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

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                GameObject obj = Instantiate(Resources.Load("Prefabs/Tile"), new Vector3(x,0,y), transform.rotation, listTiles.transform) as GameObject;
                Tile tile = obj.GetComponent<Tile>();
                tile.Position = new Vector2Int(x,y);
                tiles[x,y] = obj.GetComponent<Tile>();
            }
        }
        elementGenerator.GenerateElement(tiles);
    }

    public void cleanGrid()
    {
        
    }

    // Up, down, left, right neighbours
    internal List<Vector2Int> GetNeighbours(Node node)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (Mathf.Abs(x) == Mathf.Abs(y))
                    continue;

                int checkX = node.position.x + x;
                int checkY = node.position.y + y;

                if (checkX >= 0 && checkX < Width-1 && checkY >= 0 && checkY < Height-1)
                {
                    neighbours.Add(new Vector2Int(checkX, checkY));
                }
            }
        }

        return neighbours;
    }
}
