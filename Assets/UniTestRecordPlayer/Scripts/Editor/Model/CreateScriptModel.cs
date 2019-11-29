using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    class CreateScriptModel
    {
        public void SavingFile (string path, string className)
        {
            var builder = new StringBuilder ();

            builder = WriteClassElement (builder, className);

            var text = builder.ToString ();
            var assetPath = Application.dataPath + "/" + path + "/" + className + ".cs";

            Directory.CreateDirectory (Application.dataPath);

            if (AssetDatabase.LoadAssetAtPath (assetPath.Replace ("/Editor/..", ""), typeof (Object)) != null && EditorPrefs.GetInt (GetType ().Name, 0) == text.GetHashCode ())
                return;

            File.Copy (Application.dataPath + "/PlayModeTestNodeRecorder/StreamingAssets/asmdefTemplete.txt", Application.dataPath + "/" + path + "/Tests.asmdef", true);
            File.WriteAllText (assetPath, text);
            EditorPrefs.SetInt (GetType ().Name, text.GetHashCode ());
            AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);
        }

        // TODO: Delay処理を考えると純粋メソッドではなくコルーチンでメソッドをコールした方が良さそう
        private StringBuilder WriteClassElement (StringBuilder builder, string className)
        {
            // TODO: コメントアウトでNodeのjsonを埋め込んでおく
            builder.AppendLine ("/*");
//            builder.AppendLine (JsonUtility.ToJson(nodeArray));
            builder.AppendLine ("*/");
            builder.AppendLine ("\t");
            builder.AppendLine ("using PlayModeTestNodeRecorder;");
            builder.AppendLine ("using UnityEngine;");
            builder.AppendLine ("using NUnit.Framework;");
            builder.AppendLine ("\t");
            builder.AppendLine ("public class " + className + " : NodeTestScript");
            builder.AppendLine ("{");
            {
                builder.Append ("\t").AppendLine ("[Test]");
                builder.Append ("\t").AppendLine ("public void TestMain()");
                builder.Append ("\t").AppendLine ("{");
                {
//                    foreach (var node in nodeArray)
//                    {
//                        builder.Append ("\t").Append ("\t").AppendLine (NodeTypeToMethodString ());
//                    }
                }
                builder.Append ("\t").AppendLine ("}");
            }
            builder.AppendLine ("}");
            return builder;
        }

        private string NodeTypeToMethodString ()
        {
//            if (node is TouchNodeView touch)
//            {
//                return node.Type + "(new Vector2(" + touch.ScreenPoint.x + "f ," + touch.ScreenPoint.y + "f));";
//            }
            return "";
        }
    }
}