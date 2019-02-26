using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    struct Error
    {
        public static Exception ProductError ()
        {
            return new Exception (Application.productName + " Error");
        }
    }
}