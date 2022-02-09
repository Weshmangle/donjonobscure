using UnityEngine;
public class Node
{

    public Vector2Int position;
    public float gCost;
    public float hCost;
    public float fCost
    {
        get { return gCost + hCost; }
    }
    bool walkable;
    public bool Walkable 
    {
        get { return walkable; }
        set { walkable = value; }
    }

    public Node parent;

    public Node(Vector2Int _position)
    {
        this.position = _position;
    }

    public override bool Equals(object other)
    {
        if(other.GetType() == typeof(Node))
        {
            if ((other == null) || !this.GetType().Equals(other.GetType()))
                return false;
            else
            {
                return position.Equals(((Node)other).position);
            }
                
        }
        else
        {
            return false;
        }
    }
    public override int GetHashCode()
    {
        return 0;
    }
}