using AutoMapper;
using MeetSport.Dbo;
using MeetSport.Repositories;
using MeetSport.Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetSport.Dto.Users;
using MeetSport.Services.JwtGenerator;
using MeetSport.Services.PasswordHasher;
using MeetSport.Exceptions;
using Microsoft.Extensions.Logging;

namespace MeetSport.Business.Users
{
    public class DbUserBusiness : Business<User, IRepository<User>>, IUserBusiness<User>
    {
        private IJwtGenerator _jwtGenerator;
        private IPasswordHasher _passwordHasher;

        public DbUserBusiness(IRepository<User> repository, IMapper mapper, ILogger<IUserBusiness<User>> logger, IJwtGenerator jwtGenerator,  IPasswordHasher passwordHasher) : base(repository, mapper, logger)
        {
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
        }

        public Task<string> Authenticate(AuthenticateUserDto authenticationUserDto)
        {
            User user = null;

            try
            {
                user = _repository.GetAll().Where(user => user.Mail == authenticationUserDto.Mail).Include(user => user.RoleNavigation).Single();
            }
            finally
            {
                if (user == null || !_passwordHasher.Check(authenticationUserDto.Password, user.HashedPassword))
                {
                    throw new AuthenticationFailedException();
                }
            }

            string token = _jwtGenerator.GenerateToken(user.Id, user.RoleNavigation.Name);
            return Task.FromResult(token);
        }

        public async Task<Dto> CreateUser<Dto>(CreateUserDto createUserDto)
        {
            User user = _mapper.Map<User>(createUserDto);
            user.Role = 1;
            user.HashedPassword = _passwordHasher.Hash(createUserDto.Password);
            user = await _repository.Add(user);
            Dto mappedUser = _mapper.Map<Dto>(user);
            return mappedUser;
        }

        public async Task<Dto> UpdateUser<Dto>(UpdateUserDto updateDto, ulong primaryKey)
        {
            User user = await _repository.Get(primaryKey);
            _mapper.Map(updateDto, user);

            if(updateDto.Password != null)
            {
                user.HashedPassword = _passwordHasher.Hash(updateDto.Password);
            }

            user = await _repository.Update(user);
            Dto mappedEntity = _mapper.Map<Dto>(user);
            return mappedEntity;
        }
    }
}
