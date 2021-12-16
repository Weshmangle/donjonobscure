    using System.Collections.Generic;
public class PathFinding
{
    public static List<Node> search(Node[,] grid, Node from, Node to)
    {
        Node start =  new Node(null, from);
        start.g = start.h = start.f = 0;
        Node end =  new Node(null, to);
        end.g = end.h = end.f = 0;

        List<Node> open_list = new List<Node>();
        List<Node> closed_list = new List<Node>();

        open_list.Add(start);

        while(open_list.Count > 0)
        {
            Node current_node = open_list[0];
            int current_index = 0;

            for (var index = 0; index < open_list.Count; index++)
            {
                Node item = open_list[index];
                
                if(item.f < current_node.f)
                {
                    current_node = item;
                    current_index = index;
                }

                open_list.RemoveAt(current_index);
                closed_list.Add(current_node);

                if(current_node == end)
                {
                    List<Node> path = new List<Node>();
                    Node current = current_node;
                    
                    while(current != null)
                    {
                        path.Add(current.position);
                        current = current.parent;
                    }
                    path.Reverse();
                    return path;
                }

                List<Node> children = new List<Node>();
                
                foreach (var new_position in children)
                {
                    //new_position
                }
            } 
        }

        return new List<Node>();
    }
}