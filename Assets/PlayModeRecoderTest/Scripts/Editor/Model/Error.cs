using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeRecorderTest
{
    struct Error
    {
        public static Exception ProductError ()
        {
            return new Exception (Application.productName + " Error");
        }
    }
}