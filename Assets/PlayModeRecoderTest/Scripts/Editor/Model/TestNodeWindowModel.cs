using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly : InternalsVisibleTo ("Tests")]
namespace PlayModeRecoderTest
{
    using Node = NodeView;
    class TestNodeWindowModel
    {
        private List<Node> nodeViews = new List<Node> ();
        public IReadOnlyList<Node> NodeViews => nodeViews;

        public void CreateNode (Vector2 position)
        {
            var node = new Node (position, Vector2.one * 100);
            node.Id = nodeViews.Count;
            nodeViews.Add (node);
        }
    }
}