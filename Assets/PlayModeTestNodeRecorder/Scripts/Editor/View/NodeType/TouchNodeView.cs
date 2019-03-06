using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    class TouchNodeView : NodeView
    {
        public Vector2 ScreenPoint { get; private set; }

        public TouchNodeView (Vector2 position, Vector2 size) : base (NodeType.Touch, position, size)
        {

        }

        public TouchNodeView (Vector2 position, Vector2 size, Vector2 touchPosition) : base (NodeType.Touch, position, size)
        {
            ScreenPoint = touchPosition;
        }

        protected override void DrawNodeWindow (int id)
        {
            try
            {
                ScreenPoint = EditorGUILayout.Vector2Field ("Touch Point", ScreenPoint);
            }
            catch
            {
                // TODO: そのうちなおす
                // ArgumentException: Getting control 0's position in a group with only 0 controls when doing repaint Aborting
            }
        }
    }
}