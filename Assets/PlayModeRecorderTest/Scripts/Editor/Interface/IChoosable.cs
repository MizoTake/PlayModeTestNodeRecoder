using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    public interface IChoosable
    {
        string[] ItemData { get; }
        void Choice (object select);
    }
}