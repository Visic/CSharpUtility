using System.Collections.Generic;
using System.Windows.Input;

namespace WPFUtility {
    public class SelectableCloseableButtonWithContextMenuViewModel : SelectableButtonViewModel {
        public SelectableCloseableButtonWithContextMenuViewModel(string text, ICommand command, ICommand closeCommand, params CommandWithText[] contextMenuOptions) : base(text, command) {
            CloseCommand = closeCommand;
            ContextMenuOptions = contextMenuOptions;
        }

        public ICommand CloseCommand { get; }
        public IReadOnlyList<CommandWithText> ContextMenuOptions { get; }
    }
}
