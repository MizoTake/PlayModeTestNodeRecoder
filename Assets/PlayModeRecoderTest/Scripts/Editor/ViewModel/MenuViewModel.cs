using System;
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

    partial class MenuViewModel : IViewModel
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
                throw new Exception (Application.productName + " Error");
            }
        }

        public void Choice (object select)
        {
            switch (select.ToString ())
            {
                case SegueProcess.Transition:
                    break;
                case SegueProcess.Make:
                    break;
                case SegueProcess.Delete:
                    break;
                default:
                    throw new Exception (Application.productName + " Error");
            }
        }
    }
}