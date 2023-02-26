using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Entities;
using Forum.Domain.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _context;

        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Execute()
        {
            await _context.Database.EnsureDeletedAsync();

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

            if (!await _context.Users.AnyAsync())
            {
                foreach (var user in Users)
                {
                    await _userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (!await _context.UserPhotos.AnyAsync())
            {
                var user =
                    await _context.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(b => b.UserName == "Admin");
            }

            if (!await _context.Roles.AnyAsync())
            {
                foreach (var role in Roles)
                    await _roleManager.CreateAsync(role);

                var user = await _context.Users.FirstOrDefaultAsync(b => b.UserName == Users[0].UserName);
                await _userManager.AddToRoleAsync(user, SD.AdminRole);

                var user2 = await _context.Users.FirstOrDefaultAsync(b => b.UserName == Users[1].UserName);
                await _userManager.AddToRoleAsync(user2, SD.SuperManagerRole);

                var user3 = await _context.Users.FirstOrDefaultAsync(b => b.UserName == Users[2].UserName);
                await _userManager.AddToRoleAsync(user3, SD.UserRole);
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

        private List<AppUser> Users =>
            new List<AppUser>
            {
                new AppUser{
                     Id = Guid.NewGuid(),
                     UserName = "Admin",
                     Email = "m.senator.6247@gmail.com" ,
                     PhoneNumber = "09211573936" ,
                     Name = "AmirReza",
                     Family = "Mohammadi",
                     Age = new DateTime(2002 , 8 , 26),
                     IsMale= true,
                     Photos = new List<UserPhoto>
                     {
                         new UserPhoto{Id = Guid.NewGuid() , Url = "f6eaa1fb-4d06-400e-914f-7b41c6fc0918.jpg" , IsMain = true , Name = "Test.jpg"}
                     }
                },
                new AppUser{
                     Id = Guid.NewGuid(),
                     UserName = "Nima123",
                     PhoneNumber = "09182108077" ,
                     Name = "Nima",
                     Family = "Navidi",
                     Age = new DateTime(2000 , 7 , 26),
                     IsMale= true
                },
                new AppUser{
                     Id = Guid.NewGuid(),
                     UserName = "Pari1380",
                     Email = "pari1380@gmail.com",
                     Name = "Parisa",
                     Family = "Moradi",
                     Age = new DateTime(2001 , 4 , 26),
                     IsMale= false
                },
            };

        private List<AppRole> Roles =>
            new List<AppRole>
            {
                new AppRole{
                    Id = Guid.Parse("CFD0038F-C2C2-483C-8908-C5234417F738") ,
                    Name = SD.AdminRole ,
                    Description = "Managment every thing"},
                new AppRole
                {
                    Id = Guid.Parse("CFD0038F-C2C2-483C-8908-C5234417F739"),
                    Name = SD.SuperManagerRole,
                    Description = "Manage all topic and communities"
                },
                new AppRole
                {
                    Id = Guid.Parse("CFD0038F-C2C2-483C-8908-C5234417F740"),
                    Name = SD.ManagerRole,
                    Description = "Manage specifiec topic and communities"
                },
                new AppRole
                {
                    Id = Guid.Parse("CFD0038F-C2C2-483C-8908-C5234417F741"),
                    Name = SD.UserRole,
                    Description = "Common user"
                },

            };
    }
}
