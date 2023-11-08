using RunGroops.Models;

namespace RunGroops.Interfaces
{
    public interface IRaceRepository
    {

        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetById(int id);
        Task<IEnumerable<Race>> GetByCity(string city);
        Task<Race> GetByIdAsyncNoTracking(int id);
        bool Add(Race race);
        bool Update(Race race);
        bool Delete(Race race);
        bool Save();
    }
}
