using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace PlayModeRecorderTest
{
    using ViewModel = TestNodeWindowViewModel;
    using Menu = MenuView;
    using Node = NodeView;
    sealed partial class TestNodeWindowView
    {
        private ViewModel viewModel = new ViewModel ();
        private IViewable nodeMenu = new Menu (MenuType.Node);
        private IViewable windowMenu = new Menu (MenuType.Window);
        private Node selectedNode = null;

        private void Dispatch (Event current)
        {
            selectedNode = viewModel.ClickOnNode (current.mousePosition);
            var onNode = selectedNode != null;
            switch (current.button)
            {
                case 0: // 左クリック
                    viewModel.ConnectNode (current.mousePosition);
                    break;
                case 1: // 右クリック
                    if (onNode)
                    {
                        nodeMenu.Draw ();
                    }
                    else
                    {
                        windowMenu.Draw ();
                    }
                    break;
            }
            current.Use ();
        }

        private void SelectedAction (Event current)
        {
            var onNode = selectedNode != null;
            var selected = onNode ? nodeMenu.Selected : windowMenu.Selected;
            switch (selected)
            {
                case SegueProcess.Transition:
                    viewModel.CreateLine (selectedNode, current.mousePosition, selectedNode);
                    break;
                case SegueProcess.Make:
                    viewModel.CreateNode (current.mousePosition);
                    break;
                case SegueProcess.Delete:
                    break;
            }
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
            SelectedAction (current);
            if (current.type == EventType.MouseDown)
            {
                Dispatch (current);
            }
            if (viewModel.LastCreatedLine != null)
            {
                viewModel.LastCreatedLine.UpdateEndPoint (current.mousePosition);
            }
            foreach (var line in viewModel.LineViews)
            {
                line.Draw ();
            }
            BeginWindows ();
            foreach (var node in viewModel.NodeViews)
            {
                node.Draw ();
            }
            EndWindows ();
        }

        void Update ()
        {
            Repaint ();
        }
    }
}