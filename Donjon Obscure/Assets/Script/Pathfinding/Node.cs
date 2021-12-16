public class Node
{
    public float g;
    public float h;
    public float f;
    protected Node parent;
    protected Node position;

    public Node(Node parent = null, Node position = null)
    {
        
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