using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace GeoProject
{
    class DistanceChecking
    {
        public DistanceChecking(NpgsqlConnection conn)
        {
            string is30kmQuery = @"SELECT CASE WHEN EXISTS (
                            SELECT *
	                        FROM punkty as pkt1
	                        INNER JOIN punkty as pkt2 ON pkt1.gid < pkt2.gid 
	                        and ST_Distance_sphere(ST_Force_2D(pkt1.geom), ST_Force_2D(pkt2.geom)) <=30000
                        )
                        THEN CAST(0 AS BIT)
                        ELSE CAST(1 AS BIT) END";

            using (var cmd = new NpgsqlCommand(is30kmQuery, conn))
            {
                bool isDistanceMin30km = (bool)cmd.ExecuteScalar();

                Console.WriteLine("\nChecking the distance between points:");

                if (isDistanceMin30km)
                {
                    Console.WriteLine("Distance between each point is greater than 30 km \n");
                }
                else
                {
                    Console.WriteLine("Distance between some points is less than 30 km \n");
                }
            }
        }
    }
}
