using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_using_adjacency_list
{
    internal class Node
    {
        public string Name { get; set; }
        public List<Edge> Neighbors { get; set; }

        public Node(string name)
        {
            Name = name;
            Neighbors = new List<Edge>();
        }
    }
}
