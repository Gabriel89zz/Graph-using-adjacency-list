using System.Xml.Linq;

namespace Graph_using_adjacency_list
{
    public partial class Form1 : Form
    {
        private Graph graph;
        public Form1()
        {
            InitializeComponent();
            graph = new Graph();
        }

        private void btnAddNode_Click(object sender, EventArgs e)
        {
            string nodeName = txtNode.Text.Trim();
            if (!string.IsNullOrEmpty(nodeName))
            {
                Node newNode = new Node(nodeName);
                graph.AddNode(newNode);
                MessageBox.Show($"Nodo '{nodeName}' añadido.");
                txtNode.Clear();
            }
        }

        //private void btnAddEdge_Click(object sender, EventArgs e)
        //{
        //    string fromNodeName = txtFrom.Text.Trim();
        //    string toNodeName = txtTo.Text.Trim();

        //    Node fromNode = graph.Nodes.Find(n => n.Name == fromNodeName);
        //    Node toNode = graph.Nodes.Find(n => n.Name == toNodeName);

        //    if (fromNode != null && toNode != null)
        //    {
        //        graph.AddEdge(fromNode, toNode);
        //        MessageBox.Show($"Arista añadida de '{fromNodeName}' a '{toNodeName}'.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Uno o ambos nodos no existen.");
        //    }
        //}



        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            string fromNodeName = txtFrom.Text.Trim();
            string toNodeName = txtTo.Text.Trim();
            string weightText = txtWeight.Text.Trim(); // Asumimos que tienes un campo de texto para el peso

            Node fromNode = graph.Nodes.Find(n => n.Name == fromNodeName);
            Node toNode = graph.Nodes.Find(n => n.Name == toNodeName);

            if (fromNode != null && toNode != null)
            {
                // Si el campo de peso no está vacío, se usa el constructor con peso
                if (!string.IsNullOrEmpty(weightText) && int.TryParse(weightText, out int weight))
                {
                    if (chkDirected.Checked)
                    {
                        graph.AddEdge(fromNode, toNode, weight);
                        MessageBox.Show($"Arista añadida de '{fromNodeName}' a '{toNodeName}' con peso {weight}.");
                    }
                    else
                    {
                        graph.AddNoDirectedEdge(fromNode, toNode,weight);
                        MessageBox.Show($"Arista añadida de '{fromNodeName}' a '{toNodeName}' con peso {weight}.");
                    }
                }
                else
                {
                    // Si no se especifica peso, se usa el constructor sin peso
                    if (chkDirected.Checked)
                    {
                        graph.AddEdge(fromNode, toNode);
                        MessageBox.Show($"Arista añadida de '{fromNodeName}' a '{toNodeName}'.");
                    }
                    else
                    {
                        graph.AddNoDirectedEdge(fromNode, toNode);
                        MessageBox.Show($"Arista añadida de '{fromNodeName}' a '{toNodeName}'.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Uno o ambos nodos no existen.");
            }
        }


        private void btnRemoveNode_Click(object sender, EventArgs e)
        {
            string nodeName = txtNode.Text.Trim();
            Node nodeToRemove = graph.Nodes.Find(n => n.Name == nodeName);

            if (nodeToRemove != null)
            {
                graph.RemoveNode(nodeToRemove);
                MessageBox.Show($"Nodo '{nodeName}' eliminado.");
                txtNode.Clear();
            }
            else
            {
                MessageBox.Show("Nodo no encontrado.");
            }
        }


        //hacer que remueva un arista no dirigida
        private void btnRemoveEdge_Click(object sender, EventArgs e)
        {
            string fromNodeName = txtFrom.Text.Trim();
            string toNodeName = txtTo.Text.Trim();

            Node fromNode = graph.Nodes.Find(n => n.Name == fromNodeName);
            Node toNode = graph.Nodes.Find(n => n.Name == toNodeName);

            if (fromNode != null && toNode != null)
            {
                graph.RemoveEdge(fromNode, toNode);
                MessageBox.Show($"Arista eliminada de '{fromNodeName}' a '{toNodeName}'.");
            }
            else
            {
                MessageBox.Show("Uno o ambos nodos no existen.");
            }
        }

        private void btnShowAdjacencyList_Click(object sender, EventArgs e)
        {
            string weightText = txtWeight.Text.Trim();
            if (!string.IsNullOrEmpty(weightText) && int.TryParse(weightText, out int weight))
            {
                txtGraphRepresentation.Text = graph.ShowAdjacencyListWithWeights();
            }
            else
            {
                txtGraphRepresentation.Text = graph.ShowAdjacencyList();
            }
        }

        private void btnShowDFS_Click(object sender, EventArgs e)
        {
            txtGraphRepresentation.Clear();
            // Obtener el nombre del nodo de inicio desde una TextBox (puede ser un número o una palabra)
            string startNodeName = txtStartNode.Text;

            // Buscar el nodo en el grafo cuyo nombre coincida con el ingresado
            Node startNode = graph.Nodes.FirstOrDefault(n => n.Name == startNodeName);

            if (startNode != null)
            {
                // Realizar DFS desde el nodo encontrado
                string result = graph.DFS(startNode);
                txtGraphRepresentation.Text = result;  // Mostrar el resultado en el TextBox
            }
            else
            {
                // Si el nodo no existe, mostrar un mensaje de error
                txtGraphRepresentation.Text = "Nodo no encontrado.";
            }
        }

        private void btnShowBFS_Click(object sender, EventArgs e)
        {
            txtGraphRepresentation.Clear();
            // Obtener el nombre del nodo de inicio desde una TextBox (puede ser un número o una palabra)
            string startNodeName = txtStartNode.Text;

            // Buscar el nodo en el grafo cuyo nombre coincida con el ingresado
            Node startNode = graph.Nodes.FirstOrDefault(n => n.Name == startNodeName);

            if (startNode != null)
            {
                // Realizar DFS desde el nodo encontrado
                string result = graph.BFSIterative(startNode);
                txtGraphRepresentation.Text = result;  // Mostrar el resultado en el TextBox
            }
            else
            {
                // Si el nodo no existe, mostrar un mensaje de error
                txtGraphRepresentation.Text = "Nodo no encontrado.";
            }
        }
    }
}
