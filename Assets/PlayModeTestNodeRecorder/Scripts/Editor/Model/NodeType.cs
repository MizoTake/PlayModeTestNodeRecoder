using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    public enum NodeType
    {
        Touch,
        Swipe,
        Drag,
        Delay,
        Begin
    }

    static class NodeTypeExtensions
    {
        private static float delayTime = 0;
        private static Vector2? touchPositionCache = null;

        public static NodeView ToNode (this NodeType type, Vector2 position)
        {
            NodeView node = null;
            switch (type)
            {
                case NodeType.Touch:
                    if (touchPositionCache == null)
                    {
                        node = new TouchNodeView (position, Vector2.one * 130);
                    }
                    else
                    {
                        node = new TouchNodeView (position, Vector2.one * 130, touchPositionCache.Value);
                        touchPositionCache = null;
                    }
                    break;
                case NodeType.Delay:
                    node = new DelayNodeView (position, Vector2.one * 180, delayTime);
                    break;
                case NodeType.Begin:
                    node = new BeginNodeView (position, Vector2.one * 100);
                    break;
            }
            return node;
        }

        public static void SetTouchPosition (this NodeType type, Vector2 position)
        {
            touchPositionCache = position;
        }

        public static void SetDelayTime (this NodeType type, float time)
        {
            delayTime = time;
        }
    }
}