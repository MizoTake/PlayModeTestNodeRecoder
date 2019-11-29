using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    using ViewModel = TestNodeWindowViewModel;
    sealed partial class TestNodeWindowView
    {
        private string fieldString = "";
        private readonly ViewModel viewModel = new ViewModel ();
        private Config config;
        private float delayTime;

        public void Init ()
        {
            config = viewModel.LoadConfig ();
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
        
        private void CrateDelayNode ()
        {
//            var delay = NodeType.Delay;
//            delay.SetDelayTime (delayTime);
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
            EventUIDisplay ();
            
            EndWindows ();
            GUI.EndScrollView ();
        }

        void Update ()
        {
            delayTime += Time.deltaTime;
            Repaint ();

            if (!Application.isPlaying) return;
            if (Mathf.Sign(Input.mousePosition.x) != 1 || Mathf.Sign(Input.mousePosition.y) != 1) return;
            // TODO: Type別に生成する処理
            CrateDelayNode ();
        }
    }
}