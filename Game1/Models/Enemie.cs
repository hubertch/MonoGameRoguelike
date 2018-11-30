using Game1.Interfaces;
using Microsoft.Xna.Framework;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Models
{
    public class Enemie : IActor
    {
        public Enemie(ICell cell)
        {
            Glyph = "g";
            RangeFov = 3;
            X = cell.X;
            Y = cell.Y;
        }

        public Enemie(ICell cell, string glyph, int rangeFov) : this(cell)
        {
            Glyph = glyph;
            RangeFov = rangeFov;
        }

        public int ActualHitPoint { get; set; }
        public int MaximumHitPoint { get; set; }
        public int RangeFov { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Glyph { get; set; }
        public Color Color { get; set; }

        public Inventory Items { get; }

        public IItem DropItem(int id)
        {
            throw new NotImplementedException();
        }

        public void TakeItem(IItem item)
        {
            throw new NotImplementedException();
        }
    }
}
