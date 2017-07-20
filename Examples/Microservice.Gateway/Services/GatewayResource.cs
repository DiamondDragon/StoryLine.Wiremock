using System;
using Microservice.Gateway.Contracts;

namespace Microservice.Gateway.Services
{
    public class GatewayResource : IGatewayResource
    {
        private const string UsersService = "users";
        private const string DocumentsService = "documents";

        private readonly IServiceClientFactory _clientFactory;

        public GatewayResource(IServiceClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public User GetUser(long id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return _clientFactory.Create(UsersService).Get<User>($"users/{id}");
        }

        public UserCollection GetAllUsers(int skip, int take)
        {
            if (skip < 0)
                throw new ArgumentOutOfRangeException(nameof(skip));
            if (take <= 0)
                throw new ArgumentOutOfRangeException(nameof(take));
            if (take > Constants.Collections.MaxTake)
                throw new ArgumentOutOfRangeException(nameof(take));

            return _clientFactory.Create(UsersService).Get<UserCollection>($"users?skip={skip}&take={take}");
        }

        public User AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return _clientFactory.Create(UsersService).Post<User, User>("users", user);
        }

        public Document GetUserDocument(long userId, int documentId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));
            if (documentId <= 0)
                throw new ArgumentOutOfRangeException(nameof(documentId));

            return _clientFactory.Create(DocumentsService).Get<Document>($"v2/documents/{documentId}");
        }

        public DocumentCollection GetUserDocuments(long userId, int skip, int take)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));
            if (skip < 0)
                throw new ArgumentOutOfRangeException(nameof(skip));
            if (take <= 0)
                throw new ArgumentOutOfRangeException(nameof(take));
            if (take > Constants.Collections.MaxTake)
                throw new ArgumentOutOfRangeException(nameof(take));

            return _clientFactory.Create(DocumentsService).Get<DocumentCollection>($"v2/documents?skip={skip}&take={take}&user={userId}");
        }
    }
}
