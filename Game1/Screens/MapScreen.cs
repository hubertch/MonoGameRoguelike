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
        private static MapScreen instance = null;

        public static MapScreen Instance
        {
            get
            {
                if (instance == null) instance = new MapScreen();

                return instance;
            }
        }

        public void Draw(DrawView drawView)
        {
            drawView.DrawMap();
        }

        public CommandResult RespondToUserInput(Keys[] arraykeys, ScreenManager screens)
        {
            return CommandSystem.Instance.Commands[arraykeys[0]].Invoke();
        }
    }
}
