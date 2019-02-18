using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeRecoderTest
{
    public class TestNodeWindow : EditorWindow
    {

        [MenuItem ("Window/Test Node Editor")]
        static void Open ()
        {
            var nodeEditorWindow = CreateInstance<TestNodeWindow> ();
            nodeEditorWindow.Show ();
        }

        void OnGUI ()
        {

        }
    }
}