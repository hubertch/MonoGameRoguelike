using Game1.Interfaces;
using Game1.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Core
{
    public class DrawView
    {
        private SpriteFont _font;
        private readonly int _heightCell = 18;
        private readonly int _widthCell = 18;
        private readonly int _i = 18; //Odstęp między wierszami.
        private IActor _player;
        private ILevel _level;
        private SpriteBatch _spriteBatch;

        private static DrawView _instance = null;

        public static DrawView Instance
        {
            get
            {
                if (_instance == null) _instance = new DrawView();

                return _instance;
            }
        }

        public void LoadContent(ContentManager content, SpriteBatch spriteBatch, IActor player, ILevel level)
        {
            _font = content.Load<SpriteFont>("Console");
            _spriteBatch = spriteBatch;
            _player = player;
            _level = level;
        }

        public void DrawMap()
        {
            int heightCell = 18;
            int widthCell = 12;

            _spriteBatch.Begin();
            for (int x = 0; x < 25; x++)
                for (int y = 0; y < 25; y++)
                {
                    if (x == 13 && y == 13)
                    {
                        _spriteBatch.DrawString(_font, "@", new Vector2(x * widthCell, y * heightCell), Color.White);
                    }
                    else
                    {
                        if (_level.GetMapToDraw != null)
                        {
                            var cell = _level.GetMapToDraw.FirstOrDefault(c => c.Ox == x && c.Oy == y);
                            if (cell != null)
                            {
                                if (cell.ParentCell.IsInFov)
                                {
                                    _spriteBatch.DrawString(_font, cell.Glyph, new Vector2(x * widthCell, y * heightCell), cell.Color);
                                }
                                else
                                {
                                    if (cell.Type == TypeObject.Wall) _spriteBatch.DrawString(_font, cell.Glyph, new Vector2(x * widthCell, y * heightCell), cell.Color);
                                    else _spriteBatch.DrawString(_font, ".", new Vector2(x * widthCell, y * heightCell), Color.DarkGray);
                                }
                            }
                        }
                    }
                }
            _spriteBatch.End();
        }

        public void DrawActorInventory()
        {
            _spriteBatch.Begin();
            _player.Items.DrawInventory(_spriteBatch, _font, _heightCell, _widthCell, _i);
            _spriteBatch.End();
        }

        public void DrawListInventoryToDrop()
        {
            List<string> AlfabetList = new List<string>() { "A", "B", "C", "D", "E", "F" };
            int heightCell = 18;
            int widthCell = 12;
            int i = 2;
            //int index = 0;
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Co chcesz wyrzucic: ", new Vector2(2 * widthCell, i * heightCell), Color.White);
            //foreach (IItem item in actor.Items)
            //{
            //    i++;
            //    spriteBatch.DrawString(_font, $" [{AlfabetList[index]}] {item.Name}", new Vector2(2 * widthCell, 1.5f * i * heightCell), Color.White);
            //    index++;
            //}
            _spriteBatch.End();
        }

        public void DrawItemStackOnGround()
        {

        }
    }
}
