using System.Windows.Input;

namespace WPFUtility {
    public class SelectableButtonViewModel : ButtonViewModel, ISelectableViewModel {
        public SelectableButtonViewModel(string text, ICommand command) {
            Text = text;

            bool _ignoreSelectedChanged = false;
            IsSelected = new NotifyingPropertyWithChangedAction<bool>(isSelected => {
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
