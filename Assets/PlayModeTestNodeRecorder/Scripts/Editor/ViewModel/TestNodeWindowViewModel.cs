using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[assembly : InternalsVisibleTo ("Tests.EditMode")]
namespace PlayModeTestNodeRecorder
{
    using Node = NodeView;
    using Line = LineView;
    using CreateScript = CreateScriptModel;
    sealed class TestNodeWindowViewModel
    {
        private Config config;
        private CreateScript craeteScript = new CreateScript ();
        private Node chaceBeforeNode = null;
        private BeginNodeView beginNode;
        private Node lastCreatedNodeOfLine = null;
        private List<Node> nodeViews = new List<Node> ();
        private List<Line> lineViews = new List<Line> ();
        public IReadOnlyList<Node> NodeViews => nodeViews;
        public IReadOnlyList<Line> LineViews => lineViews;
        public Line LastCreatedLine { get; private set; }

        public Config LoadConfig ()
        {
            var config = Resources.Load<Config> ("Config");
            if (config == null)
            {
                config = Config.Create ();
            }
            this.config = config;
            return config;
        }

        // TODO: Type別で引数を作る？
        public void CreateNode (NodeType type, Vector2 position)
        {
            Node node = null;
            switch (type)
            {
                case NodeType.Begin:
                    beginNode = (BeginNodeView) type.ToNode (position);
                    node = beginNode;
                    break;
                default:
                    node = type.ToNode (position);
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
            chaceBeforeNode = begin;
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
                chaceBeforeNode.NextNode = selectedNode;
                chaceBeforeNode = null;
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
            if (fieldText == "") return;
            var nodeList = new List<Node> ();
            var next = beginNode.NextNode;
            while (next != null)
            {
                nodeList.Add (next);
                next = next.NextNode;
            }
            craeteScript.SavingFile (config.SavingPath, fieldText, nodeList.ToArray ());
        }
    }
}