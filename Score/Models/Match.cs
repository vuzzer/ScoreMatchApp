using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score.Models
{
    public class Match
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string EquipeExterieure { get; set; }
        public string EquipeDomicile { get; set; }
        public int ScoreEquipeExterieure { get; set; }
        public int ScoreEquipeDomicile { get; set; }
        public string DateDuMatch { get; set; }
    }
}
