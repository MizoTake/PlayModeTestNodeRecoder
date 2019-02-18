using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeRecoderTest
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3 (this Vector2 vec)
        {
            return new Vector3 (vec.x, vec.y, 0);
        }
    }
}