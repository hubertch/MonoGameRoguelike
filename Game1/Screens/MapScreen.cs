using Game1.Core;
using Game1.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Screens
{
    public class MapScreen : IScreen
    {
        private Dictionary<Keys, Func<CommandResult>> _commands;
        private Game1 _game;
        private static MapScreen instance = null;

        public static MapScreen Instance
        {
            get
            {
                if (instance == null) instance = new MapScreen();

                return instance;
            }
        }

        private MapScreen()
        {
            _commands = new Dictionary<Keys, Func<CommandResult>>()
            {{ Keys.NumPad1, MoveToDownLeft },
            { Keys.NumPad2, MoveToDown },
            { Keys.NumPad3, MoveToDownRight },
            { Keys.NumPad4, MoveToLeft },
            { Keys.NumPad6, MoveToRight },
            { Keys.NumPad7, MoveToUpLeft },
            { Keys.NumPad8, MoveToUp },
            { Keys.NumPad9, MoveToUpRight },

            { Keys.T, TakeItem },
            { Keys.I, ShowInventory },
            { Keys.D, ShowDropItemView },

            { Keys.Space, RefreshMap },
            { Keys.Escape, GameExit }};
        }

        public void Inicialize(Game1 game)
        {
            _game = game;
        }

        public void Draw(DrawView drawView)
        {
            drawView.DrawMap();
        }

        public CommandResult RespondToUserInput(Keys[] arraykeys)
        {
            return _commands[arraykeys[0]].Invoke();
        }

        #region Move Actions
        private CommandResult MoveToLeft()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X - 1, _game.Player.Y);
            return new CommandResult(isSuccess, isSuccess, this);
        }

        private CommandResult MoveToRight()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X + 1, _game.Player.Y);
            return new CommandResult(isSuccess, isSuccess, this);
        }

        private CommandResult MoveToUp()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X, _game.Player.Y - 1);
            return new CommandResult(isSuccess, isSuccess, this);

        }

        private CommandResult MoveToDown()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X, _game.Player.Y + 1);
            return new CommandResult(isSuccess, isSuccess, this);

        }

        private CommandResult MoveToUpLeft()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X - 1, _game.Player.Y - 1);
            return new CommandResult(isSuccess, isSuccess, this);

        }

        private CommandResult MoveToUpRight()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X + 1, _game.Player.Y - 1);
            return new CommandResult(isSuccess, isSuccess, this);
        }

        private CommandResult MoveToDownRight()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X + 1, _game.Player.Y + 1);
            return new CommandResult(isSuccess, isSuccess, this);
        }

        private CommandResult MoveToDownLeft()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X - 1, _game.Player.Y + 1);
            return new CommandResult(isSuccess, isSuccess, this);
        }
        #endregion

        #region Item Actions
        private CommandResult TakeItem()
        {
            IItem item = _game.Level.TakeItem(_game.Player.X, _game.Player.Y);

            if (item != null) _game.Player.TakeItem(item);

            return new CommandResult(item != null, item != null, this);
        }

        private CommandResult ShowInventory()
        {
            return new CommandResult(true, false, InventoryScreen.Instance);
        }
        #endregion

        private CommandResult RefreshMap()
        {
            return new CommandResult(true, false, this);
        }

        private CommandResult GameExit()
        {
            _game.Exit();
            return new CommandResult(true, true, this);
        }

        private CommandResult ShowDropItemView()
        {
            return new CommandResult(true, false, InventoryScreen.Instance);
        }
    }
}
