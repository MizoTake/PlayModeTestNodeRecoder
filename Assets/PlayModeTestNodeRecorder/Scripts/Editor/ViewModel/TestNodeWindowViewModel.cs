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

        public void CreateLine (Node start, Vector2 mousePosition)
        {
            var line = new Line (start.StartLinePoint (), mousePosition);
            if (start.StartLine != null)
            {
                // Lineが被らないようにする
                lineViews.Remove (start.StartLine);
                start.StartLine = null;
            }
            start.StartLine = line;
            LastCreatedLine = line;
            lineViews.Add (line);
        }

        public void ConnectNode (Vector2 mousePos)
        {
            var selectedNode = ClickOnNode (mousePos);
            // Nodeを選ばなければ消す
            if (selectedNode == null)
            {
                lineViews.Remove (LastCreatedLine);
            }
            else
            {
                // 同じNodeに紐づけない
                if (selectedNode.StartLine == LastCreatedLine) return;
                selectedNode.EndLine = LastCreatedLine;
            }
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