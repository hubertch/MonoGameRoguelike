using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameRougelike
{
    public interface IScreen
    {
        void LoadContent(ContentManager content);

        void Unload();

        void Update(GameTime gameTime);

        void Draw(SpriteBatch sprite);
    }
}
