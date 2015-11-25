using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFUtility {
    public class SelectableButtonViewModel : ButtonViewModel, ISelectableViewModel {
        public SelectableButtonViewModel(string text, ICommand command) {
            Text = text;
            Command = new RelayCommand(x => { IsSelected.Value = true; command.Execute(x); }, command.CanExecute);
        }

        public NotifyingProperty<bool> IsSelected { get; } = new NotifyingProperty<bool>();
    }
}
