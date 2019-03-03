using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace PlayModeTestNodeRecorder
{
    using ViewModel = TestNodeWindowViewModel;
    using Menu = MenuView;
    using Node = NodeView;
    sealed partial class TestNodeWindowView
    {
        private ViewModel viewModel = new ViewModel ();
        private Menu nodeMenu = new Menu (MenuType.Node);
        private Menu windowMenu = new Menu (MenuType.Window);
        private Node selectedNode = null;

        private void DownDispatch (Event current)
        {
            selectedNode = viewModel.ClickOnNode (current.mousePosition);
            var onNode = selectedNode != null;
            switch (current.button)
            {
                case 0: // 左クリック
                    if (viewModel.LastCreatedLine != null)
                    {
                        // Lineを出してつなぐ状態
                        viewModel.ConnectNode (current.mousePosition);
                    }
                    else
                    {
                        // TODO: Nodeを選択する状態(Nodeにout colorを追加する処理)
                    }
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

        private void DragDispatch (Event current)
        {
            var onNode = selectedNode != null;
            switch (current.button)
            {
                case 0: // 左クリック
                    if (onNode)
                    {
                        // TODO: 複数選択時には複数移動が必要
                        selectedNode.Move (current.mousePosition);
                    }
                    else
                    {
                        // TODO: 矩形選択処理
                    }
                    break;
                case 1: // 右クリック
                    break;
            }
            current.Use ();
        }

        private void SelectedAction (Event current)
        {
            var onNode = selectedNode != null;
            var selected = onNode ? nodeMenu.ChoiceTitle : windowMenu.ChoiceTitle;
            switch (selected)
            {
                case SegueProcess.Transition:
                    viewModel.CreateLine (selectedNode, current.mousePosition);
                    break;
                case SegueProcess.Make:
                    viewModel.CreateNode (NodeType.Touch, current.mousePosition);
                    break;
                case SegueProcess.Delete:
                    // TODO: Nodeをベースに削除処理を書く、紐づくLineも消す
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
            switch (current.type)
            {
                case EventType.MouseDown:
                    DownDispatch (current);
                    break;
                case EventType.MouseDrag:
                    DragDispatch (current);
                    break;
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