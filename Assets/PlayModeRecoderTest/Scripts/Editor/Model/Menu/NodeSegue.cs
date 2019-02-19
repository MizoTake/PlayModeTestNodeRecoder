using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeRecoderTest
{
    struct NodeSegue
    {
        public static SegueProcess[] Items => new SegueProcess[]
        {
            SegueProcess.Transition
        };
    }
}