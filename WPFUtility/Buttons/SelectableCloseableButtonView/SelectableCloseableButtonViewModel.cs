using System.Windows.Input;

namespace WPFUtility {
    public class SelectableCloseableButtonViewModel : SelectableButtonViewModel {
        public SelectableCloseableButtonViewModel(string text, ICommand command, ICommand closeCommand) : base(text, command) {
            CloseCommand = closeCommand;
        }

        public ICommand CloseCommand { get; }
    }
}
