using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeRecoderTest
{
    public class TestNodeWindowPresenter : EditorWindow
    {
        [MenuItem ("Window/Test Node Editor")]
        static void Open ()
        {
            var nodeEditorWindow = CreateInstance<TestNodeWindowPresenter> ();
            nodeEditorWindow.Show ();
        }

        void OnGUI ()
        {
            if (Event.current.button != -1)
            {
                Dispatch (Event.current);
            }
        }

        void Dispatch (Event current)
        {
            switch (current.type)
            {
                case EventType.MouseDown:
                    break;
                default:
                    break;
            }
            current.Use ();
        }
    }
}