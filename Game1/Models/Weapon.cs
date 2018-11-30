using Game1.Core;
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
    public class Weapon : IItem
    {
        public Weapon(ICell cell)
        {
            X = cell.X;
            Y = cell.Y;
            Id = ID.Instance.GetNumber();
        }

        public int Cost { get; set; }
        public int Weight { get; set; }
        public TypeItem Type { get; set; } = TypeItem.Weapon;
        public int X { get; set; }
        public int Y { get; set; }
        public string Glyph { get; set; } = "(";
        public Color Color { get; set; } = Color.White;
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; private set; }
    }
}
