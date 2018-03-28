using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.Web.Models
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
