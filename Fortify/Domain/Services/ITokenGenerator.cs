﻿namespace Fortify.Domain.Services;

public interface ITokenGenerator
{
    string GenerateRefreshToken();
    string GenerateEmailConfirmationToken();
    string GeneratePasswordResetToken();
    string Generate2FAToken();
}