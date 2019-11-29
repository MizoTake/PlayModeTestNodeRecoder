using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    struct WindowSegue
    {
        public static string[] Items
        {
            get
            {
                var result = new List<string> ();
                foreach (var type in Enum.GetValues (typeof (NodeType)))
                {
                    if (type.Equals (NodeType.Begin)) continue;
                    result.Add (SegueProcess.Make + "/" + type);
                }
                return result.ToArray ();
            }
        }
    }
}