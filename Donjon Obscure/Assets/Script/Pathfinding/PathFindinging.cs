using System.Collections.Generic;
using UnityEngine;

public class Noding
{
    public Noding parent;
    public Vector2Int position;
    public float g = 0;
    public float h = 0;
    public float f = 0;

    public Noding(Noding parent, Vector2Int position)
    {
        this.parent = parent;
        this.position = position;
    }

    public override bool Equals(object other)
    {
        return ((Node)other) != null && position.Equals(((Node)other).position);
    }
}

public class PathFindinging
{
    public static List<Vector2Int> GetPath(bool[,] grid, Vector2Int start, Vector2Int end)
    {
        Noding startNode = new Noding(null, start);
        startNode.g = startNode.h = startNode.f = 0;
        Noding endNode = new Noding(null, end);
        endNode.g = endNode.h = endNode.f = 0;

        List<Noding> openList = new List<Noding>();
        List<Noding> closeList = new List<Noding>();

        openList.Add(startNode);

        while(openList.Count > 0)
        {
            Noding currentNode = openList[0];
            int currentIndex = 0;

            for (int index = 0; index < openList.Count; index++)
            {
                Noding item = openList[index];
                if(item.f < currentNode.f)
                {
                    currentNode = item;
                    currentIndex = index;
                }
            }
 
            openList.RemoveAt(currentIndex);
            closeList.Add(currentNode);

            if(currentNode.Equals(endNode))
            {
                List<Vector2Int> path = new List<Vector2Int>();
                Noding current = currentNode;

                while(current != null)
                {
                    path.Add(current.position);
                    current = current.parent;
                }

                path.Reverse();
                
                return path;
            }

            List<Noding> children = new List<Noding>();

            Vector2Int [] neighbor = {new Vector2Int(0,-1), new Vector2Int(0,1), new Vector2Int(-1,0), new Vector2Int(1,0)};

            for (int i = 0; i < 6; i++)
            {
                Vector2Int nodePosition = currentNode.position + neighbor[i];
                
                if(grid[nodePosition.x, nodePosition.y])
                {
                    children.Add(new Noding(currentNode, nodePosition));
                }
            }

            foreach (var child in children)
            {
                foreach (var closedChild in closeList)
                {
                    if(child == closedChild)
                    {
                        continue;
                    }
                }
                
                child.g = currentNode.g + 1;
                child.h = (child.position - endNode.position).sqrMagnitude;
                            
                child.f = child.g + child.h;

                foreach (var openNode in openList)
                {
                    if(child == openNode  && child.g > openNode.g)
                    {
                        continue;
                    }
                }

                openList.Add(child);
            }
        }

        Debug.Log("NO PATH");

        return new List<Vector2Int>();
    }
}