using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameRougelike
{
    public class DungeonCell : Cell
    {
        public DungeonCell(int x, int y, bool isTransparent, bool isWalkable, bool isInFov) : base(x,y,isTransparent,isWalkable,isInFov)
        {

        }

        public DungeonCell(int x, int y, bool isTransparent, bool isWalkable, bool isInFov, bool isExplored) : base(x, y, isTransparent, isWalkable, isInFov, isExplored)
        {

        }


    }
}
