using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PlayModeTestNodeRecorder
{
    class Config : ScriptableObject
    {
        public string SavingPath;

#if UNITY_EDITOR
        public static Config Create ()
        {
            var asset = CreateInstance<Config> ();
            AssetDatabase.CreateAsset (asset, "Assets/PlayModeTestNodeRecorder/Resources/Config.asset");
            AssetDatabase.Refresh ();
            return asset;
        }
#endif
    }
}