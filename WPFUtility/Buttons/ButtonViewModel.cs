using System.Windows.Input;

namespace WPFUtility {
    public class ButtonViewModel : ViewModelBase, IButtonViewModel {
        public ButtonViewModel(string text, ICommand command, bool isCancel = false, bool isDefault = false) {
            Text = text;
            Command = command;
            IsCancel = IsCancel;
            IsDefault = IsDefault;
        }

        public ICommand Command { get; }
        public bool IsCancel { get; }
        public bool IsDefault { get; }
        public string Text { get; }
    }
}
