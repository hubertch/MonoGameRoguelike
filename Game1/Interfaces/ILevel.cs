using Game1.Models;
using RogueSharp;
using System;
using System.Collections.Generic;

namespace Game1.Interfaces
{
    public interface ILevel
    {
        bool SetActorPosition(IActor actor, int x, int y);
        ICell GetRandomWalkableCell(Random random);
        void ComputeFov(int centreX, int centreY, int range);
        void UpdateMapToDraw(IActor player);
        void FirstView(IActor player);
        void NPCTurn(IActor player);
        IItem TakeItem(int x, int y);
        void ItemDroped(IItem item);
        IEnumerable<DrawCell>  GetMapToDraw { get; }
    }
}
