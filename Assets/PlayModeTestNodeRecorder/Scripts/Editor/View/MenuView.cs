using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayModeTestNodeRecorder
{
    using ViewModel = MenuViewModel;
    sealed partial class MenuView : IViewable
    {
        private ViewModel viewModel;
        private GenericMenu drawingMenu = new GenericMenu ();
        public string ChoiceTitle
        {
            get
            {
                var selected = viewModel.SelectedMenuTitle;
                viewModel.SelectedMenuTitle = "";
                return selected;
            }
        }

        public MenuView (MenuType type)
        {
            this.viewModel = new ViewModel (type);
            CreateList (viewModel.ItemData);
        }

        private void CreateList (IEnumerable<string> data)
        {
            foreach (var item in data)
            {
                drawingMenu.AddItem (new GUIContent (item), false, viewModel.Choice, item);
                // TODO: 入れ子構造にすると再帰処理とかでの対応が必要そのうち
                // if (item != data.Last ()) drawingMenu.AddSeparator ("");
            }
        }
    }

    partial class MenuView : ISelected
    {
        public bool Selected
        {
            get
            {
                return viewModel.SelectedMenuTitle != "";
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