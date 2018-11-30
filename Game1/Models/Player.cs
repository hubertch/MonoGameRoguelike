using System.Collections.Generic;
using System.Linq;
using Game1.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RogueSharp;

namespace Game1.Models
{
    public class Player : IActor
    {
        public Player(ICell cell)
        {
            RangeFov = 6;
            X = cell.X;
            Y = cell.Y;
        }

        public Player(ICell cell, int rangeFov) : this(cell)
        {
            RangeFov = rangeFov;
        }

        public int RangeFov { get; set; }
        public int ActualHitPoint { get; set; }
        public int MaximumHitPoint { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Glyph { get; set; } = "@";
        public Color Color { get; set; } = Color.White;

        public Inventory Items { get; } = new Inventory();

        public IItem DropItem(int id)
        {
            IItem item = null;

            if (Items.Contein(id))
            {
                item = Items.GetItemById(id);

                item.X = X;
                item.Y = Y;
            }

            return item;
        }

        public void TakeItem(IItem item)
        {
            Items.AddItem(item);
        }
    }
}
