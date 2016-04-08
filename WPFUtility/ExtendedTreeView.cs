using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFUtility {
    //http://stackoverflow.com/questions/1000040/data-binding-to-selecteditem-in-a-wpf-treeview
    public class ExtendedTreeView : TreeView {
        public static readonly DependencyProperty SelectedItem_Property = DependencyProperty.Register(
            "SelectedItem_",
            typeof(object),
            typeof(ExtendedTreeView),
            new UIPropertyMetadata(null)
        );

        public object SelectedItem_ {
            get { return GetValue(SelectedItem_Property); }
            set { SetValue(SelectedItem_Property, value); }
        }

        public ExtendedTreeView() : base() {
            SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(___ICH);
        }

        private void ___ICH(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if(SelectedItem != null) {
                if (SelectedItem is ISelectableViewModel) ((ISelectableViewModel)SelectedItem).IsSelected.Value = true;
                SetValue(SelectedItem_Property, SelectedItem);
            }
        }
    }
}
