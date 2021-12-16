public class PathFinding
{
    public static void search(Node[,] grid, Node from, Node to)
    {
        Node start =  new Node(null, from);
        start.g = start.h = start.f = 0;
        Node end =  new Node(null, to);
        end.g = end.h = end.f = 0;
    }
}