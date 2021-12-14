using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appv1.DAL.Models
{
    public class SlownikStudentZajecia
    {
        public int IdStudenta { get; set; }
        public int IdZajecia { get; set; }


        public SlownikStudentZajecia(int idZajecia, int idStudenta)
        {
            IdStudenta = idStudenta;
            IdZajecia = idZajecia;
        }
    }
}