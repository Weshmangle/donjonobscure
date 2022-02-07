using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PathFinding : MonoBehaviour
{
    public Grid grid;
    public Material closedMaterial;
    public Material openMaterial;

    public List<Node> open = new List<Node>();
    public List<Node> closed = new List<Node>();

    public GameObject startNodePrefab;
    public GameObject endNodePrefab;
    public GameObject nodePrefab;

    GameObject lastPostHightlight;

    Node endNode;
    Node startNode;
    Node currentNode;

    private void Awake()
    {
        grid = Game.game.room.grid;
    }

    void BeginSearch(Vector2Int startPosition, Vector2Int endPosition)
    {
        
        //// get all locations of that aint a wall
        //List<Vector2Int> positions = new List<Vector2Int>();

        //for (int z = 1; z < grid.depth - 1; z++)
        //{
        //    for (int x = 1; x < grid.width - 1; x++)
        //    {
        //        if (grid.map[x, z] != 1)
        //            locations.Add(new Vector2Int(x, z));
        //    }
        //}

        // pick start location
        startNode = new Node(startPosition, 0, 0, 0, Instantiate(startNodePrefab, new Vector3(startPosition.x, 0, startPosition.y), Quaternion.identity), null);
        // pick goal location
        endNode = new Node(endPosition, 0, 0, 0, Instantiate(endNodePrefab, new Vector3(endPosition.x, 0, endPosition.y), Quaternion.identity), null);

        open.Clear();
        closed.Clear();

        open.Add(startNode);
        currentNode = startNode;

        while (open.Count > 0)
        {
            currentNode = open.OrderBy(node => node.fCost).First();
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < currentNode.fCost || open[i].fCost == currentNode.fCost)
                {
                    if (open[i].heuristic < currentNode.heuristic)
                        currentNode = open[i];
                }
            }

            open.Remove(currentNode);
            closed.Add(currentNode);

            if (currentNode == endNode)
            {
                RetracePath(startNode, endNode);
                return;
            }

            foreach (Vector2Int neighbour in grid.GetNeighbours(currentNode))
            {
                if (grid.getTile(new Vector2Int(neighbour.x, neighbour.y)).Content is Wall)
                    continue;
            }
        }
    }

    private void RetracePath(Node startNode, Node endNode)
    {
        
    }
}