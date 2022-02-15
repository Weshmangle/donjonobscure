using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public static class Pathfinding
{
    public static Grid grid;

    public static List<Node> open = new List<Node>();
    public static List<Node> closed = new List<Node>();
    public static Node[,] nodeGrid;

    static Node endNode;
    static Node startNode;
    static Node currentNode;
    
    public static List<Vector2Int> FindPath(Tile startTile, Tile endTile, Grid _grid)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        grid = _grid;
        CreateNodeGrid(grid.Width, grid.Height);
        // pick start location
        startNode = NodeFromTile(startTile);
        endNode = NodeFromTile(endTile);
        // pick goal location


        open.Clear();
        closed.Clear();

        open.Add(startNode);
        currentNode = startNode;

        while (open.Count > 0)
        {

            currentNode = open.OrderBy(node => node.fCost).First();
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost <= currentNode.fCost)
                {
                    if (open[i].hCost < currentNode.hCost)
                        currentNode = open[i];
                }
            }

            open.Remove(currentNode);
            closed.Add(currentNode);

            if (currentNode.Equals(endNode))
            {
                
                Node currentNode = endNode;

                while (currentNode != startNode)
                {
                    path.Add(currentNode.position);
                    currentNode = currentNode.parent;
                }
                path.Reverse();
                return path;
            }

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (!neighbour.Walkable || closed.Contains(neighbour))
                    continue;
                float newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, endNode);
                if (newCostToNeighbour < neighbour.gCost || !open.Contains(neighbour))
                {

                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parent = currentNode;
                    if (!open.Contains(neighbour))
                    {
                        open.Add(neighbour);
                    }
                }
            }
        }
        return null;
    }
    public static bool FindPathFromTile(Tile startTile, Tile endTile, Grid _grid)
    {
        return FindPath(startTile, endTile, _grid) != null;
    }
    public static bool FindPathFromPosition(Vector2Int startPosition, Vector2Int endPosition, Grid _grid)
    {
         return FindPath(_grid.getTile(startPosition), _grid.getTile(endPosition), _grid) != null;
    }
    public static List<Vector2Int> GetPathFromPosition(Vector2Int startPosition, Vector2Int endPosition, Grid _grid)
    {
        return FindPath(_grid.getTile(startPosition), _grid.getTile(endPosition), _grid);
    }

    static void CreateNodeGrid(int width, int height)
    {
        nodeGrid = new Node[grid.Width, grid.Height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                nodeGrid[x, y] = new Node(new Vector2Int(x, y));
                if (grid.tiles[x, y].Content is Chest || grid.tiles[x, y].Content is Hole || grid.tiles[x, y].Content is Wall)
                {
                    nodeGrid[x, y].Walkable = false;
                }
            }
        }
        //for (int x = 0; x < width; x++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        if (grid.tiles[x, y].Content is Chest || grid.tiles[x, y].Content is Hole || grid.tiles[x, y].Content is Wall)
        //        {
        //            nodeGrid[x, y].Walkable = false;
        //        }
        //        else nodeGrid[x, y].Walkable = true;


        //    }
        //}
    }
    static List<Node> GetNeighbours(Node node)
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

                if (checkX >= 0 && checkX < grid.Width && checkY >= 0 && checkY < grid.Height)
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
    static Node NodeFromTile(Tile _tile)
    {
        Tile tile = grid.getTile(_tile.Position);
        return nodeGrid[tile.Position.x, tile.Position.y];
    }

    static int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.position.x - nodeB.position.x);
        int distanceY = Mathf.Abs(nodeA.position.y - nodeB.position.y);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
    //static void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(Vector3.zero, new Vector3(grid.Width, 1, grid.Height));

    //    if (nodeGrid != null)
    //    {
    //        foreach (Node n in nodeGrid)
    //        {
    //            Gizmos.color = (n.Walkable) ? Color.white : Color.red;
    //            GUI.color = Color.cyan;
    //            Handles.Label(new Vector3(n.position.x - .3f, 0, n.position.y + .3f), "" + n.fCost + " " + n.gCost + " " + n.hCost);
    //            if (path != null)
    //                if (path.Contains(n))
    //                    Gizmos.color = Color.black;
    //            Gizmos.DrawCube(new Vector3(n.position.x, 0, n.position.y), Vector3.one * .3f);
    //        }
    //    }
    //}
}