using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    sealed partial class LineView : IViewable
    {

        private Vector2 begin;
        private Vector2 end;
        private float width;

        public LineView (Vector2 begin, Vector2 end, float width = 3f)
        {
            this.begin = begin;
            this.end = end;
            this.width = width;
        }

        public void UpdateBeginPoint (Vector2 begin)
        {
            this.begin = begin;
        }

        public void UpdateEndPoint (Vector2 end)
        {
            this.end = end;
        }
    }

    partial class LineView : ISelected
    {
        public bool Selected =>
            throw new System.NotImplementedException ();
    }

    partial class LineView : IDrawable
    {
        public void Draw ()
        {
            var beginPos = begin;
            var endPos = end;
            var beginTan = beginPos.ToVector3 () + new Vector3 (100f, 0f, 0f);
            var endTan = endPos.ToVector3 () + new Vector3 (-100f, 0f, 0f);
            Handles.DrawBezier (beginPos, endPos, beginTan, endTan, Color.gray, null, width);
        }
    }
}