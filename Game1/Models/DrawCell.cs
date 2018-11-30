using Game1.Core;
using Microsoft.Xna.Framework;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Models
{
    public class DrawCell 
    {
        public int Ox { get; set; }
        public int Oy { get; set; }
        public ICell ParentCell { get; set; }
        public string Glyph { get; set; }
        public TypeObject Type { get; set; } = TypeObject.None;
        public Color Color { get; set; }
    }
}
