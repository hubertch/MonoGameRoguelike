using Game1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Core
{
    public class CommandResult
    {
        public CommandResult(bool isSuccess, bool isNpcTurn, IScreen screen)
        {
            IsNpcTurn = isNpcTurn;
            IsSuccessAction = isSuccess;
            Screen = screen;
        }

        public bool IsNpcTurn { get; set; }
        public bool IsSuccessAction { get; set; }
        public IScreen Screen { get; set; }
    }
}
