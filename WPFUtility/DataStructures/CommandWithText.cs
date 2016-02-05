using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFUtility {
    public class CommandWithText {
        public CommandWithText(ICommand command, string text) {
            Text = text;
            Command = command;
        }

        public string Text { get; }
        public ICommand Command { get; }
    }
}
