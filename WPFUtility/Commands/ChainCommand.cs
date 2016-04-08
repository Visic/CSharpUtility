using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFUtility {
    public class ChainCommand : ICommand {
        public event EventHandler CanExecuteChanged;
        Action<object> _execute;
        Func<object, bool> _canExecute;

        public ChainCommand(ICommand postCommand, Action<object> execute, Func<object, bool> canExecute = null) {
            if (canExecute == null) canExecute = x => true;

            _execute = x => {
                execute(x);
                postCommand.Execute(x);
            };

            _canExecute = x => canExecute(x) && postCommand.CanExecute(x);
        }

        public bool CanExecute(object parameter) {
            return _canExecute(parameter);
        }

        public void Execute(object parameter) {
            _execute(parameter);
        }
    }
}
