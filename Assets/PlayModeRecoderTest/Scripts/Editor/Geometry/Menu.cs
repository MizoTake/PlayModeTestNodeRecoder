using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayModeRecoderTest
{
    public class Menu : IDrawable, IChoosable
    {
        public void Draw ()
        {

        }

        public void Choice ()
        {

        }

        public GUIContent[] SelectableItem ()
        {
            return new GUIContent[0];
        }
    }
}