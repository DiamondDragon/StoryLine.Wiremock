using Microservice.Gateway.Contracts;

namespace Microservice.Gateway.Services
{
    public interface IGatewayResource
    {
        User GetUser(long id);
        UserCollection GetAllUsers(int skip, int take);
        User AddUser(User user);
        Document GetUserDocument(long userId, int documentId);
        DocumentCollection GetUserDocuments(long userId, int skip, int take);
    }
}