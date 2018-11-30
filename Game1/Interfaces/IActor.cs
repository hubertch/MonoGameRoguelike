using Game1.Models;
using System.Collections.Generic;

namespace Game1.Interfaces
{
    public interface IActor : IDescription, IGameObject
    {
        int ActualHitPoint { get; set; }
        int MaximumHitPoint { get; set; }

        int RangeFov { get; set; }

        Inventory Items { get; }

        IItem DropItem(int id);
        void TakeItem(IItem item);
    }
}
