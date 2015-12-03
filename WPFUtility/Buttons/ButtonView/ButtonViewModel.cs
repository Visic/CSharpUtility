using System.Windows.Input;

namespace WPFUtility {
    public class ButtonViewModel : ViewModelBase, IButtonViewModel {
        protected ButtonViewModel() { }

        public ButtonViewModel(string text, ICommand command, bool isCancel = false, bool isDefault = false) {
            Text.Value = text;
            Command = command;
            IsCancel = IsCancel;
            IsDefault = IsDefault;
        }

        public ICommand Command { get; protected set; }
        public bool IsCancel { get; protected set; }
        public bool IsDefault { get; protected set; }
        public NotifyingProperty<string> Text { get; } = new NotifyingProperty<string>();
    }
}
