using Forum.Application.Utility;
using Forum.Domain.Dtoes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Features.Queries.Admin.Dashboard
{
    public class DashboardHandler : IRequestHandler<DashboardQuery, DashboardDto>
    {
        private readonly AppDbContext _context;

        public DashboardHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> Handle(DashboardQuery request, CancellationToken cancellationToken)
        {
            var model = new DashboardDto();

            #region number of topics
            var numberOfTopicPerMonth =
                await _context.Topics
                   .GroupBy(
                        o => new
                        {
                            Month = o.CreatedAt.Month
                        })
                    .Select(b => new NumberOfTopicsPerMonth { Month = b.Key.Month.ToString(), Count = b.Count() })
                    .OrderByDescending(b => b.Month)
                    .ToListAsync(cancellationToken);

            for (int i = 1; i <= 12; i++)
            {
                if (!numberOfTopicPerMonth.Select(b => b.Month).Contains(i.ToString()))
                    numberOfTopicPerMonth.Add(new NumberOfTopicsPerMonth { Month = i.ToString() });

                numberOfTopicPerMonth.Where(b => b.Month == i.ToString()).First().Month = DateTimeUtility.ToMonthName(i);
                model.NumberOfTopicPerMonth.Add(numberOfTopicPerMonth.First(b => b.Month == DateTimeUtility.ToMonthName(i)));
            }
            #endregion

            #region new members joined
            var newMembersJoined =
                await _context.Users
                   .GroupBy(
                        o => new
                        {
                            Month = o.RegisterAt.Month
                        })
                    .Select(b => new NewMembersJoined { Month = b.Key.Month.ToString(), Count = b.Count() })
                    .OrderByDescending(b => b.Month)
                    .ToListAsync(cancellationToken);

            for (int i = 1; i <= 12; i++)
            {
                if (!newMembersJoined.Select(b => b.Month).Contains(i.ToString()))
                    newMembersJoined.Add(new NewMembersJoined { Month = i.ToString() });

                newMembersJoined.Where(b => b.Month == i.ToString()).First().Month = DateTimeUtility.ToMonthName(i);

                model.NewMembersJoined.Add(newMembersJoined.First(b => b.Month == DateTimeUtility.ToMonthName(i)));
            }
            #endregion

            #region communities with most topics
            var communitiesWithMostTopics =
                await _context.Communities
                    .OrderByDescending(b => b.Topics.Count())
                    .Take(5)
                    .Select(b => new CommunitiesWithMostTopics { Community = b.Title, TopicsCount = b.Topics.Count() })
                    .ToListAsync(cancellationToken);
            #endregion

            #region topics with most comments
            var topicsWithMostComments =
                await _context.Topics
                    .OrderByDescending(b => b.Comments.Count())
                    .Select(b => new TopicsWithMostComments
                    {
                        Topic = b.Title,
                        CommentsCount = b.Comments.Count()
                    })
                    .ToListAsync(cancellationToken);
            #endregion

            #region users gender
            var usersGender =
                await _context.Users
                    .GroupBy(b =>
                    new
                    {
                        IsMale = b.IsMale
                    })
                    .Select(b => new UsersGender { IsMale = b.Key.IsMale, Count = b.Count() })
                    .ToListAsync(cancellationToken);
            #endregion

            #region users count by age
            var usersCountByAge =
                await _context.Users
                    .GroupBy(b => new
                    {
                        Year = b.Age.Year
                    })
                    .Select(b => new UsersCountByAge
                    {
                        Age = DateTime.Now.Year - b.Key.Year,
                        Count = b.Count()
                    })
                    .ToListAsync(cancellationToken);
            #endregion

            model.CommunitiesWithMostTopics = communitiesWithMostTopics;
            model.TopicsWithMostComments = topicsWithMostComments;
            model.UsersGender = usersGender;
            model.UsersCountByAge = usersCountByAge;
            model.UsersCount = await _context.Users.CountAsync(cancellationToken);
            model.CommentsCount = await _context.Comments.CountAsync(cancellationToken);
            model.TopicsCount = await _context.Topics.CountAsync(cancellationToken);
            model.CommunitiesCount = await _context.Communities.CountAsync(cancellationToken);

            return model;
        }
    }
}
