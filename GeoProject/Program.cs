using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace GeoProject
{
    class Program
    {
        static void Main(string[] args)
        {

            var connString = "Server=127.0.0.1; User Id=postgres; Password=Kurowa; Database=PointsDB;";
            NpgsqlConnection conn = new NpgsqlConnection(connString);

            conn.Open();


            using (var cmd = new NpgsqlCommand(RandomPoints.generateRandomPointsFunction, conn)) { cmd.ExecuteNonQuery(); }
            using (var cmd = new NpgsqlCommand(RandomPoints.pointsInVoivodeshipFunction, conn)) { cmd.ExecuteNonQuery(); }
            

            RandomPoints rp = new RandomPoints(conn);
            DistanceChecking ds = new DistanceChecking(conn);
            Voivodeships vs = new Voivodeships(conn);



            conn.Close();
            Console.ReadKey();
        }
    }
}
