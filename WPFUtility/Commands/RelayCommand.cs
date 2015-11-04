using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFUtility {
    public class RelayCommand : ICommand {
        public event EventHandler CanExecuteChanged;
        Action<object> _execute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null) {
            _execute = execute;
            CanExecuteDelegate = canExecute ?? new Func<object, bool>(x => true);
        }

        public Func<object, bool> CanExecuteDelegate { get; private set; }

        public bool CanExecute(object parameter) {
            return CanExecuteDelegate(parameter);
        }

        public void Execute(object parameter) {
            _execute(parameter);
        }
    }
}
