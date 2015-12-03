using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFUtility {
    public class ToggleButtonViewModel : ButtonViewModel, ISelectableButtonViewModel {
        public ToggleButtonViewModel(string text, ICommand command) {
            Text.Value = text;
            Command = new RelayCommand(x => { IsSelected.Value = !IsSelected.Value; command.Execute(x); }, command.CanExecute);
        }

        public NotifyingProperty<bool> IsSelected { get; } = new NotifyingProperty<bool>();
    }
}
