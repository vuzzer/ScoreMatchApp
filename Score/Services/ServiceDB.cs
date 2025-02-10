using Score.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score.Services
{
    public static class ServiceDB
    {
        public static SQLiteConnection ConnexionBD { get; set; }
        private const string NOM_BD = "ScoreSqlite.db3";

        public static void ConfigurerBD()
        {
            var cheminBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), NOM_BD);

            ConnexionBD = new SQLiteConnection(cheminBD);

            ConnexionBD.CreateTable<Equipe>();
            ConnexionBD.CreateTable<Match>();
        }

    }
}
