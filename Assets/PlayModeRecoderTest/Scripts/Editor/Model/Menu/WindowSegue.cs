﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeRecorderTest
{
    struct WindowSegue
    {
        public static string[] Items => new string[]
        {
            SegueProcess.Make,
            SegueProcess.Delete
        };
    }
}