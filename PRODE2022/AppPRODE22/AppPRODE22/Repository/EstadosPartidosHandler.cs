using System.Data.SqlClient;
using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Models;
using System.Collections.Generic;

namespace AppPRODE22.Repository
{
    public class EstadosPartidosHandler : DBHandler
    {
        // Método para dar de alta un nuevo estado de partido en la base de datos.
        public static bool altaEstadoHandler(PostEstadosPartidosDTO altaEstadoBody)
        {
            bool insert = false; // Indicador del éxito de la operación de inserción.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar un nuevo estado de partido.
                var InsertQuery = "INSERT INTO EstadoPartidos (IDEstado, EstadoPartido) Values (@IDEstado, @EstadoPartido)";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para insertar el estado de partido.
                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    // Asigna los valores de los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDEstado", System.Data.SqlDbType.Int) { Value = altaEstadoBody.IDEstado });
                    sqlCommand.Parameters.Add(new SqlParameter("EstadoPartido", System.Data.SqlDbType.VarChar) { Value = altaEstadoBody.EstadoDescripcion });

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

        // Método para consultar estados de partidos según el ID proporcionado.
        public static List<GetEstadosPartidosDTO> consultaEstadoHandler(GetEstadosPartidosDTO consultaEstadoBody)
        {
            List<GetEstadosPartidosDTO> listaEstados = new List<GetEstadosPartidosDTO>(); // Lista para almacenar los estados de partidos encontrados.

            // Establece la conexión con la base de datos utilizando la cadena de conexión especificada.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = (consultaEstadoBody.IDEstado == 0)
                    ? "SELECT * FROM EstadoPartidos"
                    : "SELECT * FROM EstadoPartidos WHERE IDEstado = @IDEstado";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para la consulta de estados de partidos.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    // Si se proporcionó un ID de estado específico, añade el parámetro a la consulta.
                    if (consultaEstadoBody.IDEstado != 0)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("IDEstado", System.Data.SqlDbType.Int) { Value = consultaEstadoBody.IDEstado });
                    }

                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Verifica si hay filas devueltas por la consulta.
                        if (sqlDataReader.HasRows)
                        {
                            // Itera sobre cada fila devuelta y construye objetos DTO para cada estado de partido.
                            while (sqlDataReader.Read())
                            {
                                var estadosDTO = new GetEstadosPartidosDTO
                                {
                                    IDEstado = Convert.ToInt32(sqlDataReader["IDEstado"]),
                                    EstadoDescripcion = sqlDataReader["EstadoPartido"].ToString()
                                };

                                // Agrega el objeto DTO a la lista de estados encontrados.
                                listaEstados.Add(estadosDTO);
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            // Devuelve la lista de estados de partidos encontrados.
            return listaEstados;
        }
    }
}
