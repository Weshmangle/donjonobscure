using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Pathfinding : MonoBehaviour
{
    public Grid grid;

    public List<Node> open = new List<Node>();
    public List<Node> closed = new List<Node>();

    Node endNode;
    Node startNode;
    Node currentNode;

    private void Start()
    {
        grid = Game.game.room.grid;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(FindPath(grid.Entry, grid.Exit));
        }
    }
    
    public bool FindPath(Tile startPosition, Tile endPosition)
    {
        // pick start location
        startNode = grid.NodeFromTile(startPosition);
        endNode = grid.NodeFromTile(endPosition);
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
                RetracePath(startNode, endNode);
                return true;

            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.Walkable || closed.Contains(neighbour))
                    continue;
                float newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, endNode);
                if(newCostToNeighbour < neighbour.gCost || !open.Contains(neighbour))
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
        return false;
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