using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.UI
{
    public class UserResponse
    {
        public string Command { get; private set; }
        public string Argument { get; private set; }

        public UserResponse(string command, string argument=null)
        {
            Command = command;
            Argument = argument;
        }
    }
}
