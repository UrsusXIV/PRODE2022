using System.Data.SqlClient;
using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Models;
using System.Collections.Generic;

namespace AppPRODE22.Repository
{
    public class PartidosGrupoHandler : DBHandler
    {
        // Método para dar de alta un nuevo partido de grupo en la base de datos.
        public static bool altaPartidosGrupoHandler(PostPartidosGruposDTO altaPartidosGrupoBody)
        {
            int IDPartidoMax = ObtenerMaxIDPartido(); // Obtiene el ID máximo actual de los partidos.

            bool insert = false; // Indicador del éxito de la operación de inserción.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar un nuevo partido de grupo.
                var InsertQuery = "INSERT INTO PartidosGrupos (IDPartido, PartIDCompetencia, PartGrupo, PartIDEquipoL, PartIDEquipoV, PartIDSede, PartFecha, PartHora, PartIDEstado, PartGolesL, PartGolesV, PartPuntosL, PartPuntosV) Values (@IDPartido, @PartIDCompetencia, @PartGrupo, @PartIDEquipoL, @PartIDEquipoV, @PartIDSede, @PartFecha, @PartHora, @PartIDEstado, @PartGolesL, @PartGolesV, @PartPuntosL, @PartPuntosV)";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para insertar el partido de grupo.
                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDPartido", System.Data.SqlDbType.Int) { Value = IDPartidoMax + 1 });
                    sqlCommand.Parameters.Add(new SqlParameter("PartIDCompetencia", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDCompetencia });
                    sqlCommand.Parameters.Add(new SqlParameter("PartGrupo", System.Data.SqlDbType.VarChar) { Value = altaPartidosGrupoBody.PartGrupo });
                    sqlCommand.Parameters.Add(new SqlParameter("PartIDEquipoL", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDEquipoL });
                    sqlCommand.Parameters.Add(new SqlParameter("PartIDEquipoV", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDEquipoV });
                    sqlCommand.Parameters.Add(new SqlParameter("PartIDSede", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDSede });
                    sqlCommand.Parameters.Add(new SqlParameter("PartFecha", System.Data.SqlDbType.Date) { Value = altaPartidosGrupoBody.PartFechaDate });
                    sqlCommand.Parameters.Add(new SqlParameter("PartHora", System.Data.SqlDbType.Time) { Value = altaPartidosGrupoBody.PartHoraTime });
                    sqlCommand.Parameters.Add(new SqlParameter("PartIDEstado", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDEstado });
                    sqlCommand.Parameters.Add(new SqlParameter("PartGolesL", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartGolesL });
                    sqlCommand.Parameters.Add(new SqlParameter("PartGolesV", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartGolesV });
                    sqlCommand.Parameters.Add(new SqlParameter("PartPuntosL", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartPuntosL });
                    sqlCommand.Parameters.Add(new SqlParameter("PartPuntosV", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartPuntosV });

                    // Ejecuta la consulta y obtiene el número de filas afectadas.
                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    // Si se insertó al menos una fila, marca la inserción como exitosa.
                    if (numberOfRows > 0)
                    {
                        insert = true;
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();

                // Devuelve true si la inserción fue exitosa, false de lo contrario.
                return insert;
            }
        }

        // Método para obtener el ID máximo actual de los partidos de grupo.
        public static int ObtenerMaxIDPartido()
        {
            int IDPartidoMax = 0; // Valor máximo actual de los IDPartido.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener el valor máximo de IDPartido en la tabla PartidosGrupos.
                var SelectQuery = "SELECT MAX(IDPartido) AS ValorMax FROM PartidosGrupos";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para obtener el máximo IDPartido.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Verifica si hay filas devueltas por la consulta.
                        if (sqlDataReader.Read())
                        {
                            // Verifica si el valor máximo no es nulo en la base de datos.
                            if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ValorMax")))
                            {
                                IDPartidoMax = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("ValorMax"));
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            // Devuelve el ID máximo actual de los partidos.
            return IDPartidoMax;
        }

        // Método para consultar partidos de grupo según los parámetros especificados.
        public static PartidosGruposResponse consultaPartidosGrupoHandler(GetPartidosGruposDTO consultaPartidosGrupoBody)
        {
            PartidosGruposResponse response = new PartidosGruposResponse(); // Respuesta que contendrá la lista de partidos de grupo encontrados.
            response.PartidosGrupos = new List<GetPartidosGruposDTO>(); // Inicializa la lista de partidos de grupo.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                // Determina la consulta SQL a ejecutar según los parámetros proporcionados.
                if (consultaPartidosGrupoBody.PartIDCompetencia != 0 && consultaPartidosGrupoBody.PartGrupo == "getall")
                {
                    SelectQuery = "SELECT PG.*, E1.EquipoNombre AS EquipoLocal, E2.EquipoNombre AS EquipoVisitante FROM PartidosGrupos AS PG INNER JOIN Equipos AS E1 ON PG.PartIDEquipoL = E1.IDEquipo INNER JOIN Equipos AS E2 ON PG.PartIDEquipoV = E2.IDEquipo WHERE PG.PartIDCompetencia = @PartIDCompetencia";
                }
                else if (consultaPartidosGrupoBody.PartIDCompetencia != 0 && !string.IsNullOrEmpty(consultaPartidosGrupoBody.PartGrupo) && consultaPartidosGrupoBody.PartGrupo != "getall")
                {
                    SelectQuery = "SELECT * FROM PartidosGrupos WHERE PartIDCompetencia = @PartIDCompetencia AND PartGrupo = @PartGrupo";
                }
                else if (consultaPartidosGrupoBody.PartIDPartido != 0)
                {
                    SelectQuery = "SELECT * FROM PartidosGrupos WHERE IDPartido = @IDPartido";
                }

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para la consulta de partidos de grupo.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDPartido", System.Data.SqlDbType.Int) { Value = consultaPartidosGrupoBody.PartIDPartido });
                    sqlCommand.Parameters.Add(new SqlParameter("PartIDCompetencia", System.Data.SqlDbType.Int) { Value = consultaPartidosGrupoBody.PartIDCompetencia });
                    sqlCommand.Parameters.Add(new SqlParameter("PartGrupo", System.Data.SqlDbType.VarChar) { Value = consultaPartidosGrupoBody.PartGrupo });

                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Verifica si hay filas devueltas por la consulta.
                        if (sqlDataReader.HasRows)
                        {
                            // Itera sobre cada fila devuelta y construye objetos DTO para cada partido de grupo.
                            while (sqlDataReader.Read())
                            {
                                var partidosgruposDTO = new GetPartidosGruposDTO
                                {
                                    PartIDEstado = Convert.ToInt32(sqlDataReader["PartIDEstado"]),
                                    PartIDCompetencia = Convert.ToInt32(sqlDataReader["PartIDCompetencia"]),
                                    PartIDPartido = Convert.ToInt32(sqlDataReader["IDPartido"]),
                                    PartIDSede = Convert.ToInt32(sqlDataReader["PartIDSede"]),
                                    PartIDEquipoL = Convert.ToInt32(sqlDataReader["PartIDEquipoL"]),
                                    PartIDEquipoV = Convert.ToInt32(sqlDataReader["PartIDEquipoV"]),
                                    PartGolesL = Convert.ToInt32(sqlDataReader["PartGolesL"]),
                                    PartGolesV = Convert.ToInt32(sqlDataReader["PartGolesV"]),
                                    PartGrupo = sqlDataReader["PartGrupo"].ToString(),
                                    EquipoLNombre = sqlDataReader["EquipoLocal"].ToString(),
                                    EquipoVNombre = sqlDataReader["EquipoVisitante"].ToString(),
                                    PartHoraTime = sqlDataReader["PartHora"].ToString(),
                                    PartFechaDate = sqlDataReader["PartFecha"].ToString()
                                };

                                // Agrega el objeto DTO a la lista de partidos de grupo en la respuesta.
                                response.PartidosGrupos.Add(partidosgruposDTO);
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            // Devuelve la respuesta que contiene la lista de partidos de grupo encontrados.
            return response;
        }

        // Método para modificar los detalles de un partido de grupo existente.
        public static bool modificacionPartidosGrupoHandler(PutPartidosGruposDTO modificacionPartidosGruposBody)
        {
            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false; // Indicador del éxito de la operación de actualización.

                // Consulta SQL para actualizar los detalles de un partido de grupo.
                var UpdateQuery = "UPDATE PartidosGrupos SET PartIDEstado = @PartIDEstado, PartGolesL = @PartGolesL, PartGolesV = @PartGolesV, PartPuntosL = @PartPuntosL, PartPuntosV = @PartPuntosV WHERE IDPartido = @IDPartido";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para actualizar el partido de grupo.
                using (SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {
                    int tempPartPuntosL = 0;
                    int tempPartPuntosV = 0;

                    if(modificacionPartidosGruposBody.PartGolesL > modificacionPartidosGruposBody.PartGolesV)
                    {
                        tempPartPuntosL = 3;
                        tempPartPuntosV = 0;
                    }
                    if(modificacionPartidosGruposBody.PartGolesV > modificacionPartidosGruposBody.PartGolesL)
                    {
                        tempPartPuntosL = 0;
                        tempPartPuntosV = 3;
                    }
                    if(modificacionPartidosGruposBody.PartGolesL == modificacionPartidosGruposBody.PartGolesV)
                    {
                        tempPartPuntosL = 1;
                        tempPartPuntosV = 1;
                    }

                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("PartIDEstado", System.Data.SqlDbType.Int) { Value = modificacionPartidosGruposBody.PartIDEstado });
                    sqlCommand.Parameters.Add(new SqlParameter("PartGolesL", System.Data.SqlDbType.Int) { Value = modificacionPartidosGruposBody.PartGolesL });
                    sqlCommand.Parameters.Add(new SqlParameter("PartGolesV", System.Data.SqlDbType.Int) { Value = modificacionPartidosGruposBody.PartGolesV });
                    sqlCommand.Parameters.Add(new SqlParameter("PartPuntosL", System.Data.SqlDbType.Int) { Value = tempPartPuntosL });
                    sqlCommand.Parameters.Add(new SqlParameter("PartPuntosV", System.Data.SqlDbType.Int) { Value = tempPartPuntosV });
                    sqlCommand.Parameters.Add(new SqlParameter("IDPartido", System.Data.SqlDbType.Int) { Value = modificacionPartidosGruposBody.PartIDPartido });

                    // Ejecuta la consulta y obtiene el número de filas afectadas.
                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    // Si se actualizó al menos una fila, marca la actualización como exitosa.
                    if (numberOfRows > 0)
                    {
                        update = true;
                    }
                }
                // Devuelve true si la actualización fue exitosa, false de lo contrario.

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();

              

                if(modificacionPartidosGruposBody.PartIDEstado == 3)
                {
                    traerYActualizar(modificacionPartidosGruposBody.PartIDPartido, modificacionPartidosGruposBody.PartGolesL, modificacionPartidosGruposBody.PartGolesV);
                }

                return update;

            }
        }

        public static void traerYActualizar(int partidoSeleccionado, int golesLocal, int golesVisitante) // Rutina de la actualizacion de la tabla de apuestas, en caso de que el partido llegue finalizado.
        {
            ApuestasResponse response = new ApuestasResponse();
            response.Apuestas = new List<GetApuestasDTO>();

            using (SqlConnection sqlConnectionInternalGet = new SqlConnection(connectionString))
            {
                var routineQueryGet = "SELECT * FROM Apuestas WHERE ApIDPartido = @ApIDPartido";

                sqlConnectionInternalGet.Open();
                
                using (SqlCommand sqlCommand = new SqlCommand(routineQueryGet, sqlConnectionInternalGet))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("ApIDPartido", System.Data.SqlDbType.Int) { Value = partidoSeleccionado });

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                 var apuestasRecuperadas = new GetApuestasDTO
                                {
                                    ApIDPartido = Convert.ToInt32(sqlDataReader["ApIDPartido"]),
                                    ApIDApostador = Convert.ToInt32(sqlDataReader["ApIDApostador"]),
                                    ApGolesL = Convert.ToInt32(sqlDataReader["ApGolesL"]),
                                    ApGolesV = Convert.ToInt32(sqlDataReader["ApGolesV"]),
                                    ApPuntosObtenidos = Convert.ToInt32(sqlDataReader["ApPuntosObtenidos"]),
                                };

                                response.Apuestas.Add(apuestasRecuperadas);
                            }
                        }
                    }
                }
             sqlConnectionInternalGet.Close();
            }

            using(SqlConnection sqlConnectionInternalPut = new SqlConnection(connectionString))
            {
                string resultadoPartido = string.Empty;

                var routineQueryUpdate = "UPDATE Apuestas SET ApPuntosObtenidos = @ApPuntosObtenidos WHERE ApIDPartido = @ApIDPartido AND ApIDApostador = @ApIDApostador";

                sqlConnectionInternalPut.Open();
                
                if(golesLocal > golesVisitante)
                {
                     resultadoPartido = "L";
                }
                
                else if(golesVisitante > golesLocal)
                {
                     resultadoPartido = "V";
                }

                else
                {
                    resultadoPartido = "E";   
                }

                foreach(var apuestas in response.Apuestas)
                {

                    using(SqlCommand sqlCommand = new SqlCommand(routineQueryUpdate, sqlConnectionInternalPut))
                    {
                        int puntosObtenidos = 0;

                        if (golesLocal == apuestas.ApGolesL && apuestas.ApGolesV == golesVisitante)
                        {
                            puntosObtenidos = 5;
                        }

                        else if (apuestas.ApGolesL > apuestas.ApGolesV && resultadoPartido == "L")
                        {
                            puntosObtenidos = 3;
                        }

                        else if (apuestas.ApGolesV > apuestas.ApGolesL && resultadoPartido == "V")
                        {
                            puntosObtenidos = 3;
                        }

                        else if(apuestas.ApGolesL == apuestas.ApGolesV && resultadoPartido == "E")
                        {
                            puntosObtenidos = 3;
                        }
                        sqlCommand.Parameters.Add(new SqlParameter("ApIDPartido", System.Data.SqlDbType.Int) { Value = apuestas.ApIDPartido });
                        sqlCommand.Parameters.Add(new SqlParameter("ApIDApostador", System.Data.SqlDbType.Int) { Value = apuestas.ApIDApostador });
                        sqlCommand.Parameters.Add(new SqlParameter("ApPuntosObtenidos", System.Data.SqlDbType.Int) { Value = puntosObtenidos });

                        sqlCommand.ExecuteNonQuery();
                    }

                    sqlConnectionInternalPut.Close();
                }
            }

        }
        


        // Método para eliminar un partido de grupo de la base de datos.
        public static bool bajaPartidosGrupoHandler(DeletePartidosGruposDTO bajaPartidosGrupoBody)
        {
            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false; // Indicador del éxito de la operación de eliminación.

                // Consulta SQL para eliminar un partido de grupo.
                var DeleteString = "DELETE FROM PartidosGrupos WHERE IDPartido = @IDPartido";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para eliminar el partido de grupo.
                using (SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDPartido", System.Data.SqlDbType.Int) { Value = bajaPartidosGrupoBody.PartIDPartido });

                    // Ejecuta la consulta y obtiene el número de filas afectadas.
                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    // Si se eliminó al menos una fila, marca la eliminación como exitosa.
                    if (numberOfRows > 0)
                    {
                        delete = true;
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();

                // Devuelve true si la eliminación fue exitosa, false de lo contrario.
                return delete;
            }
        }
    }
}
