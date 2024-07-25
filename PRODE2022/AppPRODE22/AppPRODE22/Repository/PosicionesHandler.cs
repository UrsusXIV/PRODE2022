using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class PosicionesHandler : DBHandler
    {
       public static PosicionesResponse consultaPosicionesHandler(GetPosiciones consultaPosicionesQuery)
        {
            PosicionesResponse response = new PosicionesResponse();

                response.Posiciones = new List<GetPosiciones>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string SelectQuery = @"
                                    SELECT a.ApIDApostador, a.ApIDCompetencia, ap.ApostNombre, SUM(a.ApPuntosObtenidos) AS TotalPuntosObtenidos
                                    FROM 
                                    Apuestas a
                                    INNER JOIN 
                                    Apostadores ap ON a.ApIDApostador = ap.IDApostador
                                    WHERE 
                                    a.ApIDCompetencia = @ApIDCompetencia
                                    GROUP BY
                                    a.ApIDApostador,
                                    a.ApIDCompetencia,
                                    ap.ApostNombre
                                    ORDER BY 
                                    TotalPuntosObtenidos DESC";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDCompetencia", System.Data.SqlDbType.Int) { Value = consultaPosicionesQuery.PosIDCompetencia });

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var posicionesDTO = new GetPosiciones
                                {
                                    PosNombre = sqlDataReader["ApostNombre"].ToString(),

                                    PosPuntosTotales = Convert.ToInt32(sqlDataReader["TotalPuntosObtenidos"])   
                                };
                                response.Posiciones.Add(posicionesDTO);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return response;
        }
    }
}
