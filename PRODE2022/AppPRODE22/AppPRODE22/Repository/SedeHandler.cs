using System.Data.SqlClient;
using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Models;
using System.Collections.Generic;

namespace AppPRODE22.Repository
{
    public class SedeHandler : DBHandler
    {
        // Método para dar de alta a las sedes.
        public static bool altaSedeHandler(PostSedeDTO altaSedeBody)
        {
            bool insert = false; // Indicador del éxito de la operación de inserción.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar una nueva sede.
                var InsertQuery = "INSERT INTO Sedes (SedeID, SedeNombre) Values (@SedeID, @SedeNombre)";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para la inserción de la sede.
                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("SedeID", System.Data.SqlDbType.Int) { Value = altaSedeBody.SedeID });
                    sqlCommand.Parameters.Add(new SqlParameter("SedeNombre", System.Data.SqlDbType.VarChar) { Value = altaSedeBody.SedeNombre });

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

        // Método para consultar sedes según los parámetros especificados.
        public static SedesResponse consultaSedeHandler(GetSedeDTO consultaSedeBody)
        {
            SedesResponse sedesResponse = new SedesResponse(); // Respuesta que contendrá la lista de sedes encontradas.
            sedesResponse.Sede = new List<GetSedeDTO>(); // Inicializa la lista de sedes.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                // Determina la consulta SQL a ejecutar según los parámetros proporcionados.
                if (consultaSedeBody.SedeID == 0)
                {
                    SelectQuery = "SELECT * FROM Sedes";
                }
                else
                {
                    SelectQuery = "SELECT * FROM Sedes WHERE SedeID = @SedeID";
                }

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para la consulta de sedes.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("SedeID", System.Data.SqlDbType.Int) { Value = consultaSedeBody.SedeID });

                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Verifica si hay filas devueltas por la consulta.
                        if (sqlDataReader.HasRows)
                        {
                            // Itera sobre cada fila devuelta y construye objetos DTO para cada sede.
                            while (sqlDataReader.Read())
                            {
                                var sedeDTO = new GetSedeDTO
                                {
                                    SedeID = Convert.ToInt32(sqlDataReader["SedeID"]),
                                    SedeNombre = sqlDataReader["SedeNombre"].ToString()
                                };

                                // Agrega el objeto DTO a la lista de sedes en la respuesta.
                                sedesResponse.Sede.Add(sedeDTO);
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            // Devuelve la respuesta que contiene la lista de sedes encontradas.
            return sedesResponse;
        }

        // Método para modificar los detalles de una sede existente.
        public static bool modificacionSedeHandler(PutSedeDTO modificacionSedeBody)
        {
            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false; // Indicador del éxito de la operación de actualización.

                // Consulta SQL para actualizar los detalles de una sede.
                var UpdateQuery = "UPDATE Sedes SET SedeNombre = @SedeNombre WHERE SedeID = @SedeID";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para actualizar la sede.
                using (SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("SedeNombre", System.Data.SqlDbType.VarChar) { Value = modificacionSedeBody.SedeNombre });
                    sqlCommand.Parameters.Add(new SqlParameter("SedeID", System.Data.SqlDbType.Int) { Value = modificacionSedeBody.SedeID });

                    // Ejecuta la consulta y obtiene el número de filas afectadas.
                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    // Si se actualizó al menos una fila, marca la actualización como exitosa.
                    if (numberOfRows > 0)
                    {
                        update = true;
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();

                // Devuelve true si la actualización fue exitosa, false de lo contrario.
                return update;
            }
        }

        // Método para eliminar una sede de la base de datos.
        public static bool bajaSedeHandler(DeleteSedeDTO bajaSedeBody)
        {
            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false; // Indicador del éxito de la operación de eliminación.

                // Consulta SQL para eliminar una sede.
                var DeleteString = "DELETE FROM Sedes WHERE SedeID = @SedeID";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para eliminar la sede.
                using (SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("SedeID", System.Data.SqlDbType.Int) { Value = bajaSedeBody.SedeID });

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
