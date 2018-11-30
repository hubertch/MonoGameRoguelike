using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGameRougelike
{
    public class Console : IScreen
    {
        private IScreen currentScreen;
        
        private static Console instance = null;

        public Vector2 Dimensions { get; private set; } = new Vector2(800f, 560f);

        public ContentManager Content { get; private set; }

        public static Console Instance
        {
            get
            {
                if (instance == null) instance = new Console();

                return instance;
            }
        }

        public void LoadContent(ContentManager content)
        {
            Content = content;
            currentScreen = new DungeonMap(16f, 16f);
            currentScreen.LoadContent(content);
        }

        public void Unload()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch sprite)
        {
            currentScreen.Draw(sprite);
        }
    }
}
