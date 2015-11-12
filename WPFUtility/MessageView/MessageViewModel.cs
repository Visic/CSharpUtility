using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtility {
    public class MessageViewModel :ViewModelBase {
        public MessageViewModel(string message) {
            Message = message;
        }

        public string Message { get; }
    }
}
