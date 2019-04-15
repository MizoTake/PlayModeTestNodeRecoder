using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    struct NodeSegue
    {
        public static string[] Items => new string[]
        {
            SegueProcess.Transition,
            SegueProcess.Delete
        };
    }
}