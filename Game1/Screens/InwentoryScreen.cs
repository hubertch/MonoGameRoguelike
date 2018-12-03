using Game1.Core;
using Game1.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1.Screens
{
    public class InventoryScreen : IScreen
    {
        private static InventoryScreen instance = null;

        public  static InventoryScreen Instance
        {
            get
            {
                if (instance == null) instance = new InventoryScreen();

                return instance;
            }
        }

        public void Draw(DrawView drawView)
        {
            drawView.DrawActorInventory();
        }

        public CommandResult RespondToUserInput(Keys[] arraykeys)
        {
            //return CommandSystem.Instance.Commands[arraykeys[0]].Invoke();
            return null;
        }
    }
}
