using System;
using System.Windows;

namespace WPFUtility {
    public static class WindowGenerator {
        public static void CreateAndShow(Func<Action, ViewModelBase> contentGenerator, bool resizeable, double height, double width, string backgroundColor) {
            var window = new VMHostWindowView();
            var content = new VMHostWindowViewModel(contentGenerator(window.Close), backgroundColor);
            SetupWindow(window, content, resizeable, height, width);
            window.Show();
        }

        public static bool? CreateAndShowModal(Func<Action, ViewModelBase> contentGenerator, bool resizeable, double height, double width, string backgroundColor) {
            var window = new VMHostWindowView();
            var content = new VMHostWindowViewModel(contentGenerator(window.Close), backgroundColor);
            SetupWindow(window, content, resizeable, height, width);
            return window.ShowDialog();
        }

        public static void Popup(string message, double height, double width, string backgroundColor) {
            CreateAndShowModal(x => new MessageViewModel(message), false, height, width, backgroundColor);
        }

        private static void SetupWindow(VMHostWindowView window, VMHostWindowViewModel content, bool resizeable, double height, double width) {
            window.Owner = Application.Current.MainWindow;
            window.Height = height;
            window.Width = width;
            window.DataContext = content;
            window.ResizeMode = resizeable ? ResizeMode.CanResize : ResizeMode.NoResize;
        }
    }
}
