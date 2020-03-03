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

namespace MeetSport.Business.Users
{
    public class DbUserBusiness : Business<User, IUserRepository<User>>, IUserBusiness<User>
    {
        private IJwtGenerator _jwtGenerator;
        private IPasswordHasher _passwordHasher;

        public DbUserBusiness(IUserRepository<User> repository, IMapper mapper, IJwtGenerator jwtGenerator, IPasswordHasher passwordHasher) : base(repository, mapper)
        {
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Authenticate(UserAuthenticationDto userAuthenticationDto)
        {
            /*User user = await _repository.FindByMail(userAuthenticationDto.Email);
            if (!_passwordHasher.Check(userAuthenticationDto.Password, user.HashedPassword))
            {
                throw new Exception("Invalid mail or password");
            }

            string token = _jwtGenerator.GenerateToken(user.Id, user.Role);
            */
            string token = _jwtGenerator.GenerateToken(0, 1, new string[] { "sqfd", "CanRead", "pk"});
            return token;
        }
    }
}
