using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Models
{
    public class Exame
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Paper { get; set; }
        public int Marks { get; set; }
    }
}
