using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    sealed partial class LineView : IViewable
    {

        private Vector2 start;
        private Vector2 end;
        private float width;

        public LineView (Vector2 start, Vector2 end, float width = 3f)
        {
            this.start = start;
            this.end = end;
            this.width = width;
        }

        public void UpdateStartPoint (Vector2 start)
        {
            this.start = start;
        }

        public void UpdateEndPoint (Vector2 end)
        {
            this.end = end;
        }
    }

    partial class LineView : ISelected
    {
        public string Selected =>
            throw new System.NotImplementedException ();
    }

    partial class LineView : IDrawable
    {
        public void Draw ()
        {
            var startPos = start;
            var endPos = end;
            var startTan = startPos.ToVector3 () + new Vector3 (100f, 0f, 0f);
            var endTan = endPos.ToVector3 () + new Vector3 (-100f, 0f, 0f);
            Handles.DrawBezier (startPos, endPos, startTan, endTan, Color.gray, null, width);
        }
    }
}