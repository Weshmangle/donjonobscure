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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FindPath(grid.Entry, grid.Exit);
            Debug.Log("findingPath");
        }
    }
    void FindPath(Tile startPosition, Tile endPosition)
    {
        // pick start location
        startNode = new Node(startPosition.Position);
        // pick goal location
        endNode = new Node(endPosition.Position);

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

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                ElementGrid content = grid.getTile(new Vector2Int(neighbour.position.x, neighbour.position.y)).Content;
                if (content is Chest || content is Hole || closed.Contains(neighbour))
                    continue;

                float newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, endNode);
                if(newCostToNeighbour < neighbour.gCost || !open.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.heuristic = GetDistance(neighbour, endNode);
                    neighbour.parent = currentNode;

                    if(!open.Contains(neighbour))
                    {
                        open.Add(neighbour);
                    }
                }
            }
        }
    }


    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        grid.path = path;
        string pathLog = "";
        foreach (Node node in path)
        {
            pathLog += "\n";
            pathLog += node.position;

        }
        Debug.Log(pathLog);
    }
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.position.x - nodeB.position.x);
        int distanceY = Mathf.Abs(nodeA.position.y - nodeB.position.y);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}