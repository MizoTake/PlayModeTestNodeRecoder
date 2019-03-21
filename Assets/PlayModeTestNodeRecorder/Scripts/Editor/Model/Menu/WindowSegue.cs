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
                    result.Add (SegueProcess.Make + "/" + type);
                }
                result.Add (SegueProcess.Delete);
                return result.ToArray ();
            }
        }
    }
}