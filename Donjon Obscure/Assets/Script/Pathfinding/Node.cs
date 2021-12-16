public class Node
{
    protected float g;
    protected float h;
    protected float f;
    protected Node parent;
    protected Node position;

    public Node(Node parent = null, Node position = null)
    {
        
    }

    public override bool Equals(Node other)
    {
        return this.position == other.position;
    }
}