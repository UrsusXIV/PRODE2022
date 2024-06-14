using System.Data.SqlClient; 
using AppPRODE22.Controllers.DTOs; 
using AppPRODE22.Models; 

namespace AppPRODE22.Repository
{
    public class EquiposXCompetenciaHandler : DBHandler
    {
        // Método para dar de alta la relación entre equipos y competencias.
        public static bool altaEqpsXCompHandler(PostEquiposXCompetenciaDTO altaEqpsXCompBody)
        {
            bool insert = false; // Indicador del éxito de la operación de inserción.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar una nueva relación EquiposXCompetencia.
                var InsertQuery = "INSERT INTO EquiposXCompetencia (IDEquipo, IDCompetencia) VALUES (@IDEquipo, @IDCompetencia)";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para insertar la relación.
                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = altaEqpsXCompBody.IDEquipo });
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = altaEqpsXCompBody.IDCompetencia });

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

        // Método para consultar las relaciones EquiposXCompetencia según los criterios de búsqueda proporcionados.
        public static EquiposXCompetenciaResponse consultaEquiposXCompetenciaHandler(GetEquiposXCompetenciaDTO consultaEquiposXCompetenciaBody)
        {
            EquiposXCompetenciaResponse response = new EquiposXCompetenciaResponse();
            response.EquiposXCompetencia = new List<GetEquiposXCompetenciaDTO>(); // Inicializa la lista de relaciones.

            // Establece la conexión con la base de datos.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string SelectQuery = string.Empty; // Variable para almacenar la consulta SQL.

                // Construye la consulta SQL según los criterios de búsqueda proporcionados.
                if (consultaEquiposXCompetenciaBody.IDEquipo == 0 && consultaEquiposXCompetenciaBody.IDCompetencia == 0)
                {
                    // Consulta para obtener todas las relaciones EquiposXCompetencia con nombres de equipos y competencias.
                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre " +
                                  "FROM EquiposXCompetencia " +
                                  "INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia " +
                                  "INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo";
                }
                else if (consultaEquiposXCompetenciaBody.IDEquipo != 0 && consultaEquiposXCompetenciaBody.IDCompetencia != 0)
                {
                    // Consulta para obtener una relación específica por ID de equipo y competencia.
                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre " +
                                  "FROM EquiposXCompetencia " +
                                  "INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia " +
                                  "INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo " +
                                  "WHERE EquiposXCompetencia.IDEquipo = @IDEquipo AND EquiposXCompetencia.IDCompetencia = @IDCompetencia";
                }
                else if (consultaEquiposXCompetenciaBody.IDEquipo != 0 && consultaEquiposXCompetenciaBody.IDCompetencia == 0)
                {
                    // Consulta para obtener todas las competencias de un equipo específico.
                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre " +
                                  "FROM EquiposXCompetencia " +
                                  "INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia " +
                                  "INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo " +
                                  "WHERE EquiposXCompetencia.IDEquipo = @IDEquipo";
                }
                else if (consultaEquiposXCompetenciaBody.IDEquipo == 0 && consultaEquiposXCompetenciaBody.IDCompetencia != 0)
                {
                    // Consulta para obtener todos los equipos de una competencia específica.
                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre " +
                                  "FROM EquiposXCompetencia " +
                                  "INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia " +
                                  "INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo " +
                                  "WHERE EquiposXCompetencia.IDCompetencia = @IDCompetencia";
                }

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para consultar las relaciones EquiposXCompetencia.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = consultaEquiposXCompetenciaBody.IDEquipo });
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = consultaEquiposXCompetenciaBody.IDCompetencia });

                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Verifica si hay resultados.
                        if (sqlDataReader.HasRows)
                        {
                            // Lee cada fila y construye el objeto DTO para cada relación EquiposXCompetencia.
                            while (sqlDataReader.Read())
                            {
                                var eqpsxcompDTO = new GetEquiposXCompetenciaDTO
                                {
                                    IDEquipo = Convert.ToInt32(sqlDataReader["IDEquipo"]),
                                    IDCompetencia = Convert.ToInt32(sqlDataReader["IDCompetencia"]),
                                    EquipoNombre = sqlDataReader["EquipoNombre"].ToString(),
                                    NombreCompetencia = sqlDataReader["CompetenciaNombre"].ToString()
                                };

                                // Añade el DTO de la relación a la lista de respuesta.
                                response.EquiposXCompetencia.Add(eqpsxcompDTO);
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            return response; // Devuelve la respuesta con la lista de relaciones EquiposXCompetencia.
        }

        // Método para modificar la relación EquiposXCompetencia.
        public static bool modificacionEqpsXCompHandler(PutEquiposXCompetenciaDTO modificacionEqpsXCompBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false; // Indicador del éxito de la operación de actualización.

                // Consulta SQL para actualizar la relación EquiposXCompetencia.
                var UpdateQuery = "UPDATE EquiposXCompetencia SET IDCompetencia = @IDCompetencia WHERE IDEquipo = @IDEquipo";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para actualizar la relación.
                using (SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = modificacionEqpsXCompBody.IDCompetencia });
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = modificacionEqpsXCompBody.IDEquipo });

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

        // Método para eliminar la relación EquiposXCompetencia.
        public static bool bajaEqpsXCompHandler(DeleteEquiposXCompetenciaDTO bajaEqpsXCompBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false; // Indicador del éxito de la operación de eliminación.

                // Consulta SQL para eliminar la relación EquiposXCompetencia.
                var DeleteQuery = "DELETE FROM EquiposXCompetencia WHERE IDEquipo = @IDEquipo AND IDCompetencia = @IDCompetencia";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para eliminar la relación.
                using (SqlCommand sqlCommand = new SqlCommand(DeleteQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = bajaEqpsXCompBody.IDEquipo });
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = bajaEqpsXCompBody.IDCompetencia });

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
