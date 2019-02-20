using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

[assembly : InternalsVisibleTo ("Tests")]
namespace PlayModeRecoderTest
{
    partial class MenuViewModel
    {
        private MenuType type;

        public MenuViewModel (MenuType type)
        {
            this.type = type;
        }
    }

    partial class MenuViewModel : IChoosable
    {
        public string[] ItemData
        {
            get
            {
                switch (type)
                {
                    case MenuType.Node:
                        return NodeSegue.Items;
                    case MenuType.Window:
                        return WindowSegue.Items;
                }
                Assert.IsTrue (false, "何でここ通っとんねん");
                return null;
            }
        }

        public void Choice (GUIContent select)
        {

        }
    }
}