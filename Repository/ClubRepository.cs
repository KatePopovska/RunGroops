using Microsoft.EntityFrameworkCore;
using RunGroops.Data;
using RunGroops.Interfaces;
using RunGroops.Models;

namespace RunGroops.Repository
{
    public class ClubRepository : IClubRepository
    {
        private ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
           var clubToDelete =  _context.Clubs.FirstOrDefault<Club>(s => s.Id == club.Id);
            if (clubToDelete == null)
            {
                throw new KeyNotFoundException("Not Found");
            }
            _context.Remove(clubToDelete);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
           return await _context.Clubs.ToListAsync();
        }

        public async Task<IEnumerable<Club>> GetByCity(string city)
        {
            return await _context.Clubs.Where(s => s.Address.City.Contains(city))
                .Distinct().ToListAsync();
        }

        public async Task<Club> GetById(int id)
        {
            return await _context.Clubs.Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
