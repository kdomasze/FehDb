using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Resource
{
    public class BaseResource
    {
        [Key]
        public int ID { get; set; }
    }
}
