using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Resource.UserModel
{
    public class UserResource : BaseResource
    {
        /// <summary>
        /// User username
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
