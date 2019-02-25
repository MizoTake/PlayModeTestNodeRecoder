using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeRecorderTest
{
    using Line = LineView;
    partial class NodeView
    {
        private Rect rect;
        private Line end;
        public Vector2 Position => new Vector2 (rect.x, rect.height);
        public Vector2 CenterHeightPositon => new Vector2 (rect.x, rect.y / 2.0f);
        public Vector2 Size => new Vector2 (rect.width, rect.height);
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

        static void DrawNodeWindow (int id)
        {
            GUI.DragWindow ();
        }
    }

    partial class NodeView : IDrawable
    {
        public void Draw ()
        {
            GUI.Window (Id.Value, rect, NodeView.DrawNodeWindow, "NodeView" + Id.Value);
        }
    }
}