using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtility {
    public class HostWithDialogButtonsViewModel : ViewModelBase {
        public HostWithDialogButtonsViewModel(ViewModelBase content, params IButtonViewModel[] buttons) {
            Content = content;
            Buttons = buttons;
        }

        public ViewModelBase Content { get; }
        public IEnumerable<IButtonViewModel> Buttons { get; }
    }
}
