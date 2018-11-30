using Game1.Core;
using Game1.Interfaces;
using Game1.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class CommandSystem
    {
        private Game1 _game;
        private static CommandSystem _instance = null;
        public ScreenManager Screens { get; set; }

        public static CommandSystem Instance
        {
            get
            {
                if (_instance == null) _instance = new CommandSystem();

                return _instance;
            }
        }

        public Dictionary<Keys, Func<CommandResult>> Commands { get; private set; }

        private CommandSystem()
        {
            Commands = new Dictionary<Keys, Func<CommandResult>>()
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

        public void LoadContent(Game1 game, ScreenManager screens)
        {
            _game = game;
            Screens = screens;
        }

        private CommandResult ShowDropItemView()
        {
            return new CommandResult(true, false, Screens.InventoryScreen);
        }

        #region Move Actions
        private CommandResult MoveToLeft()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X - 1, _game.Player.Y);
            return new CommandResult(isSuccess, isSuccess, Screens.MapScreen);
        }

        private CommandResult MoveToRight()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X + 1, _game.Player.Y);
            return new CommandResult(isSuccess, isSuccess, Screens.MapScreen);
        }

        private CommandResult MoveToUp()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X, _game.Player.Y - 1);
            return new CommandResult(isSuccess, isSuccess, Screens.MapScreen);

        }

        private CommandResult MoveToDown()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X, _game.Player.Y + 1);
            return new CommandResult(isSuccess, isSuccess, Screens.MapScreen);

        }

        private CommandResult MoveToUpLeft()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X - 1, _game.Player.Y - 1);
            return new CommandResult(isSuccess, isSuccess, Screens.MapScreen);

        }

        private CommandResult MoveToUpRight()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X + 1, _game.Player.Y - 1);
            return new CommandResult(isSuccess, isSuccess, Screens.MapScreen);
        }

        private CommandResult MoveToDownRight()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X + 1, _game.Player.Y + 1);
            return new CommandResult(isSuccess, isSuccess, Screens.MapScreen);
        }

        private CommandResult MoveToDownLeft()
        {
            bool isSuccess = _game.Level.SetActorPosition(_game.Player, _game.Player.X - 1, _game.Player.Y + 1);
            return new CommandResult(isSuccess, isSuccess, Screens.MapScreen);
        }
        #endregion

        #region Item Actions
        private CommandResult TakeItem()
        {
            IItem item = _game.Level.TakeItem(_game.Player.X, _game.Player.Y);

            if(item != null) _game.Player.TakeItem(item);

            return new CommandResult(item != null, item != null, Screens.MapScreen);
        }

        private CommandResult ShowInventory()
        {
            return new CommandResult(true, false, Screens.InventoryScreen);
        }
        #endregion

        private CommandResult RefreshMap()
        {
            return new CommandResult(true, false, Screens.MapScreen);
        }

        private CommandResult GameExit()
        {
            _game.Exit();
            return new CommandResult(true, true, Screens.MapScreen);
        }

    }
}
