﻿using WPSUR.Repository.EntityEnums;

namespace WPSUR.WebApi.Models.Account.Request
{
    public sealed class RegisterRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
