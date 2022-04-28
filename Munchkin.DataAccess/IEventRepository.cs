using Munchkin.Infrastucture.Projections;

namespace Munchkin.DataAccess
{
    public interface IEventRepository
    {
        Game GetGame(Guid id);
    }
}