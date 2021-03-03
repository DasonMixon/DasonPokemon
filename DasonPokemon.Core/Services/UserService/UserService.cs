using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DasonPokemon.Core.Entities;
using DasonPokemon.Core.Models;
using MongoDB.Extensions.Repository.Interfaces;

namespace DasonPokemon.Core.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserService(IMongoRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AuthenticateUserResult> Authenticate(string email, string password)
        {
            if (email == null)
                return new AuthenticateUserResult { WasSuccessful = false, FailureReason = "Email cannot be null." };

            if (password == null)
                return new AuthenticateUserResult { WasSuccessful = false, FailureReason = "Password cannot be null." };

            var existingUser = (await _repository.GetManyAsync(u => u.Email == email)).SingleOrDefault();
            if (existingUser == null)
            {
                return new AuthenticateUserResult { WasSuccessful = false, FailureReason = "User was not found." };
            }

            var result = new Hasher().VerifyHash(existingUser.Hash, password);
            switch(result)
            {
                case HashVerificationResult.Failed:
                    return new AuthenticateUserResult { WasSuccessful = false, FailureReason = "Incorrect password." };
                case HashVerificationResult.SuccessRehashNeeded:
                    // TODO: Implement this
                    break;
                case HashVerificationResult.Success:
                    return new AuthenticateUserResult { WasSuccessful = true };
            }

            return new AuthenticateUserResult { WasSuccessful = false, FailureReason = "Unknown error." };
        }

        public Task<AuthorizeUserResult> Authorize(string accessToken, string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CreateUserResult> CreateUser(UserServiceModel user)
        {
            if (user == null)
                return new CreateUserResult { WasSuccessful = false, FailureReason = "User cannot be null." };

            var existingUser = await _repository.GetManyAsync(u => u.Email == user.Email);
            if (existingUser.SingleOrDefault() != null)
            {
                return new CreateUserResult { WasSuccessful = false, FailureReason = "Email unavailable." };
            }

            var newUser = _mapper.Map<User>(user);
            newUser.Hash = new Hasher().Hash(user.Password);

            await _repository.AddAsync(newUser);

            return new CreateUserResult { WasSuccessful = true };
        }
    }
}
