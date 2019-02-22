using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly : InternalsVisibleTo ("Tests")]
namespace PlayModeRecoderTest
{
    using Node = NodeView;
    using Line = LineView;
    class TestNodeWindowViewModel
    {
        private List<Node> nodeViews = new List<Node> ();
        private List<Line> lineViews = new List<Line> ();
        public IReadOnlyList<Node> NodeViews => nodeViews;
        public IReadOnlyList<Line> LineViews => lineViews;
        public Line LastCreatedLine => lineViews.Count > 0 ? lineViews.Last () : null;

        public void CreateNode (Vector2 position)
        {
            var node = new Node (position, Vector2.one * 100);
            node.Id = nodeViews.Count;
            nodeViews.Add (node);
        }

        public void CreateLine (Node start, Vector2 mousePosition)
        {
            var line = new Line (start.CenterHeightPositon, mousePosition);
            lineViews.Add (line);
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