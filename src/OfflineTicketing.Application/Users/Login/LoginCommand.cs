using MediatR;
using OfflineTicketing.Application.Common.Models;
using OfflineTicketing.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Users.Login
{
    public class LoginCommand : IRequest<Result<LoginResponseDto>>
    {
        public string Email { get; set; } 
        public string Password { get; set; }
    }
}
