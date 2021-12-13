using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appv1.DAL.Models
{
    public class Zajecia
    {
        public int ID { get; set; }
        public string NazwaZajec { get; set; }
        public DateTime TerminZajec { get; set; }
    }
}
