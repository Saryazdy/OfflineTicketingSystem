using MediatR;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Application.Users.Dtos;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // 1. Find user by email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            // 2. Verify password (BCrypt)
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

            // 3. Generate token
            var token = _jwtTokenService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                UserId = user.Id,
                FullName = user.FullName,
                Role = user.Role.ToString()
            };
        }
    }
}
