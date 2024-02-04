/*using CarReview.Data;
using CarReview.Interfaces;
using CarReview.Models;
using Microsoft.EntityFrameworkCore;

namespace CarReview.Repository
{
    public class ProfilRepository : IProfilRepository
    {
        private readonly DataContext _context;
        public ProfilRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateProfile(Profil profil)
        {
            _context.Add(profil);
            return Save();
        }

        public Profil GetProfile(int id)
        {
            return _context.Profiles.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Profil> GetProfiles()
        {
            return _context.Profiles.ToList();
        }

        public bool ProfileExists(int id)
        {
            return _context.Profiles.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}*/
