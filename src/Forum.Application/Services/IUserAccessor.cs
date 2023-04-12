namespace Forum.Application.Services
{
    public interface IUserAccessor
    {
        Guid GetId();
        String GetUserName();

    }
}
