using Microsoft.EntityFrameworkCore;
using RunGroops.Data;
using RunGroops.Interfaces;
using RunGroops.Models;

namespace RunGroops.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Race race)
        {
            _context.Races.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            var raceToDelete = _context.FindAsync<Race>(race);
            if(raceToDelete == null)
            {
                throw new KeyNotFoundException("Not Found");
            }
            _context.Remove(raceToDelete);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetByCity(string city)
        {
            return await _context.Races.Where(s => s.Address.City.Contains(city))
                .Distinct().ToListAsync();
        }

        public async Task<Race> GetById(int id)
        {
            return await _context.Races.Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Race race)
        {
            var raceToUpdate = _context.FindAsync<Race>(race);
            if (raceToUpdate == null)
            {
                throw new KeyNotFoundException("Not Found");
            }
            _context.Update(raceToUpdate);
            return Save();
        }
    }
}
