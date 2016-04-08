using System.Windows;

namespace WPFUtility {
    public class VMHostWindowViewModel : ViewModelBase {
        public VMHostWindowViewModel(ViewModelBase content, string backgroundColor) {
            Content = content;
            BackgroundColor = backgroundColor;
        }
        
        public ViewModelBase Content { get; }
        public string BackgroundColor { get; }
    }
}
