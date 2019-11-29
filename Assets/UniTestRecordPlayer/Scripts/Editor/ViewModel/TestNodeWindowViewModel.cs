using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[assembly : InternalsVisibleTo ("PlayModeTestNodeRecorder.EditMode")]
namespace PlayModeTestNodeRecorder
{
    using CreateScript = CreateScriptModel;
    sealed class TestNodeWindowViewModel
    {
        private Config config;
        private CreateScript craeteScript = new CreateScript ();

        public Config LoadConfig ()
        {
            var config = Resources.Load<Config> ("Config");
            if (config == null)
            {
                config = Config.Create ();
            }
            this.config = config;
            return config;
        }

        public void SavingScriptFile (string fieldText)
        {
            if (fieldText == "") return;

//            craeteScript.SavingFile (config.SavingPath, fieldText, nodeList.ToArray ());
        }
    }
}