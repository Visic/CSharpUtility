using System.Windows.Input;

namespace WPFUtility {
    public class SelectableButtonViewModel : ButtonViewModel, ISelectableButtonViewModel {
        public SelectableButtonViewModel(string text, ICommand command) {
            Text.Value = text;

            bool _ignoreSelectedChanged = false;
            IsSelected = new NotifyingProperty<bool>(isSelected => {
                if (_ignoreSelectedChanged || !isSelected) return;
                Command.Execute(null);
            });

            Command = new RelayCommand(
                x => {
                    _ignoreSelectedChanged = true;
                    IsSelected.Value = true;
                    command.Execute(x);
                    _ignoreSelectedChanged = false;
                }, 
                command.CanExecute
            );
        }

        public NotifyingProperty<bool> IsSelected { get; }
    }
}
