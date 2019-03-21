using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Assert = UnityEngine.Assertions.Assert;

namespace PlayModeTestNodeRecorder
{
    using ViewModel = TestNodeWindowViewModel;
    using Menu = MenuView;
    using Node = NodeView;
    sealed partial class TestNodeWindowView
    {
        private Vector2 scrollPos = Vector2.zero;
        private string fieldString = "";
        private ViewModel viewModel = new ViewModel ();
        private Menu nodeMenu = new Menu (MenuType.Node);
        private Menu windowMenu = new Menu (MenuType.Window);
        private Node selectedNode = null;
        private Config config;

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
                    // TODO: Nodeをベースに削除処理を書く、紐づくLineも消す
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
            Repaint ();

            if (Application.isPlaying && Input.GetMouseButtonDown (0))
            {
                if (Mathf.Sign (Input.mousePosition.x) == 1 && Mathf.Sign (Input.mousePosition.y) == 1)
                {
                    // TODO: Type別に生成する処理
                    var lastNode = viewModel.NodeViews.Last ();
                    var lastNodeRect = lastNode.ViewableRect;
                    var viewablePoint = lastNodeRect.position + lastNodeRect.size * 2f;
                    // TODO: 依存なので除去りたい
                    var type = NodeType.Touch;
                    type.SetTouchPosition (Input.mousePosition);
                    viewModel.CreateNode (type, viewablePoint);
                    viewModel.CreateLine (lastNode, viewablePoint);
                    viewModel.ConnectNode (viewablePoint);
                }
            }
        }
    }
}