using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace GeoProject
{
    class Voivodeships
    {
        public Voivodeships(NpgsqlConnection conn)
        {

            string[] voivodeships = new string[16] { "Lódzkie","Wielkopolskie", "Kujawsko-Pomorskie", "Pomorskie",
                                                    "Malopolskie", "Swietokrzyskie", "Dolnoslaskie", "Lubelskie", "Lubuskie", "Mazowieckie",
                                                    "Opolskie", "Podlaskie", "Slaskie", "Podkarpackie",
                                                    "Warminsko-Mazurskie","Zachodniopomorskie" };


            string generateRandomPointsFunction = @"CREATE OR REPLACE FUNCTION generateRandomPoints(int) RETURNS void AS $$
                        INSERT INTO punkty (geom)
                        SELECT ST_SetSRID(ST_Makepoint(ST_X(xy), ST_Y(xy), (random()*2501-2)), 4326)
                        FROM (SELECT (st_dump(ST_generatePoints(geom,$1))).geom as xy
	                          FROM (SELECT geom 
			                        FROM pol_adm0) as als1 ) as als2
                        $$ LANGUAGE sql;";

    
            for (int i = 1; i <= 16; i++)
            {
                Console.WriteLine(i + "\t" + voivodeships[i - 1]);
            }

            Console.WriteLine("\nChoose number: ");
            int num = Convert.ToInt32(Console.ReadLine()) - 1;

            using (var cmd = new NpgsqlCommand("SELECT ST_Y(points), ST_X(points), ST_Z(points) " +
                                               "FROM pointsInVoivodeship(@p) AS points; ", conn))
            {
                cmd.Parameters.AddWithValue("p", voivodeships[num]);
                NpgsqlDataReader dr = cmd.ExecuteReader();

          
                    Console.WriteLine("Points located in {0}\n", voivodeships[num]);
                    while (dr.Read())
                        Console.WriteLine("lat: {0}\tlong: {1}\tZ: {2}", dr[0], dr[1], dr[2]);


            }
        }

    }
}
