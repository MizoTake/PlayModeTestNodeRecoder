using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace PlayModeRecoderTest
{
    using Model = TestNodeWindowModel;
    using Menu = MenuView;
    partial class TestNodeWindowPresenter
    {
        private Model model = new Model ();
        private IViewable nodeMenu = new Menu (MenuType.Node);
        private IViewable windowMenu = new Menu (MenuType.Window);

        private void Dispatch (Event current)
        {
            switch (current.type)
            {
                case EventType.MouseDown:
                    nodeMenu.Draw ();
                    break;
                default:
                    throw new Exception (Application.productName + " Error");
            }
            current.Use ();

            switch (nodeMenu.Selected)
            {

            }
            model.CreateNode (current.mousePosition);
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
            BeginWindows ();
            foreach (var node in model.NodeViews)
            {
                node.Draw ();
            }
            EndWindows ();
        }
    }
}