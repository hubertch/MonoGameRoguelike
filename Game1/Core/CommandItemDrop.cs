using Game1.Interfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Core
{
    public class CommandItemDrop
    {
        Game1 _game;

        public CommandItemDrop(Game1 game)
        {
            _game = game;
        }

        //public bool DropItem()
        //{
        //    _game.TypeView = TypeView.Map;

        //    IItem item = _game.Player.DropItem(1);
        //    if (item == null) return IsNPCTurn = false;

        //    _game.Level.ItemDroped(item);
        //    return IsNPCTurn = true;
        //}
    }
}
