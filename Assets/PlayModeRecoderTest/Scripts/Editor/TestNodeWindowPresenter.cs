﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace PlayModeRecoderTest
{
    using Menu = MenuView;
    partial class TestNodeWindowPresenter
    {

        Menu nodeMenu = new Menu (MenuType.Node);
        Menu windowMenu = new Menu (MenuType.Window);

        private void Dispatch (Event current)
        {
            switch (current.type)
            {
                case EventType.MouseDown:
                    nodeMenu.Draw ();
                    break;
                default:
                    Assert.IsTrue (false, "何でここ通っとんねん");
                    break;
            }
            current.Use ();
        }
    }

    partial class TestNodeWindowPresenter : EditorWindow
    {
        [MenuItem ("Window/Test Node Editor")]
        static void Open ()
        {
            var nodeEditorWindow = CreateInstance<TestNodeWindowPresenter> ();
            nodeEditorWindow.Show ();
        }

        void OnGUI ()
        {
            var current = Event.current;
            if (current.type == EventType.MouseDown)
            {
                Dispatch (current);
            }
        }
    }
}