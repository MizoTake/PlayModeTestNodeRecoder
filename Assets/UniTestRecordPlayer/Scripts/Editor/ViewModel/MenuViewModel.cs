using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly : InternalsVisibleTo ("Tests")]
namespace PlayModeTestNodeRecorder
{
    sealed partial class MenuViewModel
    {
        private MenuType type;
        public string SelectedMenuTitle { get; set; }

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
                throw Error.ProductError ();
            }
        }

        public void Choice (object select)
        {
            var selected = select.ToString ();
            SelectedMenuTitle = selected;
        }
    }
}