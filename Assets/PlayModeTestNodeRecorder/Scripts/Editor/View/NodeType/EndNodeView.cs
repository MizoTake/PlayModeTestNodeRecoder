using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    class EndNodeView : NodeView
    {
        public EndNodeView (Vector2 position, Vector2 size) : base (NodeType.End, position, size)
        {

        }

        protected override void DrawNodeWindow (int id)
        {

        }
    }
}