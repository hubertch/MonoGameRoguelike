using Game1.Core;

namespace Game1.Interfaces
{
    public interface IItem : IGameObject, IDescription
    {
        int Id { get; }
        int Cost { get; set; }
        int Weight { get; set; }
        TypeItem Type { get; set; }
    }
}
