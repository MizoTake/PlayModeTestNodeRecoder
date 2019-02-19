using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeRecoderTest
{
    struct WindowSegue
    {
        public static SegueProcess[] Items => new SegueProcess[]
        {
            SegueProcess.Make,
            SegueProcess.Delete
        };
    }
}