using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly : InternalsVisibleTo ("Tests")]
namespace PlayModeTestNodeRecorder
{
    using Node = NodeView;
    using Line = LineView;
    sealed class TestNodeWindowViewModel
    {
        private List<Node> nodeViews = new List<Node> ();
        private List<Line> lineViews = new List<Line> ();
        public IReadOnlyList<Node> NodeViews => nodeViews;
        public IReadOnlyList<Line> LineViews => lineViews;
        public Line LastCreatedLine { get; private set; }

        public void CreateNode (NodeType type, Vector2 position)
        {
            Node node = null;
            switch (type)
            {
                case NodeType.Touch:
                    node = new TouchNodeView (position, Vector2.one * 100);
                    break;
            }
            node.Id = nodeViews.Count;
            nodeViews.Add (node);
        }

        public void CreateLine (Node start, Vector2 mousePosition, Node selected)
        {
            var lineX = start.ViewableRect.x + start.ViewableRect.width;
            var lineY = start.ViewableRect.y + (start.ViewableRect.height / 2f);
            var line = new Line (new Vector2 (lineX, lineY), mousePosition);
            selected.StartLine = line;
            LastCreatedLine = line;
            lineViews.Add (line);
        }

        public void ConnectNode (Vector2 mousePos)
        {
            if (LastCreatedLine == null) return;
            var selectedNode = ClickOnNode (mousePos);
            selectedNode.EndLine = LastCreatedLine;
            LastCreatedLine = null;
        }

        public Node ClickOnNode (Vector2 mousePos)
        {
            Node result = null;
            for (int i = 0; i < nodeViews.Count; i++)
            {
                if (nodeViews[i].ViewableRect.Contains (mousePos))
                {
                    result = nodeViews[i];
                    break;
                }
            }
            return result;
        }
    }
}