using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    class ScriptCreateModel
    {
        public void CreateScript (string className)
        {
            var builder = new StringBuilder ();

            builder = WriteClassElement (builder);

            var text = builder.ToString ().Replace (",}", "}");
            var assetPath = Application.dataPath + className + ".cs";

            Directory.CreateDirectory (Application.dataPath);

            if (AssetDatabase.LoadAssetAtPath (assetPath.Replace ("/Editor/..", ""), typeof (UnityEngine.Object)) != null && EditorPrefs.GetInt (this.GetType ().Name, 0) == text.GetHashCode ())
                return;

            System.IO.File.WriteAllText (assetPath, text);
            EditorPrefs.SetInt (this.GetType ().Name, text.GetHashCode ());
            AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);
        }

        private StringBuilder WriteClassElement (StringBuilder builder)
        {

            builder.AppendLine ("public class TagName");
            builder.AppendLine ("{");

            builder.AppendLine ("}");
            return builder;
        }
    }
}