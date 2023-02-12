using Forum.Application.Services;
using Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _context;

        public DbInitializer(AppDbContext context)
        {
            _context = context;
        }

        public async Task Execute()
        {
            //await _context.Database.EnsureDeletedAsync();

            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                    await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
            }

            if (!await _context.Communities.AnyAsync())
            {
                _context.Communities.AddRange(Communities);
            }

            await _context.SaveChangesAsync();
        }


        private List<Community> Communities =>
            new List<Community>
            {
                new Community{Title = "Sport" ,
                    Description = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sed voluptatibus expedita accusamus et quia quae animi mollitia eaque qui soluta" },
                new Community{Title = "Sience" , Description = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sed voluptatibus expedita accusamus et quia quae animi mollitia eaque qui soluta" },
                new Community{Title = "Culture" , Description = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sed voluptatibus expedita accusamus et quia quae animi mollitia eaque qui soluta" },
            };
    }
}
