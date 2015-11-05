using System;
using System.Windows;

namespace WPFUtility {
    public static class WindowGenerator {
        public static void CreateAndShow(Func<Action, ViewModelBase> contentGenerator, bool resizeable) {
            var window = new VMHostWindowView();
            var content = new VMHostWindowViewModel(contentGenerator(window.Close));
            SetupWindow(window, content, resizeable);
            window.Show();
        }

        public static bool? CreateAndShowModal(Func<Action, ViewModelBase> contentGenerator, bool resizeable) {
            var window = new VMHostWindowView();
            var content = new VMHostWindowViewModel(contentGenerator(window.Close));
            SetupWindow(window, content, resizeable);
            return window.ShowDialog();
        }

        private static void SetupWindow(VMHostWindowView window, VMHostWindowViewModel content, bool resizeable) {
            window.ShowInTaskbar = false;
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = content;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.ResizeMode = resizeable ? ResizeMode.CanResize : ResizeMode.NoResize;
        }
    }
}
