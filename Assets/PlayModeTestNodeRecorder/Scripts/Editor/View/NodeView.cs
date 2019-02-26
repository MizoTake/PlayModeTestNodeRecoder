using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    using Line = LineView;
    using Type = NodeType;
    partial class NodeView : IViewable
    {
        private Rect rect;
        private Line end;
        private Type type;
        public Rect ViewableRect => rect;
        public int? Id { get; set; } = null;
        public Line StartLine { get; set; }
        public Line EndLine
        {
            get
            {
                return end;
            }
            set
            {
                var line = value;
                line.UpdateEndPoint (new Vector2 (rect.x, rect.y + rect.height / 2f));
                end = line;
            }
        }

        public NodeView (Vector2 position, Vector2 size)
        {
            rect = new Rect (position.x, position.y, size.x, size.y);
        }

        public NodeView (Type type, Vector2 position, Vector2 size)
        {
            this.type = type;
            rect = new Rect (position.x, position.y, size.x, size.y);
        }

        static void DrawNodeWindow (int id)
        {
            GUI.DragWindow ();
        }
    }

    partial class NodeView : ISelected
    {
        public string Selected =>
            throw new System.NotImplementedException ();
    }

    partial class NodeView : IDrawable
    {
        public void Draw ()
        {
            GUI.Window (Id.Value, rect, NodeView.DrawNodeWindow, "NodeView" + Id.Value);
        }
    }
}