using System.Data.SqlClient; // Librería para manejar conexiones y comandos SQL.
using AppPRODE22.Controllers.DTOs; // Referencia a los DTOs del proyecto.
using AppPRODE22.Models; // Referencia a los modelos del proyecto.

namespace AppPRODE22.Repository
{
    // Clase para manejar operaciones relacionadas con equipos.
    public class EquiposHandler : DBHandler
    {
        // Método para dar de alta un nuevo equipo en la base de datos.
        public static bool altaEquipoHandler(PostEquipoDTO altaEquipoBody)
        {
            bool insert = false; // Indicador del éxito de la operación de inserción.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar un nuevo equipo en la tabla Equipos.
                var InsertQuery = "INSERT INTO Equipos (IDEquipo, EquipoNombre) VALUES (@IDEquipo, @EquipoNombre)";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para insertar un nuevo equipo.
                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = altaEquipoBody.IdEquipo });
                    sqlCommand.Parameters.Add(new SqlParameter("EquipoNombre", System.Data.SqlDbType.VarChar) { Value = altaEquipoBody.EquipoNombre });

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

        // Método para consultar equipos según los criterios de búsqueda proporcionados.
        public static EquiposResponse consultaEquiposHandler(GetEquipoDTO consultaEquipoBody)
        {
            EquiposResponse response = new EquiposResponse
            {
                // Inicializa la lista de equipos en la respuesta.
                Equipos = new List<GetEquipoDTO>()
            };

            // Establece la conexión con la base de datos.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string SelectQuery = string.Empty; // Variable para almacenar la consulta SQL.

                // Construye la consulta SQL según los criterios de búsqueda proporcionados.
                if (consultaEquipoBody.IdEquipo == 0)
                {
                    // Consulta para obtener todos los equipos si no se especifica un ID.
                    SelectQuery = "SELECT * FROM Equipos";
                }
                else
                {
                    // Consulta para obtener un equipo específico por ID.
                    SelectQuery = "SELECT * FROM Equipos WHERE IDEquipo = @IDEquipo";
                }

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para consultar los equipos.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    // Asigna el valor del parámetro de la consulta si se especifica un ID.
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = consultaEquipoBody.IdEquipo });

                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Verifica si hay resultados.
                        if (sqlDataReader.HasRows)
                        {
                            // Lee cada fila y construye el objeto DTO para cada equipo.
                            while (sqlDataReader.Read())
                            {
                                var equipoDTO = new GetEquipoDTO
                                {
                                    IdEquipo = Convert.ToInt32(sqlDataReader["IdEquipo"]),
                                    EquipoNombre = sqlDataReader["EquipoNombre"].ToString()
                                };

                                // Añade el DTO del equipo a la lista de respuesta.
                                response.Equipos.Add(equipoDTO);
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            return response; // Devuelve la respuesta con la lista de equipos.
        }

        // Método para modificar un equipo existente en la base de datos.
        public static bool modificacionEquiposHandler(PutEquipoDTO modificacionEquipoBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false; // Indicador del éxito de la operación de actualización.

                // Consulta SQL para actualizar un equipo.
                var UpdateQuery = "UPDATE Equipos SET EquipoNombre = @EquipoNombre WHERE IDEquipo = @IDEquipo";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para actualizar el equipo.
                using (SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("EquipoNombre", System.Data.SqlDbType.VarChar) { Value = modificacionEquipoBody.EquipoNombre });
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = modificacionEquipoBody.IdEquipo });

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

        // Método para eliminar un equipo existente de la base de datos.
        public static bool bajaEquipoHandler(DeleteEquipoDTO bajaEquipoBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false; // Indicador del éxito de la operación de eliminación.

                // Consulta SQL para eliminar un equipo.
                var DeleteQuery = "DELETE FROM Equipos WHERE IDEquipo = @IDEquipo";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para eliminar el equipo.
                using (SqlCommand sqlCommand = new SqlCommand(DeleteQuery, sqlConnection))
                {
                    // Asigna el valor del parámetro de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = bajaEquipoBody.IdEquipo });

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
