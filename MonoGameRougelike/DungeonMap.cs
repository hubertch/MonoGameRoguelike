using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RogueSharp;
using RogueSharp.MapCreation;

namespace MonoGameRougelike
{
    public class DungeonMap : IScreen
    {
        IMap map;
        public Vector2 CellDimensions { get; private set; }
        Texture2D wall;
        Texture2D floor;

        public DungeonMap(float cellWidth, float cellHeight, int maxRooms = 20, int roomMaxSize = 8, int roomMinSize = 3)
        {
            CellDimensions = new Vector2(cellWidth, cellHeight);
            map = Map.Create(new RandomRoomsMapCreationStrategy<Map>((int)(Console.Instance.Dimensions.X / CellDimensions.X), (int)(Console.Instance.Dimensions.Y / CellDimensions.Y), maxRooms, roomMaxSize, roomMinSize));
        }

        public void LoadContent(ContentManager content)
        {
            wall = content.Load<Texture2D>("wall");
            floor = content.Load<Texture2D>("floor");
        }

        public void Unload()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Cell cell in map.GetAllCells())
            {
                var position = new Vector2(cell.X * CellDimensions.X, cell.Y * CellDimensions.Y);
                if (cell.IsWalkable)
                {
                    spriteBatch.Draw(floor, position, Color.White);
                }
                else
                {
                    spriteBatch.Draw(wall, position,  Color.White);
                }
            }
        }
    }
}
