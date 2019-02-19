using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeRecoderTest
{
    partial class LineView
    {

        private NodeView start;
        private NodeView end;
        private float width;

        public LineView (NodeView start, NodeView end, float width = 3f)
        {
            this.start = start;
            this.end = end;
            this.width = width;
        }
    }

    partial class LineView : IDrawable
    {
        public void Draw ()
        {
            var startPos = start.CenterHeightPositon;
            var endPos = end.CenterHeightPositon;
            var startTan = startPos.ToVector3 () + new Vector3 (100f, 0f, 0f);
            var endTan = endPos.ToVector3 () + new Vector3 (-100f, 0f, 0f);
            Handles.DrawBezier (startPos, endPos, startTan, endTan, Color.gray, null, width);
        }
    }
}