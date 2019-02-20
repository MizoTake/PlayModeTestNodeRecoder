using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeRecoderTest
{
    public interface IChoosable
    {
        string[] ItemData { get; }
        void Choice (object select);
    }
}