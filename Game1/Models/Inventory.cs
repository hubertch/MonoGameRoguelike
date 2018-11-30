using Game1.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Models
{
    public class Inventory
    {
        List<IItem> _itemList = new List<IItem>();

        public Dictionary<char, IItem> ItemDictonaryToDrop
        {
            get
            {
                char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                Dictionary<char, IItem> itemList = new Dictionary<char, IItem>();
                int index = 0;
                foreach (IItem item in _itemList)
                {
                    itemList.Add(alpha[index++], item);
                }
                return itemList;
            }
        }

        public void AddItem(IItem item) => _itemList.Add(item);

        public void RemoveItem(IItem item) => _itemList.Remove(item);

        public bool Contein(int id) => _itemList.Any(i => i.Id == id);

        public IItem GetItemById(int id)
        {
            IItem item = _itemList.FirstOrDefault(i => i.Id == id);

            if (item != null) RemoveItem(item);

            return item;
        }

        public void DrawInventoryToDrop()
        {

        }

        public void DrawInformation()
        {

        }

        internal void DrawInventory(SpriteBatch spriteBatch, SpriteFont font, int heightCell, int widthCell, int i)
        {
            spriteBatch.DrawString(font, "Twoj inwentarz: ", new Vector2(2 * widthCell, i * heightCell), Color.White);
            foreach (IItem item in _itemList)
            {
                i++;
                spriteBatch.DrawString(font, $" - {item.Name}", new Vector2(2 * widthCell, 1.5f * i * heightCell), Color.White);
            }
        }
    }
}
