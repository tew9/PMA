using Accelerator.API.Shared.Models;
using Accelerator.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.API.Shared.Mappers
{
    public class UserMapper : IMapper<UserDTO, Users>
    {
        public UserDTO Map(Users from) => new UserDTO()
            {
                Email = from.Email,
                FirstName = from.FirstName,
                LastName = from.LastName,
                Phone = from.Phone,
                JobTitle = from.JobTitle,
            };

        public Users Map(UserDTO from) => new Users()
        {
            UserId = from.UserId,
            Email = from.Email,
            FirstName = from.FirstName,
            LastName = from.LastName,
            Phone = from.Phone,
            JobTitle = from.JobTitle,

        };
    }
}
