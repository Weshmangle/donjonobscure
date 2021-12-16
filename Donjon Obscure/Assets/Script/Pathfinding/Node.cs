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

    public override bool Equals(Node other)
    {
        return this.position == other.position;
    }
}