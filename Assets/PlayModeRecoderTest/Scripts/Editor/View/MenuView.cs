using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeRecoderTest
{
    using ViewModel = MenuViewModel;
    partial class MenuView
    {
        private ViewModel viewModel;
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
                drawingMenu.AddItem (new GUIContent (item), false, null, item);
                drawingMenu.AddSeparator ("");
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