using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Authentication
{
    public interface ITokenProvider
    {
        void GenerateJwtToken(ApplicationUser user);
        (string token, DateTime expiresOn) GenerateRefreshToken();
    }
}
