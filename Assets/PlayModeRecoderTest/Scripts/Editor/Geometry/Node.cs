using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeRecoderTest
{
    public class Node : IDrawable
    {
        private Rect rect;
        public Vector2 Position => new Vector2 (rect.x, rect.height);
        public Vector2 CenterHeightPositon => new Vector2 (rect.x, rect.y / 2.0f);
        public Vector2 Size => new Vector2 (rect.width, rect.height);

        public Node (Vector2 position, Vector2 size)
        {
            rect = new Rect (position.x, position.y, size.x, size.y);
        }

        public void Draw ()
        {
            GUI.DragWindow ();
        }
    }
}