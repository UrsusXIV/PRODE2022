using System.Data.SqlClient;
using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class ApostadoresXGrupoHandler : DBHandler
    {
        // Método para agregar un nuevo apostador a un grupo en la base de datos.
        public static bool altaApostadoresXGrupoHandler(PostApostadoresXGrupo altaApostadoresXGrupoBody)
        {
            bool insert = false; // Indicador de éxito de la operación de inserción.

            // Establece la conexión con la base de datos utilizando la cadena de conexión definida.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar un nuevo registro en la tabla ApostadoresXGrupo.
                var InsertQuery = "INSERT INTO ApostadoresXGrupo (IDApostador, IDGruposAp) VALUES (@IDApostador, @IDGruposAp)";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para insertar un nuevo registro.
                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    // Asigna los valores a los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = altaApostadoresXGrupoBody.IDApostador });
                    sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) { Value = altaApostadoresXGrupoBody.IDGruposAp });

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

        // Método para consultar apostadores por grupo en la base de datos.
        public static ApostadoresXGrupoResponse consultaApostadoresXGrupoHandler(GetApostadoresXGrupo consultaApostadoresXGrupoQuery)
        {
            ApostadoresXGrupoResponse response = new ApostadoresXGrupoResponse
            {
                // Inicializa la lista de apostadores por grupo en la respuesta.
                ApostadoresXGrupos = new List<GetApostadoresXGrupo>()
            };

            // Establece la conexión con la base de datos.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Selecciona la consulta SQL según el ID del grupo proporcionado.
                var SelectQuery = consultaApostadoresXGrupoQuery.IDGruposAp == 0 ?
                    "SELECT ApostadoresXGrupo.IDApostador, ApostadoresXGrupo.IDGruposAp, Apostadores.ApostNombre, GrupoApuestas.GruposApDescripcion FROM ApostadoresXGrupo INNER JOIN GrupoApuestas ON ApostadoresXGrupo.IDGruposAp = GrupoApuestas.IDGruposAp INNER JOIN Apostadores ON ApostadoresXGrupo.IDApostador = Apostadores.IDApostador" :
                    "SELECT ApostadoresXGrupo.IDApostador, ApostadoresXGrupo.IDGruposAp, Apostadores.ApostNombre, GrupoApuestas.GruposApDescripcion FROM ApostadoresXGrupo INNER JOIN GrupoApuestas ON ApostadoresXGrupo.IDGruposAp = GrupoApuestas.IDGruposAp INNER JOIN Apostadores ON ApostadoresXGrupo.IDApostador = Apostadores.IDApostador WHERE ApostadoresXGrupo.IDGruposAp = @IDGruposAp";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para consultar apostadores por grupo.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) { Value = consultaApostadoresXGrupoQuery.IDGruposAp });

                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Si hay resultados, lee cada fila.
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                // Crea un objeto GetApostadoresXGrupo para cada registro y lo añade a la lista de respuesta.
                                var apostadoresXgrupoDTO = new GetApostadoresXGrupo
                                {
                                    IDApostador = Convert.ToInt32(sqlDataReader["IDApostador"]),
                                    IDGruposAp = Convert.ToInt32(sqlDataReader["IDGruposAp"]),
                                    GruposApDescripcion = sqlDataReader["GruposApDescripcion"].ToString(),
                                    ApostNombre = sqlDataReader["ApostNombre"].ToString()
                                };

                                response.ApostadoresXGrupos.Add(apostadoresXgrupoDTO);
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            return response; // Devuelve la respuesta con la lista de apostadores por grupo.
        }

        // Método para eliminar un apostador de un grupo en la base de datos.
        public static bool bajaApostadoresXGrupoHandler(DeleteApostadoresXGrupo bajaApostadoresXGrupoBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false; // Indicador de éxito de la operación de eliminación.

                // Consulta SQL para eliminar un registro en la tabla ApostadoresXGrupo.
                var DeleteString = "DELETE FROM ApostadoresXGrupo WHERE IDApostador = @IDApostador";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para eliminar un registro.
                using (SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {
                    // Asigna el valor del ID del apostador a eliminar.
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = bajaApostadoresXGrupoBody.IDApostador });

                    // Ejecuta la consulta y verifica si se eliminó alguna fila.
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        delete = true; // Si se eliminó una fila, cambia el indicador a verdadero.
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
                return delete; // Devuelve verdadero si se realizó la eliminación.
            }
        }
    }
}

