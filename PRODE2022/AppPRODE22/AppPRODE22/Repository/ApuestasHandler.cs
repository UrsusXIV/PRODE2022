using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;


namespace AppPRODE22.Repository
{
    public class ApuestasHandler : DBHandler
    {
        public static bool altaApuestasHandler(PostApuestasDTO altaApuestasBody)
        {
            bool insert = false;
            
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var InsertQuery = "INSERT INTO Apuestas (ApIDApostador, ApIDPartido, ApIDCompetencia, ApGolesL, ApGolesV, ApPuntosObtenidos) Values (@ApIDApostador, @ApIDPartido, @ApIDCompetencia, @ApGolesL, @ApGolesV, @ApPuntosObtenidos)";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDApostador", System.Data.SqlDbType.Int) { Value = altaApuestasBody.ApIDApostador });

                    sqlCommand.Parameters.Add(new SqlParameter("ApIDPartido", System.Data.SqlDbType.Int) { Value = altaApuestasBody.ApIDPartido });

                    sqlCommand.Parameters.Add(new SqlParameter("ApIDCompetencia", System.Data.SqlDbType.Int) { Value = altaApuestasBody.ApIDCompetencia });

                    sqlCommand.Parameters.Add(new SqlParameter("ApGolesL", System.Data.SqlDbType.Int) { Value = altaApuestasBody.ApGolesL });

                    sqlCommand.Parameters.Add(new SqlParameter("ApGolesV", System.Data.SqlDbType.Int) { Value = altaApuestasBody.ApGolesV });

                    sqlCommand.Parameters.Add(new SqlParameter("ApPuntosObtenidos", System.Data.SqlDbType.Int) { Value = 0 }); // Es 0 porque todas las apuestas inicialmente acumulan 0 puntos.

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        insert = true;
                    }
                }

                sqlConnection.Close();

                return insert;
            }
        }

        public static ApuestasResponse consultaApuestasHandler(GetApuestasDTO consultaApuestasQuery)
        {
            ApuestasResponse response = new ApuestasResponse();

            response.Apuestas = new List<GetApuestasDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if (consultaApuestasQuery.PartIDCompetencia == 0)
                {
                    SelectQuery = "";

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("PartIDCompetencia", System.Data.SqlDbType.Int) { Value = consultaApuestasQuery.PartIDCompetencia });
                
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var apuestasDTO = new GetApuestasDTO();

                                apuestasDTO.PartIDPartido = Convert.ToInt32(sqlDataReader["PartIDPartido"]);

                                apuestasDTO.PartIDCompetencia = Convert.ToInt32(sqlDataReader["PartIDCompetencia"]);

                                apuestasDTO.PartIDEquipoL = Convert.ToInt32(sqlDataReader["PartIDEquipoL"]);

                                apuestasDTO.PartIDEquipoV = Convert.ToInt32(sqlDataReader["PartIDEquipoV"]);

                                apuestasDTO.ApGolesL = Convert.ToInt32(sqlDataReader["ApGolesL"]);

                                apuestasDTO.ApGolesV = Convert.ToInt32(sqlDataReader["ApGolesV"]);

                                apuestasDTO.ApPuntosObtenidos = Convert.ToInt32(sqlDataReader["ApPuntosObtenidos"]);

                                apuestasDTO.EquipoLNombre = sqlDataReader["EquipoLocal"].ToString();

                                apuestasDTO.EquipoVNombre = sqlDataReader["EquipoVisitante"].ToString();

                                response.Apuestas.Add(apuestasDTO);
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
