using Microsoft.Xna.Framework;
using RogueSharp;

namespace Game1.Interfaces
{
    public interface IGameObject
    {
        int X { get; set; }
        int Y { get; set; }
        string Glyph { get; set; }
        Color Color { get; set; }
    }
}
