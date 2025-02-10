using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score.DTO
{
    public class EquipeDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Nom { get; set; }
        public int NombreDeMatchJoue { get; set; }
        public int ButMarque { get; set; }
        public int ButRecu { get; set; }
        public int NombreDePoints { get; set; }
    }
}
