using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeRecoderTest
{
    public class Line : IDrawable
    {

        private Node start;
        private Node end;
        private float width;

        public Line (Node start, Node end, float width = 3f)
        {
            this.start = start;
            this.end = end;
            this.width = width;
        }

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