using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeRecoderTest
{
    partial class NodeView
    {
        private Rect rect;
        public Vector2 Position => new Vector2 (rect.x, rect.height);
        public Vector2 CenterHeightPositon => new Vector2 (rect.x, rect.y / 2.0f);
        public Vector2 Size => new Vector2 (rect.width, rect.height);

        public NodeView (Vector2 position, Vector2 size)
        {
            rect = new Rect (position.x, position.y, size.x, size.y);
        }
    }

    partial class NodeView : IDrawable
    {
        public void Draw ()
        {
            GUI.DragWindow ();
        }
    }
}