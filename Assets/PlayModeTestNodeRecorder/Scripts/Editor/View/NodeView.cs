using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    using Line = LineView;
    using Type = NodeType;
    abstract partial class NodeView : IViewable
    {
        private Rect rect;
        private Line end;
        public int? Id { get; set; } = null;
        public Type Type { get; private set; }
        public Rect ViewableRect => rect;
        public Line BeginLine { get; set; }
        public Line EndLine
        {
            get
            {
                return end;
            }
            set
            {
                var line = value;
                line.UpdateEndPoint (EndLinePoint ());
                end = line;
            }
        }
        public NodeView BeforeNode { get; set; }

        public NodeView (Type type, Vector2 position, Vector2 size)
        {
            this.Type = type;
            rect = new Rect (position.x, position.y, size.x, size.y);
        }

        public void Move (Vector2 position)
        {
            // TODO: centerで良いのかはいずれ考える、処理としては仕様と言い張れるので一旦は良い
            rect.center = position;
            BeginLine?.UpdateBeginPoint (BeginLinePoint ());
            EndLine?.UpdateEndPoint (EndLinePoint ());
        }

        public Vector2 BeginLinePoint ()
        {
            var lineX = rect.x + rect.width;
            var lineY = rect.y + (rect.height / 2f);
            return new Vector2 (lineX, lineY);
        }

        private Vector2 EndLinePoint ()
        {
            return new Vector2 (rect.x, rect.y + rect.height / 2f);
        }

        protected abstract void DrawNodeWindow (int id);

    }

    partial class NodeView : ISelected
    {
        public bool Selected =>
            throw new System.NotImplementedException ();
    }

    partial class NodeView : IDrawable
    {
        public void Draw ()
        {
            GUI.Window (Id.Value, rect, DrawNodeWindow, Type.ToString ());
        }
    }
}