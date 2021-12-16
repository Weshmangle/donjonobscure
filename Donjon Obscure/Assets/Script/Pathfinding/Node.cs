
public class Node
{
    public float g;
    public float h;
    public float f;
    public Node parent;
    public Node position;

    public Node(Node parent = null, Node position = null)
    {
        this.parent = parent;
        this.position = position;
        this.g = 0;
        this.f = 0;
        this.h = 0;
    }

    public override bool Equals(object other)
    {
        if(other.GetType() == typeof(Node))
        {
            Node node = other as Node;
            return this.position == node.position;
        }
        else
        {
            return false;
        }
    }
}