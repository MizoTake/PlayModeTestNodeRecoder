using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly : InternalsVisibleTo ("Tests.EditMode")]
namespace PlayModeTestNodeRecorder
{
    using Node = NodeView;
    using Line = LineView;
    using CreateScript = CreateScriptModel;
    sealed class TestNodeWindowViewModel
    {
        private CreateScript craeteScript = new CreateScript ();
        private Node lastCreatedNodeOfLine = null;
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

        public void CreateLine (Node begin, Vector2 mousePosition)
        {
            var line = new Line (begin.BeginLinePoint (), mousePosition);
            if (begin.BeginLine != null)
            {
                // Lineが被らないようにする
                lineViews.Remove (begin.BeginLine);
                begin.BeginLine = null;
            }
            lastCreatedNodeOfLine = begin;
            begin.BeginLine = line;
            LastCreatedLine = line;
            lineViews.Add (line);
        }

        public void ConnectNode (Vector2 mousePos)
        {
            var selectedNode = ClickOnNode (mousePos);
            // Nodeを選ばなければ消す
            if (selectedNode == null)
            {
                lastCreatedNodeOfLine.BeginLine = null;
                lineViews.Remove (LastCreatedLine);
            }
            else
            {
                // 同じNodeに紐づけない
                if (selectedNode.BeginLine == LastCreatedLine)
                {
                    selectedNode.BeginLine = null;
                    return;
                }
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

        public void SavingScriptFile (string fieldText)
        {
            craeteScript.SavingFile (fieldText);
        }
    }
}