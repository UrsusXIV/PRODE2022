using System.Data.SqlClient;
using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class ApuestasHandler : DBHandler
    {
        // Método para registrar una nueva apuesta en la base de datos.
        public static bool altaApuestasHandler(PostApuestasDTO altaApuestasBody)
        {
            bool insert = false; // Indicador del éxito de la operación de inserción.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar una nueva apuesta en la tabla Apuestas.
                var InsertQuery = "INSERT INTO Apuestas (ApIDApostador, ApIDPartido, ApIDCompetencia, ApGolesL, ApGolesV, ApPuntosObtenidos) VALUES (@ApIDApostador, @ApIDPartido, @ApIDCompetencia, @ApGolesL, @ApGolesV, @ApPuntosObtenidos)";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para insertar una nueva apuesta.
                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDApostador", System.Data.SqlDbType.Int) { Value = altaApuestasBody.ApIDApostador });
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDPartido", System.Data.SqlDbType.Int) { Value = altaApuestasBody.ApIDPartido });
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDCompetencia", System.Data.SqlDbType.Int) { Value = altaApuestasBody.ApIDCompetencia });
                    sqlCommand.Parameters.Add(new SqlParameter("ApGolesL", System.Data.SqlDbType.Int) { Value = altaApuestasBody.ApGolesL });
                    sqlCommand.Parameters.Add(new SqlParameter("ApGolesV", System.Data.SqlDbType.Int) { Value = altaApuestasBody.ApGolesV });
                    sqlCommand.Parameters.Add(new SqlParameter("ApPuntosObtenidos", System.Data.SqlDbType.Int) { Value = 0 }); // Inicializa los puntos obtenidos a 0.

                    // Ejecuta la consulta y verifica si se insertó una nueva fila.
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        insert = true; // Si se insertó una fila, cambia el indicador a verdadero.
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
                return insert; // Devuelve verdadero si se realizó la inserción.
            }
        }

        // Método para consultar las apuestas según los criterios de búsqueda proporcionados.
        public static ApuestasResponse consultaApuestasHandler(GetApuestasDTO consultaApuestasQuery)
        {
            ApuestasResponse response = new ApuestasResponse
            {
                // Inicializa la lista de apuestas en la respuesta.
                Apuestas = new List<GetApuestasDTO>()
            };

            // Establece la conexión con la base de datos.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string SelectQuery; // Variable para almacenar la consulta SQL.

                // Construye la consulta SQL según los criterios de búsqueda proporcionados.
                if (consultaApuestasQuery.ApIDApostador != null && consultaApuestasQuery.ApIDCompetencia != null)
                {
                    // Consulta para obtener las apuestas del apostador y la competencia especificados.
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
                else
                {
                    SelectQuery = ""; // En caso de no haber criterios, se asigna una consulta vacía.
                }

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para consultar las apuestas.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDApostador", System.Data.SqlDbType.Int) { Value = consultaApuestasQuery.ApIDApostador });
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDCompetencia", System.Data.SqlDbType.Int) { Value = consultaApuestasQuery.ApIDCompetencia });

                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Verifica si hay resultados.
                        if (sqlDataReader.HasRows)
                        {
                            // Lee cada fila y construye el objeto DTO para cada apuesta.
                            while (sqlDataReader.Read())
                            {
                                var apuestasDTO = new GetApuestasDTO
                                {
                                    ApIDPartido = Convert.ToInt32(sqlDataReader["ApIDPartido"]),
                                    ApIDCompetencia = Convert.ToInt32(sqlDataReader["ApIDCompetencia"]),
                                    ApIDEquipoL = Convert.ToInt32(sqlDataReader["EquipoLocalID"]),
                                    ApIDEquipoV = Convert.ToInt32(sqlDataReader["EquipoVisitanteID"]),
                                    ApGolesL = Convert.ToInt32(sqlDataReader["ApGolesL"]),
                                    ApGolesV = Convert.ToInt32(sqlDataReader["ApGolesV"]),
                                    ApPuntosObtenidos = Convert.ToInt32(sqlDataReader["ApPuntosObtenidos"]),
                                    EquipoLNombre = sqlDataReader["EquipoLocalNombre"].ToString(),
                                    EquipoVNombre = sqlDataReader["EquipoVisitanteNombre"].ToString(),
                                    ApIDApostador = Convert.ToInt32(sqlDataReader["ApIDApostador"]),
                                    PartIDEstado = Convert.ToInt32(sqlDataReader["PartIDEstado"]),
                                    tieneApuesta = true // Indica que se encontró una apuesta para los criterios dados.
                                };

                                // Añade el DTO de la apuesta a la lista de respuesta.
                                if(apuestasDTO.ApGolesL == -1 && apuestasDTO.ApGolesV == -1)
                                {
                                    apuestasDTO.ApGolesL = 0;
                                    apuestasDTO.ApGolesV = 0;

                                    // Enmascara el -1 del POST general inicial, como una primera medida para validar apuestas posteriores.
                                }
                                response.Apuestas.Add(apuestasDTO);
                            }
                        }
                        else
                        {
                            // Si no hay apuestas, se consulta la información de los partidos por competencia.
                            sqlConnection.Close();

                            if (consultaApuestasQuery.ApIDCompetencia != null)
                            {
                                // Consulta para obtener la información de los partidos de la competencia especificada.
                                SelectQuery = @"
                                SELECT PG.IDPartido, PartIDCompetencia, PartIDEquipoL, PartIDEquipoV, PartGolesL, PartGolesV, E1.EquipoNombre AS EquipoLocal, E2.EquipoNombre AS EquipoVisitante, PartIDEstado 
                                FROM PartidosGrupos AS PG 
                                INNER JOIN Equipos AS E1 ON PG.PartIDEquipoL = E1.IDEquipo 
                                INNER JOIN Equipos AS E2 ON PG.PartIDEquipoV = E2.IDEquipo 
                                WHERE PG.PartIDCompetencia = @ApIDCompetencia";
                            }

                            sqlConnection.Open();

                            // Ejecuta la consulta para obtener los partidos de la competencia.
                            using (SqlCommand altSqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                            {
                                altSqlCommand.Parameters.Add(new SqlParameter("ApIDCompetencia", System.Data.SqlDbType.Int) { Value = consultaApuestasQuery.ApIDCompetencia });

                                // Lee los resultados de la consulta y construye el objeto DTO para cada partido.
                                using (SqlDataReader altSqlDataReader = altSqlCommand.ExecuteReader())
                                {
                                    if (altSqlDataReader.HasRows)
                                    {
                                        while (altSqlDataReader.Read())
                                        {
                                            var apuestasDTO = new GetApuestasDTO
                                            {
                                                ApIDPartido = Convert.ToInt32(altSqlDataReader["IDPartido"]),
                                                ApIDCompetencia = Convert.ToInt32(altSqlDataReader["PartIDCompetencia"]),
                                                ApIDEquipoL = Convert.ToInt32(altSqlDataReader["PartIDEquipoL"]),
                                                ApIDEquipoV = Convert.ToInt32(altSqlDataReader["PartIDEquipoV"]),
                                                ApGolesL = Convert.ToInt32(altSqlDataReader["PartGolesL"]),
                                                ApGolesV = Convert.ToInt32(altSqlDataReader["PartGolesV"]),
                                                PartIDEstado = Convert.ToInt32(altSqlDataReader["PartIDEstado"]),
                                                EquipoLNombre = altSqlDataReader["EquipoLocal"].ToString(),
                                                EquipoVNombre = altSqlDataReader["EquipoVisitante"].ToString(),
                                                tieneApuesta = false // Indica que no se encontraron apuestas previas para el partido.
                                            };

                                            // Inicializa los goles si no existen en la tabla de partidos.
                                            if (apuestasDTO.ApGolesL != 0 && apuestasDTO.ApGolesV != 0)
                                            {
                                                apuestasDTO.ApGolesL = 0;
                                                apuestasDTO.ApGolesV = 0;
                                            }

                                            // Añade el DTO del partido a la lista de respuesta.
                                            response.Apuestas.Add(apuestasDTO);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            return response; // Devuelve la respuesta con la lista de apuestas o partidos.
        }

        // Método para modificar una apuesta existente en la base de datos.
        public static bool modificacionApuestasHandler(PutApuestasDTO modificacionApuestasBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false; // Indicador del éxito de la operación de actualización.

                // Consulta SQL para actualizar una apuesta.
                var UpdateQuery = @"UPDATE Apuestas 
                                    SET ApGolesL = @ApGolesL, ApGolesV = @ApGolesV
                                    WHERE ApIDPartido = @ApIDPartido AND ApIDApostador = @ApIDApostador";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para actualizar la apuesta.
                using (SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("ApGolesL", System.Data.SqlDbType.Int) { Value = modificacionApuestasBody.ApGolesL });
                    sqlCommand.Parameters.Add(new SqlParameter("ApGolesV", System.Data.SqlDbType.Int) { Value = modificacionApuestasBody.ApGolesV });
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDPartido", System.Data.SqlDbType.Int) { Value = modificacionApuestasBody.ApIDPartido });
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDApostador", System.Data.SqlDbType.Int) { Value = modificacionApuestasBody.ApIDApostador });

                    // Ejecuta la consulta y verifica si se actualizó alguna fila.
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        update = true; // Si se actualizó alguna fila, cambia el indicador a verdadero.
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
                return update; // Devuelve verdadero si se realizó la actualización.
            }
        }

        // Método para eliminar una apuesta existente de la base de datos.
        public static bool bajaApuestasHandler(DeleteApuestasDTO bajaApuestasDTO)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false; // Indicador del éxito de la operación de eliminación.

                // Consulta SQL para eliminar una apuesta.
                var DeleteQuery = @"DELETE FROM Apuestas
                                    WHERE ApIDPartido = @ApIDPartido AND ApIDApostador = @ApIDApostador";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para eliminar la apuesta.
                using (SqlCommand sqlCommand = new SqlCommand(DeleteQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDApostador", System.Data.SqlDbType.Int) { Value = bajaApuestasDTO.ApIDApostador });
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDPartido", System.Data.SqlDbType.Int) { Value = bajaApuestasDTO.ApIDPartido });

                    // Ejecuta la consulta y verifica si se eliminó alguna fila.
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        delete = true; // Si se eliminó alguna fila, cambia el indicador a verdadero.
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
                return delete; // Devuelve verdadero si se realizó la eliminación.
            }
        }
    }
}
