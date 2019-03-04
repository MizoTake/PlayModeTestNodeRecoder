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
        public void CreateScript (string className)
        {
            var builder = new StringBuilder ();

            builder = WriteClassElement (builder, className);

            var text = builder.ToString ().Replace (",}", "}");
            var assetPath = Application.dataPath + className + ".cs";

            Directory.CreateDirectory (Application.dataPath);

            if (AssetDatabase.LoadAssetAtPath (assetPath.Replace ("/Editor/..", ""), typeof (UnityEngine.Object)) != null && EditorPrefs.GetInt (this.GetType ().Name, 0) == text.GetHashCode ())
                return;

            System.IO.File.WriteAllText (assetPath, text);
            EditorPrefs.SetInt (this.GetType ().Name, text.GetHashCode ());
            AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);
        }

        private StringBuilder WriteClassElement (StringBuilder builder, string className)
        {

            builder.AppendLine ("public class " + className);
            builder.AppendLine ("{");

            builder.AppendLine ("}");
            return builder;
        }
    }
}