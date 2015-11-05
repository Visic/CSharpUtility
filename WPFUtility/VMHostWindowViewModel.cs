using System;

namespace WPFUtility {
    public class VMHostWindowViewModel : ViewModelBase {
        public VMHostWindowViewModel(ViewModelBase content) {
            Content = content;
        }

        public ViewModelBase Content { get; private set; }
    }
}
