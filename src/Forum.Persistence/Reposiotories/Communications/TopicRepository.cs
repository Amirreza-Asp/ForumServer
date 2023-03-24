
using AutoMapper;
using Forum.Application.Models;
using Forum.Application.Repositories.Communications;
using Forum.Domain.Entities.Communications;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Reposiotories.Communications
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        public TopicRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ListActionResult<TDto>> GetAllAsync<TDto>(GridQuery query, CancellationToken cancellationToken = default)
        {
            query = await ApplyCommunityFilter(query);
            query = await ApplyAuthorFilter(query);
            return await base.GetAllAsync<TDto>(query, cancellationToken);
        }

        #region Utilities
        private async Task<GridQuery> ApplyCommunityFilter(GridQuery query)
        {

            if (query.Filters.Any(filter => filter.column.ToLower() == "community"))
            {
                if (String.IsNullOrEmpty(query.Filters.First(b => b.column == "community").value))
                {
                    query.RemoveFilter("community");
                    return query;
                }

                var filterCommunityTitle =
                    query.Filters.First(filter => filter.column.ToLower() == "community").value;

                var community =
                        await _context.Communities
                            .Where(b => b.Title.ToLower() == filterCommunityTitle.ToLower())
                            .FirstOrDefaultAsync();

                query.RemoveFilter("community");
                query.AddFilter("communityId", community != null ? community.Id : Guid.NewGuid());
            }

            return query;
        }

        private async Task<GridQuery> ApplyAuthorFilter(GridQuery query)
        {

            if (query.Filters.Any(filter => filter.column.ToLower() == "author"))
            {
                if (String.IsNullOrEmpty(query.Filters.First(b => b.column == "author").value))
                {
                    query.RemoveFilter("author");
                    return query;
                }

                var filterAuthorName =
                    query.Filters.First(filter => filter.column.ToLower() == "author").value;

                var author =
                        await _context.Users
                            .Where(b => (b.Name + " " + b.Family).ToLower() == filterAuthorName.ToLower())
                            .FirstOrDefaultAsync();

                query.RemoveFilter("author");
                query.AddFilter("AuthorId", author != null ? author.Id : Guid.NewGuid());
            }

            return query;
        }
        #endregion
    }
}
