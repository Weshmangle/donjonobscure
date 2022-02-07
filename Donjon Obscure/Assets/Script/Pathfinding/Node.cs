using UnityEngine;
public class Node
{

    public Vector2Int position;
    public float gCost;
    public float heuristic;
    public float fCost;
    public GameObject nodePrefab;
    public Node parent;

    public Node(Vector2Int _position, float _gCost, float _heuristic, float _fCost, GameObject _nodePrefab, Node parent)
    {
        this.parent = parent;
        this.position = _position;
        this.gCost = _gCost;
        this.heuristic = _heuristic;
        this.fCost = _fCost;
    }

    public override bool Equals(object other)
    {
        if(other.GetType() == typeof(Node))
        {
            if ((other == null) || !this.GetType().Equals(other.GetType()))
                return false;
            else
                return position.Equals(((Node)other).position);
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