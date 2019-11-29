using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    class DelayNodeView : NodeView
    {
        public float DelayTime { get; private set; }

        public DelayNodeView (Vector2 position, Vector2 size, float delayTime) : base (NodeType.Delay, position, size)
        {
            DelayTime = delayTime;
        }

        protected override void DrawNodeWindow (int id)
        {
            try
            {
                DelayTime = EditorGUILayout.FloatField ("Delay Time", DelayTime);
            }
            catch
            {
                // TODO: そのうちなおす
                // ArgumentException: Getting control 0's position in a group with only 0 controls when doing repaint Aborting
            }
        }
    }
}