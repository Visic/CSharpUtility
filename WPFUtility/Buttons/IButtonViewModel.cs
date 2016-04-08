using System.Windows.Input;

namespace WPFUtility {
    public interface IButtonViewModel : IViewModelBase {
        NotifyingProperty<string> Text { get; }
        ICommand Command { get; }
        bool IsDefault { get; }
        bool IsCancel { get; }
    }
}
