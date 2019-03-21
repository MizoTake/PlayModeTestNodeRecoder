using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayModeTestNodeRecorder
{
    public abstract class NodeTestScript : IMonoBehaviourTest
    {
        public bool IsTestFinished { get; private set; }

        protected void Touch (Vector2 position)
        {

        }
    }
}