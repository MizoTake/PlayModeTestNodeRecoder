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
        public void SavingFile (string path, string className, NodeView[] nodeArray)
        {
            var builder = new StringBuilder ();

            builder = WriteClassElement (builder, className, nodeArray);

            var text = builder.ToString ();
            var assetPath = Application.dataPath + "/" + path + "/" + className + ".cs";

            Directory.CreateDirectory (Application.dataPath);

            if (AssetDatabase.LoadAssetAtPath (assetPath.Replace ("/Editor/..", ""), typeof (UnityEngine.Object)) != null && EditorPrefs.GetInt (this.GetType ().Name, 0) == text.GetHashCode ())
                return;

            File.Copy (Application.dataPath + "/PlayModeTestNodeRecorder/StreamingAssets/asmdefTemplete.txt", Application.dataPath + "/" + path + "/Tests.asmdef", true);
            File.WriteAllText (assetPath, text);
            EditorPrefs.SetInt (this.GetType ().Name, text.GetHashCode ());
            AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);
        }

        private StringBuilder WriteClassElement (StringBuilder builder, string className, NodeView[] nodeArray)
        {
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
                    foreach (var node in nodeArray)
                    {
                        builder.Append ("\t").Append ("\t").AppendLine (NodeTypeToMethodString (node));
                    }
                }
                builder.Append ("\t").AppendLine ("}");
            }
            builder.AppendLine ("}");
            return builder;
        }

        private string NodeTypeToMethodString (NodeView node)
        {
            if (node is TouchNodeView touch)
            {
                return node.Type.ToString () + "(new Vector2(" + touch.ScreenPoint.x + "," + touch.ScreenPoint.y + "));";
            }
            return "";
        }
    }
}