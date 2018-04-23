using System;
using System.Collections.Generic;
using System.Text;

namespace TopologicalSort {
    class Program {
        
        static void Main(string[] args) {
            DoDXTopologicalSort();
        }
        private static void DoDXTopologicalSort() {
            Console.WriteLine("DX Topological Sorter");
            Console.WriteLine(new string('=', 21));
            Console.WriteLine("Nodes:");
            GraphNode[] list = PrepareNodes();
            PrintNodes(list);
            
            IComparer<GraphNode> comparer = new GraphNodeComparer();
            
            IList<GraphNode> sortedNodes = DevExpress.Utils.Algorithms.TopologicalSort<GraphNode>(list, comparer);

            Console.WriteLine("Sorted nodes:");
            PrintNodes(sortedNodes);

            Console.Read();
        }
        static void PrintNodes(IList<GraphNode> list) {
            for (int i = 0; i < list.Count; i++) {
                string s = string.Empty;
                if (i > 0) 
                    s = "->";
                s += list[i].Id.ToString();
                Console.Write(s);
            }
            Console.WriteLine("\r\n");
        }
        static GraphNode[] PrepareNodes() {
            GraphNode nodeA = new GraphNode("A");
            GraphNode nodeB = new GraphNode("B");
            GraphNode nodeC = new GraphNode("C");
            GraphNode nodeD = new GraphNode("D");
            GraphNode nodeE = new GraphNode("E");

            nodeA.LinkedNodes.AddRange(new GraphNode[] { nodeB, nodeC, nodeE });
            nodeB.LinkedNodes.Add(nodeD);
            nodeC.LinkedNodes.AddRange(new GraphNode[] { nodeD, nodeE });
            nodeD.LinkedNodes.Add(nodeE);

            return new GraphNode[] { nodeD, nodeA, nodeC, nodeE, nodeB };
        }
    }
 }
