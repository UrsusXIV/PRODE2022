using System.Data.SqlClient;
using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Models;
using System.Collections.Generic;

namespace AppPRODE22.Repository
{
    public class GruposApuestasHandler : DBHandler
    {
        // Método para dar de alta un nuevo grupo de apuestas en la base de datos.
        public static bool altaGruposApuestasHandler(PostGruposApuestasDTO altaGruposApuestasBody)
        {
            bool insert = false; // Indicador del éxito de la operación de inserción.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar un nuevo grupo de apuestas.
                var InsertQuery = "INSERT INTO GrupoApuestas (IDGruposAp, GruposApDescripcion) Values (@IDGruposAp, @GruposApDescripcion)";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para insertar el grupo de apuestas.
                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) { Value = altaGruposApuestasBody.IDGruposAp });
                    sqlCommand.Parameters.Add(new SqlParameter("GruposApDescripcion", System.Data.SqlDbType.VarChar) { Value = altaGruposApuestasBody.GrupoApDescripcion });

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

        // Método para consultar grupos de apuestas según el ID proporcionado.
        public static GrupoApuestasResponse consultaGruposApuestasHandler(GetGruposApuestasDTO consultaGruposApuestasQuery)
        {
            GrupoApuestasResponse response = new GrupoApuestasResponse(); // Respuesta que contendrá la lista de grupos de apuestas encontrados.
            response.GrupoApuestas = new List<GetGruposApuestasDTO>(); // Inicializa la lista de grupos de apuestas.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = (consultaGruposApuestasQuery.IDGruposAp == 0)
                    ? "SELECT * FROM GrupoApuestas"
                    : "SELECT GrupoApuestas.IDGruposAp, GrupoApuestas.GruposApDescripcion FROM GrupoApuestas WHERE IDGruposAp = @IDGruposAp";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para la consulta de grupos de apuestas.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    // Si se proporcionó un ID de grupo de apuestas específico, añade el parámetro a la consulta.
                    if (consultaGruposApuestasQuery.IDGruposAp != 0)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) { Value = consultaGruposApuestasQuery.IDGruposAp });
                    }

                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Verifica si hay filas devueltas por la consulta.
                        if (sqlDataReader.HasRows)
                        {
                            // Itera sobre cada fila devuelta y construye objetos DTO para cada grupo de apuestas.
                            while (sqlDataReader.Read())
                            {
                                var gruposApuestasDTO = new GetGruposApuestasDTO
                                {
                                    IDGruposAp = Convert.ToInt32(sqlDataReader["IDGruposAp"]),
                                    GrupoApDescripcion = sqlDataReader["GruposApDescripcion"].ToString()
                                };

                                // Agrega el objeto DTO a la lista de grupos de apuestas encontrados en la respuesta.
                                response.GrupoApuestas.Add(gruposApuestasDTO);
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            // Devuelve la respuesta que contiene la lista de grupos de apuestas encontrados.
            return response;
        }

        // Método para modificar la descripción de un grupo de apuestas existente.
        public static bool modificacionGruposApuestasHandler(PutGruposApuestasDTO modificacionGruposApuestasBody)
        {
            bool update = false; // Indicador del éxito de la operación de actualización.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para actualizar la descripción de un grupo de apuestas.
                var UpdateQuery = "UPDATE GrupoApuestas SET GruposApDescripcion = @GruposApDescripcion WHERE IDGruposAp = @IDGruposAp";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para actualizar la descripción del grupo de apuestas.
                using (SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("GruposApDescripcion", System.Data.SqlDbType.VarChar) { Value = modificacionGruposApuestasBody.GrupoApDescripcion });
                    sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) { Value = modificacionGruposApuestasBody.IDGruposAp });

                    // Ejecuta la consulta y verifica si se actualizó alguna fila.
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

        // Método para eliminar un grupo de apuestas según su ID.
        public static bool bajaGruposApuestasHandler(DeleteGruposApuestasDTO bajaGruposApuestasBody)
        {
            bool delete = false; // Indicador del éxito de la operación de eliminación.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para eliminar un grupo de apuestas.
                var DeleteString = "DELETE FROM GrupoApuestas WHERE IDGruposAp = @IDGruposAp";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para eliminar el grupo de apuestas.
                using (SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) { Value = bajaGruposApuestasBody.IDGruposAp });

                    // Ejecuta la consulta y verifica si se eliminó alguna fila.
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
