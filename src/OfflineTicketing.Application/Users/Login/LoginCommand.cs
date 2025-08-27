using MediatR;
using OfflineTicketing.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Users.Login
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
