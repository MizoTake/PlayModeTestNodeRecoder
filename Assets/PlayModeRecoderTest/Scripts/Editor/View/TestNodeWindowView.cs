using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace PlayModeRecoderTest
{
    using ViewModel = TestNodeWindowViewModel;
    using Menu = MenuView;
    partial class TestNodeWindowView
    {
        private ViewModel viewModel = new ViewModel ();
        private IViewable nodeMenu = new Menu (MenuType.Node);
        private IViewable windowMenu = new Menu (MenuType.Window);

        private void Dispatch (Event current)
        {
            var onNode = viewModel.ClickOnNode (current.mousePosition);
            switch (current.type)
            {
                case EventType.MouseDown:
                    if (onNode)
                    {
                        nodeMenu.Draw ();
                    }
                    else
                    {
                        windowMenu.Draw ();
                    }
                    break;
                default:
                    throw new Exception (Application.productName + " Error");
            }

            var selected = (onNode) ? nodeMenu.Selected : windowMenu.Selected;
            switch (selected)
            {
                case SegueProcess.Transition:
                    break;
                case SegueProcess.Make:
                    viewModel.CreateNode (current.mousePosition);
                    break;
                case SegueProcess.Delete:
                    break;
            }
            current.Use ();
        }
    }

    partial class TestNodeWindowView : EditorWindow
    {
        [MenuItem ("Window/Test Node Editor")]
        static void Open ()
        {
            var nodeEditorWindow = CreateInstance<TestNodeWindowView> ();
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
            foreach (var node in viewModel.NodeViews)
            {
                node.Draw ();
            }
            EndWindows ();
        }
    }
}