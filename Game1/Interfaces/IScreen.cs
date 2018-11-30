using Game1.Core;
using Game1.Screens;
using Microsoft.Xna.Framework.Input;

namespace Game1.Interfaces
{
    public interface IScreen
    {
        void Draw(DrawView drawView);
        CommandResult RespondToUserInput(Keys[] arraykeys, ScreenManager screens);
    }
}
