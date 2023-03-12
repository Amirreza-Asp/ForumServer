using Forum.Application.Services;
using Forum.Domain;
using Forum.Domain.Entities;
using Forum.Domain.Entities.Account;
using Forum.Domain.Entities.Communications;
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



            #region Account

            if (!await _context.Users.AnyAsync())
            {
                for (int i = 0; i < 52; i++)
                {
                    foreach (var user in Users)
                    {
                        user.UserName = user.UserName + i;
                        await _userManager.CreateAsync(user, "Pa$$w0rd");
                    }
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

                //var user = await _context.Users.FirstOrDefaultAsync(b => b.UserName == Users[0].UserName);
                //await _userManager.AddToRoleAsync(user, SD.AdminRole);

                //var user2 = await _context.Users.FirstOrDefaultAsync(b => b.UserName == Users[1].UserName);
                //await _userManager.AddToRoleAsync(user2, SD.SuperManagerRole);

                //var user3 = await _context.Users.FirstOrDefaultAsync(b => b.UserName == Users[2].UserName);
                //await _userManager.AddToRoleAsync(user3, SD.UserRole);

                var users = await _context.Users.ToListAsync();
                foreach (var user in users)
                    await _userManager.AddToRoleAsync(user, SD.AdminRole);
            }

            #endregion

            #region Communitications

            if (!await _context.Communities.AnyAsync())
            {
                _context.Communities.AddRange(Communities);
            }

            if (!await _context.Topics.AnyAsync())
            {
                var user = _context.Users.FirstOrDefault(b => b.UserName == "Admin0");

                var topic1 = new Topic
                {
                    Id = Guid.NewGuid(),
                    Author = user,
                    CommunityId = Guid.Parse("F23449E3-A189-44A6-AE3B-89A9BE1AD73C"),
                    Like = 54,
                    DisLike = 78,
                    Title = "how to selling dead coin",
                    View = 327
                };
                var topic2 = new Topic
                {
                    Id = Guid.NewGuid(),
                    Author = user,
                    CommunityId = Guid.Parse("F23449E3-A189-44A6-AE3B-89A9BE1AD73C"),
                    Like = 700,
                    DisLike = 543,
                    Title = "when sell shit coin?",
                    View = 516
                };
                var topic3 = new Topic
                {
                    Id = Guid.NewGuid(),
                    Author = user,
                    CommunityId = Guid.Parse("F23449E3-A189-44A6-AE3B-89A9BE1AD73C"),
                    Like = 852,
                    DisLike = 54,
                    Title = "how long time need to become senior trader",
                    View = 98
                };
                var topic4 = new Topic
                {
                    Id = Guid.NewGuid(),
                    Author = user,
                    CommunityId = Guid.Parse("F23449E3-A189-44A6-AE3B-89A9BE1AD73C"),
                    Like = 925,
                    DisLike = 20,
                    Title = "trading on moon",
                    View = 157
                };

                _context.Topics.AddRange(topic1, topic2, topic3, topic4);
            }

            #endregion

            await _context.SaveChangesAsync();
        }


        private List<Community> Communities =>
            new List<Community>
            {
                new Community{
                    Id = Guid.Parse("F23449E3-A189-44A6-AE3B-89A9BE1AD73C"),
                    Title = "Trading hours" ,
                    Description = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sed voluptatibus expedita accusamus et quia quae animi mollitia eaque qui soluta",
                    Image = "aron-visuals-BXOXnQ26B7o-unsplash.jpg",
                    Icon="icons8-hourglass-64.png"},
                new Community{
                    Title = "Profit" ,
                    Description = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sed voluptatibus expedita accusamus et quia quae animi mollitia eaque qui soluta" ,
                    Image = "jonathan-borba-nRJTf6v9p5Q-unsplash.jpg",
                    Icon = "icons8-duration-finance-64.png"
                },
                new Community{
                    Title = "News" ,
                    Description = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sed voluptatibus expedita accusamus et quia quae animi mollitia eaque qui soluta" ,
                    Image = "markus-spiske-2G8mnFvH8xk-unsplash.jpg",
                    Icon = "icons8-news-64.png"},
                new Community{
                    Title = "Destroy money" ,
                    Description = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sed voluptatibus expedita accusamus et quia quae animi mollitia eaque qui soluta" ,
                    Image = "max-saeling-9_OejvA7ooI-unsplash.jpg",
                    Icon = "icons8-fire-64.png"},
                new Community{
                    Title = "AI" ,
                    Description = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sed voluptatibus expedita accusamus et quia quae animi mollitia eaque qui soluta" ,
                    Image = "michael-dziedzic-uZr0oWxrHYs-unsplash.jpg",
                    Icon = "icons8-bot-64.png"},

                new Community{
                    Title = "World" ,
                    Description = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sed voluptatibus expedita accusamus et quia quae animi mollitia eaque qui soluta" ,
                    Image = "nasa-Q1p7bh3SHj8-unsplash.jpg",
                    Icon = "icons8-earth-globe-64.png"},
                new Community{
                    Title = "Consultation" ,
                    Description = "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Sed voluptatibus expedita accusamus et quia quae animi mollitia eaque qui soluta" ,
                    Image = "priscilla-du-preez-XkKCui44iM0-unsplash.jpg",
                    Icon = "icons8-genealogy-64.png"},

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
                         new UserPhoto{Id = Guid.NewGuid() , Url = "content-img-02.jpg" , IsMain = true , Name = "Test.jpg"},
                         new UserPhoto{Id = Guid.NewGuid() , Url = "full-bg-07-1.jpg" , IsMain = false , Name = "Test.jpg"},
                         new UserPhoto{Id = Guid.NewGuid() , Url = "logo-01.jpg" , IsMain = false , Name = "Test.jpg"},
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
