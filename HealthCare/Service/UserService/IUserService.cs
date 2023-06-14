using HealthCare.Model;
using System;
using System.Collections.Generic;

namespace HealthCare.Service.UserService
{
    public interface IUserService
    {
        List<User> GetAllUsers();
    }
}
