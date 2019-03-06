using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    class BeginNodeView : NodeView
    {
        public BeginNodeView (Vector2 position, Vector2 size) : base (NodeType.Begin, position, size)
        {

        }

        protected override void DrawNodeWindow (int id)
        {

        }
    }
}