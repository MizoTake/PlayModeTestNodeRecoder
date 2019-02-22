using System.Collections;
using System.Collections.Generic;
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

        public void CreateNode (Vector2 position)
        {
            var node = new Node (position, Vector2.one * 100);
            node.Id = nodeViews.Count;
            nodeViews.Add (node);
        }

        public bool ClickOnNode (Vector2 mousePos)
        {
            bool onNode = false;
            for (int i = 0; i < nodeViews.Count; i++)
            {
                if (nodeViews[i].ViewableRect.Contains (mousePos))
                {
                    onNode = true;
                    break;
                }
            }
            return onNode;
        }
    }
}