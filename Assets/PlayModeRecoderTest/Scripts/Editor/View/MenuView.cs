using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PlayModeRecorderTest
{
    using ViewModel = MenuViewModel;
    partial class MenuView : IViewable
    {
        private MenuViewModel viewModel;
        private GenericMenu drawingMenu = new GenericMenu ();

        public MenuView (MenuType type)
        {
            this.viewModel = new ViewModel (type);
            CreateList (viewModel.ItemData);
        }

        private void CreateList (string[] data)
        {
            foreach (var item in data)
            {
                drawingMenu.AddItem (new GUIContent (item), false, viewModel.Choice, item);
                if (item != data.Last ()) drawingMenu.AddSeparator ("");
            }
        }
    }

    partial class MenuView : ISelected
    {
        public string Selected
        {
            get
            {
                var selected = viewModel.Selected;
                viewModel.Selected = "";
                return selected;
            }
        }
    }

    partial class MenuView : IDrawable
    {
        public void Draw ()
        {
            drawingMenu.ShowAsContext ();
        }
    }
}