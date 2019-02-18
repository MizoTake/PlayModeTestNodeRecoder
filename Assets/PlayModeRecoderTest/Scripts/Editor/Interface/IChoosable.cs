using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeRecoderTest
{
    public interface IChoosable
    {
        void Choice ();
        GUIContent[] SelectableItem ();
    }
}