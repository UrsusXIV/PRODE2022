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

                if (consultaApuestasQuery.ApIDApostador != null && consultaApuestasQuery.ApIDCompetencia != null ) 
                { // Se parte de la premisa que puede haber apuestas para el Apostador y Competencia
                    SelectQuery = @"
                    SELECT Apuestas.*, 
                    PartidosGrupos.PartIDEquipoL AS EquipoLocalID, Equipos.EquipoNombre AS EquipoLocalNombre, 
                    PartidosGrupos.PartIDEquipoV AS EquipoVisitanteID, Equipos_1.EquipoNombre AS EquipoVisitanteNombre,
                    PartidosGrupos.PartIDEstado
                    FROM Apuestas
                    INNER JOIN PartidosGrupos ON Apuestas.ApIDPartido = PartidosGrupos.IDPartido
                    INNER JOIN Equipos ON PartidosGrupos.PartIDEquipoL = Equipos.IDEquipo
                    INNER JOIN Equipos AS Equipos_1 ON PartidosGrupos.PartIDEquipoV = Equipos_1.IDEquipo
                    WHERE Apuestas.ApIDApostador = @ApIDApostador AND Apuestas.ApIDCompetencia = @ApIDCompetencia";

                } 

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDApostador", System.Data.SqlDbType.Int) { Value = consultaApuestasQuery.ApIDApostador });
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDCompetencia", System.Data.SqlDbType.Int) { Value = consultaApuestasQuery.ApIDCompetencia });
                
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)      // Si vuelven registros, significa que ya hay una apuesta para el Apostador-Competencia
                        {

                            while (sqlDataReader.Read())
                            {
                                var apuestasDTO = new GetApuestasDTO();

                                apuestasDTO.ApIDPartido = Convert.ToInt32(sqlDataReader["ApIDPartido"]);

                                apuestasDTO.ApIDCompetencia = Convert.ToInt32(sqlDataReader["ApIDCompetencia"]);

                                apuestasDTO.ApIDEquipoL = Convert.ToInt32(sqlDataReader["EquipoLocalID"]);

                                apuestasDTO.ApIDEquipoV = Convert.ToInt32(sqlDataReader["EquipoVisitanteID"]);

                                apuestasDTO.ApGolesL = Convert.ToInt32(sqlDataReader["ApGolesL"]);

                                apuestasDTO.ApGolesV = Convert.ToInt32(sqlDataReader["ApGolesV"]);

                                apuestasDTO.ApPuntosObtenidos = Convert.ToInt32(sqlDataReader["ApPuntosObtenidos"]);

                                apuestasDTO.EquipoLNombre = sqlDataReader["EquipoLocalNombre"].ToString();

                                apuestasDTO.EquipoVNombre = sqlDataReader["EquipoVisitanteNombre"].ToString();

                                apuestasDTO.ApIDApostador = Convert.ToInt32(sqlDataReader["ApIDApostador"]);

                                apuestasDTO.PartIDEstado = Convert.ToInt32(sqlDataReader["PartIDEstado"]);

                                apuestasDTO.tieneApuesta = true;   // Definimos un flag para que el front sepa que SI Tenia Apuestas

                                response.Apuestas.Add(apuestasDTO);
                            }
                        }
                        else  // Si la consulta sobre apuestas volvió vacía, entonces se completa la respuesta con los partidos x competencia
                        {
                            sqlConnection.Close();
                            
                            if(consultaApuestasQuery.ApIDCompetencia != null)
                            {
                                SelectQuery = @"SELECT PG.IDPartido, PartIDCompetencia,PartIDEquipoL, PartIDEquipoV, PartGolesL, PartGolesV, E1.EquipoNombre AS EquipoLocal,E2.EquipoNombre AS EquipoVisitante, PartIDEstado 
                                            FROM PartidosGrupos AS PG 
                                            INNER JOIN Equipos AS E1 ON PG.PartIDEquipoL = E1.IDEquipo 
                                            INNER JOIN Equipos AS E2 ON PG.PartIDEquipoV = E2.IDEquipo 
                                            WHERE PG.PartIDCompetencia = @ApIDCompetencia";
                            }

                            sqlConnection.Open();

                            using (SqlCommand altSqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                            {
                                altSqlCommand.Parameters.Add(new SqlParameter("ApIDCompetencia", System.Data.SqlDbType.Int) { Value = consultaApuestasQuery.ApIDCompetencia });

                                using (SqlDataReader altSqlDataReader = altSqlCommand.ExecuteReader())
                                {
                                    if (altSqlDataReader.HasRows)

                                    // Preparar la sentencia del INSERT INTO Apuestas ... antes de entrar en el while
                                    //

                                    {
                                        while (altSqlDataReader.Read())
                                        {
                                            var apuestasDTO = new GetApuestasDTO();

                                            apuestasDTO.ApIDPartido = Convert.ToInt32(altSqlDataReader["IDPartido"]);

                                            apuestasDTO.ApIDCompetencia = Convert.ToInt32(altSqlDataReader["PartIDCompetencia"]);

                                            apuestasDTO.ApIDEquipoL = Convert.ToInt32(altSqlDataReader["PartIDEquipoL"]);

                                            apuestasDTO.ApIDEquipoV = Convert.ToInt32(altSqlDataReader["PartIDEquipoV"]);

                                            apuestasDTO.ApGolesL = Convert.ToInt32(altSqlDataReader["PartGolesL"]);

                                            apuestasDTO.ApGolesV = Convert.ToInt32(altSqlDataReader["PartGolesV"]);

                                            apuestasDTO.PartIDEstado = Convert.ToInt32(altSqlDataReader["PartIDEstado"]);

                                            if (apuestasDTO.ApGolesL != 0 && apuestasDTO.ApGolesV != 0)
                                            {
                                                apuestasDTO.ApGolesL = 0;  // Se inicializa porque no existe en la tabla de PartidosGrupo
                                                apuestasDTO.ApGolesV = 0;
                                            }

                                            apuestasDTO.EquipoLNombre = altSqlDataReader["EquipoLocal"].ToString();

                                            apuestasDTO.EquipoVNombre = altSqlDataReader["EquipoVisitante"].ToString();

                                            apuestasDTO.tieneApuesta = false;  // Definimos un flag para que el front sepa que No Tenia Apuestas

                                            response.Apuestas.Add(apuestasDTO);

                                            // Hacer el using del insert. Las variables se alimentan con apuestasDTO.xxxxx 
                                            //

                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                sqlConnection.Close();
            }
            return response;
        }

        public static bool modificacionApuestasHandler(PutApuestasDTO modificacionApuestasBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false;

                var UpdateQuery = @"UPDATE Apuestas 
                                    SET ApGolesL = @ApGolesL, ApGolesV = @ApGolesV
                                    WHERE ApIDPartido = @ApIDPartido AND ApIDApostador = @ApIDApostador";
                
                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("ApGolesL", System.Data.SqlDbType.Int) { Value = modificacionApuestasBody.ApGolesL });
                    sqlCommand.Parameters.Add(new SqlParameter("ApGolesV", System.Data.SqlDbType.Int) { Value = modificacionApuestasBody.ApGolesV });
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDPartido", System.Data.SqlDbType.Int) { Value = modificacionApuestasBody.ApIDPartido });
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDApostador", System.Data.SqlDbType.Int) { Value = modificacionApuestasBody.ApIDApostador });

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if(numberOfRows > 0)
                    {
                        update = true;
                    }
                }

                sqlConnection.Close();
                return update;
            }
        }

        public static bool bajaApuestasHandler(DeleteApuestasDTO bajaApuestasDTO)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false;

                var DeleteQuery = @"DELETE FROM Apuestas
                                    WHERE ApIDPartido = @ApIDPartido AND ApIDApostador = ApIDApostador";

                sqlConnection.Open();                

                using(SqlCommand sqlCommand = new SqlCommand(DeleteQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDApostador", System.Data.SqlDbType.Int) { Value = bajaApuestasDTO.ApIDApostador });
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDPartido", System.Data.SqlDbType.Int) { Value = bajaApuestasDTO.ApIDPartido });
                    
                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        delete = true;
                    }
                }
                sqlConnection.Close();

                return delete;
            }

        }

    }
}
