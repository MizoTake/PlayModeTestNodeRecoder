using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    class TouchNodeView : NodeView
    {
        public TouchNodeView (Vector2 position, Vector2 size) : base (NodeType.Touch, position, size)
        {

        }
    }
}