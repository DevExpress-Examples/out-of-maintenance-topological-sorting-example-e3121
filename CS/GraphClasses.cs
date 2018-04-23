using System;
using System.Collections.Generic;
using System.Text;

namespace TopologicalSort {
    public class GraphNode {
        List<GraphNode> linkedNodes = new List<GraphNode>();
        object id;
        public GraphNode(object id) {
            this.id = id;
        }
        public List<GraphNode> LinkedNodes { get { return linkedNodes; } }
        public object Id { get { return id; } }
    }

    public class GraphNodeComparer : IComparer<GraphNode> {
        #region IComparer<GraphNode> Members

        public int Compare(GraphNode x, GraphNode y) {
            if (x.LinkedNodes.Contains(y))
                return -1;
            if (y.LinkedNodes.Contains(x))
                return 1;
            return 0;
        }

        #endregion
    }
}
