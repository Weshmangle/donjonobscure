using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    public Tile[,] tiles;
    public int Width = 10;
    public int Height = 10;
    public Tile Entry;
    public Tile Exit;
    private int currentLevelSeed = 0;

    public Tile[] GetTiles()
    {
        return transform.GetComponentsInChildren<Tile>();
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
        if(transform.childCount != 0) return;
        
        tiles = new Tile[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject obj = Instantiate(Resources.Load("Prefabs/Tile"), new Vector3(x,0,y), transform.rotation, transform) as GameObject;
                Tile tile = obj.GetComponent<Tile>();
                tile.Position = new Vector2Int(x,y);
                tiles[x,y] = obj.GetComponent<Tile>();
            }
        }

        // GetComponent<ElementsGenerator>().OldGenerateElement(tiles, this);
        GridSeed seed = Resources.Load("Seeds/test") as GridSeed;
        GetComponent<ElementsGenerator>().GenerateElementFromSeed(tiles, seed, this);

    }
    public void LoadNextLevel()
    {
        GridSeed seed = Resources.Load("Seeds/level" + currentLevelSeed.ToString()) as GridSeed;
        GetComponent<ElementsGenerator>().GenerateElementFromSeed(tiles, seed, this);
        currentLevelSeed++;
    }

    public Vector2Int RandomNeibgbour(Vector2Int position)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (Mathf.Abs(x) == Mathf.Abs(y))
                    continue;

                int checkX = position.x + x;
                int checkY = position.y + y;

                if (checkX > 0 && checkX < Width - 1 && checkY > 0 && checkY < Height - 1)
                {
                    neighbours.Add(new Vector2Int(checkX, checkY));
                }
            }
        }

        return neighbours[Random.Range(0, neighbours.Count)];
    }

    public void SetTileContent(Vector2Int position, ElementGrid content)
    {
        tiles[position.x, position.y].Content = content;
    }
}
