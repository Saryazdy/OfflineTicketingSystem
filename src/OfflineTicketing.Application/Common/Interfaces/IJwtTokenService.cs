using OfflineTicketing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Common.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
