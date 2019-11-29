﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    using ViewModel = TestNodeWindowViewModel;
    using Menu = MenuView;
    using Node = NodeView;
    sealed partial class TestNodeWindowView
    {
        private Vector2 scrollPos = Vector2.zero;
        private string fieldString = "";
        private readonly ViewModel viewModel = new ViewModel ();
        private readonly Menu nodeMenu = new Menu (MenuType.Node);
        private readonly Menu windowMenu = new Menu (MenuType.Window);
        private Node selectedNode;
        private Config config;
        private float delayTime;

        public void Init ()
        {
            config = viewModel.LoadConfig ();
            viewModel.CreateNode (NodeType.Begin, new Vector2 (position.width / 2f, position.height / 2f));
        }

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
                        // TODO: 矩形一括選択処理
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
            var splitString = Regex.Split (selected ?? "", "/");
            switch (splitString[0])
            {
                case SegueProcess.Transition:
                    viewModel.CreateLine (selectedNode, current.mousePosition);
                    break;
                case SegueProcess.Make:
                    var type = (NodeType) Enum.Parse (typeof (NodeType), splitString[1]);
                    viewModel.CreateNode (type, current.mousePosition);
                    break;
                case SegueProcess.Delete:
                    viewModel.RemoveNode (selectedNode);
                    break;
            }
        }

        private void EventUIDisplay ()
        {
            var option = new GUILayoutOption[] { GUILayout.MaxWidth (position.width / 2f) };
            using (new EditorGUILayout.HorizontalScope ())
            {
                fieldString = GUILayout.TextField (fieldString, option);
                if (GUILayout.Button ("Save"))
                {
                    viewModel.SavingScriptFile (fieldString);
                }
                if (GUILayout.Button ("Load"))
                {
                    // TODO: Scriptを読み込んでNodeを出す処理
                }
            }
        }

        private void CreateNode (NodeType type, Node lastNode)
        {
            var lastNodeRect = lastNode.ViewableRect;
            var viewablePoint = lastNodeRect.position + lastNodeRect.size * 1.2f;
            viewModel.CreateNode (type, viewablePoint);
            viewModel.CreateLine (lastNode, viewablePoint);
            viewModel.ConnectNode (viewablePoint);
        }

        private void CrateDelayNode ()
        {
            var delay = NodeType.Delay;
            delay.SetDelayTime (delayTime);
            CreateNode (delay, viewModel.NodeViews.Last ());
            delayTime = 0;
        }
    }

    partial class TestNodeWindowView : EditorWindow
    {
        [MenuItem ("Window/Test Node Editor")]
        static void Open ()
        {
            var nodeEditorWindow = GetWindow<TestNodeWindowView> (typeof (SceneView));
            nodeEditorWindow.Init ();
        }

        void OnGUI ()
        {
            // TODO: Scroll範囲をNodeがある範囲にする
            scrollPos = GUI.BeginScrollView (new Rect (0, 0, position.width, position.height), scrollPos, new Rect (0, 0, 1000, 1000));
            EventUIDisplay ();
            var current = Event.current;

            viewModel.LastCreatedLine?.UpdateEndPoint (current.mousePosition);
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
            GUI.EndScrollView ();

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
        }

        void Update ()
        {
            delayTime += Time.deltaTime;
            Repaint ();

            if (!Application.isPlaying) return;
            if (Mathf.Sign(Input.mousePosition.x) != 1 || Mathf.Sign(Input.mousePosition.y) != 1) return;
            // TODO: Type別に生成する処理
            NodeType? type = null;
            if (Input.GetMouseButtonDown (0))
            {
                type = NodeType.Touch;
            }
            if (!type.HasValue) return;
            CrateDelayNode ();
            var lastNode = viewModel.NodeViews.Last ();
            type.Value.SetTouchPosition (Input.mousePosition);
            CreateNode (type.Value, lastNode);
        }
    }
}