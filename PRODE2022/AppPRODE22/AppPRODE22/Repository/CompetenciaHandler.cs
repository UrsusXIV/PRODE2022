using System.Data.SqlClient; // Librería para manejar conexiones y comandos SQL.
using AppPRODE22.Controllers.DTOs; // Referencia a los DTOs del proyecto.
using AppPRODE22.Models; // Referencia a los modelos del proyecto.

namespace AppPRODE22.Repository
{
    // Clase para manejar operaciones relacionadas con competencias.
    public class CompetenciaHandler : DBHandler
    {
        // Método para registrar una nueva competencia en la base de datos.
        public static bool altaCompetenciaHandler(PostCompetenciasDTO altaCompetenciaBody)
        {
            bool insert = false; // Indicador del éxito de la operación de inserción.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar una nueva competencia en la tabla Competencia.
                var InsertQuery = "INSERT INTO Competencia (IDCompetencia, CompetenciaNombre) VALUES (@IDCompetencia, @CompetenciaNombre)";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para insertar una nueva competencia.
                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = altaCompetenciaBody.IDCompetencia });
                    sqlCommand.Parameters.Add(new SqlParameter("CompetenciaNombre", System.Data.SqlDbType.VarChar) { Value = altaCompetenciaBody.CompetenciaNombre });

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

        // Método para consultar competencias según los criterios de búsqueda proporcionados.
        public static CompetenciasResponse consultaCompetenciasHandler(GetCompetenciasDTO consultaCompetenciasBody)
        {
            CompetenciasResponse response = new CompetenciasResponse
            {
                // Inicializa la lista de competencias en la respuesta.
                Competencias = new List<GetCompetenciasDTO>()
            };

            // Establece la conexión con la base de datos.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string SelectQuery = string.Empty; // Variable para almacenar la consulta SQL.

                // Construye la consulta SQL según los criterios de búsqueda proporcionados.
                if (consultaCompetenciasBody.IDCompetencia == 0)
                {
                    // Consulta para obtener todas las competencias si no se especifica un ID.
                    SelectQuery = "SELECT * FROM Competencia";
                }
                else
                {
                    // Consulta para obtener una competencia específica por ID.
                    SelectQuery = "SELECT * FROM Competencia WHERE IDCompetencia = @IDCompetencia";
                }

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para consultar las competencias.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    // Asigna el valor del parámetro de la consulta si se especifica un ID.
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = consultaCompetenciasBody.IDCompetencia });

                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Verifica si hay resultados.
                        if (sqlDataReader.HasRows)
                        {
                            // Lee cada fila y construye el objeto DTO para cada competencia.
                            while (sqlDataReader.Read())
                            {
                                var competenciasDTO = new GetCompetenciasDTO
                                {
                                    IDCompetencia = Convert.ToInt32(sqlDataReader["IDCompetencia"]),
                                    CompetenciaNombre = sqlDataReader["CompetenciaNombre"].ToString()
                                };

                                // Añade el DTO de la competencia a la lista de respuesta.
                                response.Competencias.Add(competenciasDTO);
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            return response; // Devuelve la respuesta con la lista de competencias.
        }

        // Método para modificar una competencia existente en la base de datos.
        public static bool modificacionCompetenciaHandler(PutCompetenciaDTO modificacionCompetenciaBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false; // Indicador del éxito de la operación de actualización.

                // Consulta SQL para actualizar una competencia.
                var UpdateQuery = "UPDATE Competencia SET CompetenciaNombre = @CompetenciaNombre WHERE IDCompetencia = @IDCompetencia";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para actualizar la competencia.
                using (SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("CompetenciaNombre", System.Data.SqlDbType.VarChar) { Value = modificacionCompetenciaBody.CompetenciaNombre });
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = modificacionCompetenciaBody.IDCompetencia });

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

        // Método para eliminar una competencia existente de la base de datos.
        public static bool bajaCompetenciaHandler(DeleteCompetenciasDTO bajaCompetenciaBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false; // Indicador del éxito de la operación de eliminación.

                // Consulta SQL para eliminar una competencia.
                var DeleteQuery = "DELETE FROM Competencia WHERE IDCompetencia = @IDCompetencia";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para eliminar la competencia.
                using (SqlCommand sqlCommand = new SqlCommand(DeleteQuery, sqlConnection))
                {
                    // Asigna el valor del parámetro de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = bajaCompetenciaBody.IDCompetencia });

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
