using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

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

    public List<Node> path;
    public Tile Entry;
    public Tile Exit;

    public Node[,] nodeGrid;
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
        nodeGrid = new Node[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject obj = Instantiate(Resources.Load("Prefabs/Tile"), new Vector3(x,0,y), transform.rotation, listTiles.transform) as GameObject;
                Tile tile = obj.GetComponent<Tile>();
                tile.Position = new Vector2Int(x,y);
                tiles[x,y] = obj.GetComponent<Tile>();
                nodeGrid[x, y] = new Node(new Vector2Int(x, y));

            }
        }
        elementGenerator.GenerateElement(tiles);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if(tiles[x, y].Content is Chest || tiles[x,y].Content is Hole || tiles[x, y].Content is Wall)
                {
                    nodeGrid[x, y].Walkable = false;
                }
                else nodeGrid[x, y].Walkable = true;


            }
        }
    }

    public void cleanGrid()
    {
        
    }

    // Up, down, left, right neighbours
    internal List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (Mathf.Abs(x) == Mathf.Abs(y))
                    continue;

                int checkX = node.position.x + x;
                int checkY = node.position.y + y;

                if (checkX >= 0 && checkX < Width && checkY >= 0 && checkY < Height)
                {
                    neighbours.Add(nodeGrid[checkX, checkY]);
                }
                //if (checkX > 0 && checkX < Width - 1 && checkY > 0 && checkY < Height - 1)
                //{
                //    neighbours.Add(nodeGrid[checkX, checkY]);
                //}
            }
        }

        return neighbours;
    }
    public Node NodeFromTile(Tile _tile)
    {
        Tile tile = getTile(_tile.Position);
        return nodeGrid[tile.Position.x, tile.Position.y];
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, 1, Height));

        if (nodeGrid != null)
        {
            foreach (Node n in nodeGrid)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                GUI.color = Color.cyan;
                Handles.Label(new Vector3(n.position.x - .3f, 0, n.position.y + .3f), "" + n.fCost + " " + n.gCost + " " + n.hCost);
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                Gizmos.DrawCube(new Vector3(n.position.x, 0, n.position.y), Vector3.one * .3f);
            }
        }
    }
}
