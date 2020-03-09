using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace GeoProject
{
    class RandomPoints
    {
        internal static string generateRandomPointsFunction;
        internal static string pointsInVoivodeshipFunction;

        public RandomPoints(NpgsqlConnection conn)
        {

            string createPointsTable = @"CREATE TABLE punkty
                (
                    gid serial NOT NULL,
                    CONSTRAINT punkty_pkey PRIMARY KEY (gid)
                );
                SELECT AddGeometryColumn('punkty', 'geom', 4326, 'POINTZ',3);";

  
            try
            {
                using (var cmd = new NpgsqlCommand(createPointsTable, conn)) { cmd.ExecuteNonQuery(); }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Table 'punkty' already exist");
            }


           Console.WriteLine("100 points genereted");
    
           
            using (var cmd = new NpgsqlCommand("SELECT generateRandomPoints(100)", conn))
            {
                cmd.ExecuteNonQuery();
            }

            
        }

    }
}
