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
                // Verificar si el nodo ya existe en el grafo
                if (graph.Nodes.Any(node => node.Name == nodeName))
                {
                    MessageBox.Show($"The node '{nodeName}' already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    // Si no existe, añadir el nodo
                    Node newNode = new Node(nodeName);
                    graph.AddNode(newNode);
                    MessageBox.Show($"Node '{nodeName}' added.", "Success", MessageBoxButtons.OK);
                    txtNode.Clear();
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
            }
        }


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
                        MessageBox.Show($"Edge added from '{fromNodeName}' to '{toNodeName}' with weight {weight}.");
                    }
                    else
                    {
                        graph.AddNoDirectedEdge(fromNode, toNode, weight);
                        MessageBox.Show($"Edge added from '{fromNodeName}' to '{toNodeName}' with weight {weight}.");
                    }
                    txtGraphRepresentation.Text = graph.ShowAdjacencyListWithWeights();
                }
                else
                {
                    // Si no se especifica peso, se usa el constructor sin peso
                    if (chkDirected.Checked)
                    {
                        graph.AddEdge(fromNode, toNode);
                        MessageBox.Show($"Edge added from '{fromNodeName}' to '{toNodeName}'.");
                    }
                    else
                    {
                        graph.AddNoDirectedEdge(fromNode, toNode);
                        MessageBox.Show($"Edge added from '{fromNodeName}' to '{toNodeName}'.");
                    }
                    txtGraphRepresentation.Text = graph.ShowAdjacencyList();
                }
            }
            else
            {
                MessageBox.Show("One or both nodes do not exist.");
            }
        }


        private void btnRemoveNode_Click(object sender, EventArgs e)
        {
            string nodeName = txtNode.Text.Trim();
            Node nodeToRemove = graph.Nodes.Find(n => n.Name == nodeName);

            if (nodeToRemove != null)
            {
                graph.RemoveNode(nodeToRemove);
                MessageBox.Show($"Node '{nodeName}' deleted.");
                txtNode.Clear();
            }
            else
            {
                MessageBox.Show("Node not found.");
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
                MessageBox.Show($"Edge removed from '{fromNodeName}' to '{toNodeName}'.");
            }
            else
            {
                MessageBox.Show("One or both nodes do not exist.");
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
                string result = graph.DFSIterative(startNode);
                txtGraphRepresentation.Text = result;  // Mostrar el resultado en el TextBox
            }
            else
            {
                // Si el nodo no existe, mostrar un mensaje de error
                txtGraphRepresentation.Text = "Node not found.";
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
                string result = graph.BFS(startNode);
                txtGraphRepresentation.Text = result;  // Mostrar el resultado en el TextBox
            }
            else
            {
                // Si el nodo no existe, mostrar un mensaje de error
                txtGraphRepresentation.Text = "Node not found.";
            }
        }

        private void btnShowDFSRecursive_Click(object sender, EventArgs e)
        {
            txtGraphRepresentation.Clear();
            // Obtener el nombre del nodo de inicio desde una TextBox (puede ser un número o una palabra)
            string startNodeName = txtStartNode.Text;
            Node startNode = graph.Nodes.FirstOrDefault(n => n.Name == startNodeName);
            if (startNode != null)
            {
                string result = graph.DFS(startNode);
                txtGraphRepresentation.Text = result;
            }
            else
            {
                txtGraphRepresentation.Text = "Node not found.";
            }

        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            //add a validation in english
            DialogResult result = MessageBox.Show("Are you sure you want to remove all nodes and edges?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                graph.RemoveGraph();
                MessageBox.Show("All nodes and edges removed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGraphRepresentation.Clear();

            }
        }
    }
}
