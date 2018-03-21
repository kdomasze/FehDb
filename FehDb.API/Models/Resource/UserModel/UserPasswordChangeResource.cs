using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Resource.UserModel
{
    public class UserPasswordChangeResource : UserResource
    {
        /// <summary>
        /// New password for user
        /// </summary>
        [Required]
        public string NewPassword { get; set; }
    }
}
