using System.Data.SqlClient;
using AppPRODE22.Controllers.DTOs;
using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class ApostadoresHandler : DBHandler
    {
        // Método para agregar un nuevo apostador a la base de datos.
        public static bool altaApostadoresHandler(PostApostadoresDTO altaApostadoresBody)
        {
            bool insert = false;

            // Establece la conexión con la base de datos utilizando la cadena de conexión definida.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar un nuevo apostador en la tabla Apostadores.
                var InsertQuery = "INSERT INTO Apostadores (IDApostador, ApostMail, ApostNombre, ApostPuntos) VALUES (@IDApostador, @ApostMail, @ApostNombre, @ApostPuntos)";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para insertar un nuevo apostador.
                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    // Asigna los valores a los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = altaApostadoresBody.IDApostador });
                    sqlCommand.Parameters.Add(new SqlParameter("ApostPuntos", System.Data.SqlDbType.Int) { Value = 0 }); // Inicializa los puntos en 0 al dar de alta.
                    sqlCommand.Parameters.Add(new SqlParameter("ApostMail", System.Data.SqlDbType.VarChar) { Value = altaApostadoresBody.ApostMail });
                    sqlCommand.Parameters.Add(new SqlParameter("ApostNombre", System.Data.SqlDbType.VarChar) { Value = altaApostadoresBody.AposNombre });

                    // Ejecuta la consulta y verifica si se insertó una nueva fila.
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        insert = true; // Si se insertó una fila, cambia el indicador a verdadero.
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
                return insert;
            }
        }

        // Método para consultar los apostadores en la base de datos.
        public static ApostadoresResponse consultaApostadoresHandler(GetApostadoresDTO consultaApostadoresBody)
        {
            ApostadoresResponse response = new ApostadoresResponse
            {
                // Inicializa la lista de apostadores en la respuesta.
                Apostadores = new List<GetApostadoresDTO>()
            };

            // Establece la conexión con la base de datos.
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Selecciona la consulta SQL según el ID proporcionado.
                var SelectQuery = consultaApostadoresBody.IDApostador == 0 ?
                    "SELECT IDApostador, ApostNombre, ApostPuntos, ApostMail FROM Apostadores" : // Consulta para todos los apostadores si el ID es 0.
                    "SELECT IDApostador, ApostNombre, ApostPuntos, ApostMail FROM Apostadores WHERE IDApostador = @IDApostador"; // Consulta específica para un apostador.

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para consultar los apostadores.
                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = consultaApostadoresBody.IDApostador });

                    // Ejecuta la consulta y obtiene los resultados.
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        // Si hay resultados, lee cada fila.
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                // Crea un objeto GetApostadoresDTO para cada apostador y lo añade a la lista de respuesta.
                                var apostadoresDTO = new GetApostadoresDTO
                                {
                                    IDApostador = Convert.ToInt32(sqlDataReader["IDApostador"]),
                                    AposNombre = sqlDataReader["ApostNombre"].ToString(),
                                    AposPuntos = Convert.ToInt32(sqlDataReader["ApostPuntos"]),
                                    ApostMail = sqlDataReader["ApostMail"].ToString()
                                };

                                response.Apostadores.Add(apostadoresDTO);
                            }
                        }
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
            }

            return response; // Devuelve la respuesta con la lista de apostadores.
        }

        // Método para actualizar la información de un apostador existente.
        public static bool modificacionApostadoresHandler(PutApostadoresDTO modificacionApostadoresBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false;

                // Consulta SQL para actualizar los datos de un apostador.
                var UpdateQuery = "UPDATE Apostadores SET ApostMail = @ApostMail, ApostNombre = @ApostNombre WHERE IDApostador = @IDApostador";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para actualizar el apostador.
                using (SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {
                    // Asigna los valores a los parámetros de la consulta.
                    sqlCommand.Parameters.Add(new SqlParameter("ApostMail", System.Data.SqlDbType.VarChar) { Value = modificacionApostadoresBody.ApostMail });
                    sqlCommand.Parameters.Add(new SqlParameter("ApostNombre", System.Data.SqlDbType.VarChar) { Value = modificacionApostadoresBody.AposNombre });
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = modificacionApostadoresBody.IDApostador });

                    // Ejecuta la consulta y verifica si se actualizó alguna fila.
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        update = true; // Si se actualizó una fila, cambia el indicador a verdadero.
                    }
                }

                // Cierra la conexión con la base de datos.
                sqlConnection.Close();
                return update; // Devuelve verdadero si se realizó la actualización.
            }
        }

        // Método para eliminar un apostador de la base de datos.
        public static bool bajaApostadoresHandler(DeleteApostadoresDTO bajaApostadoresBody)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false;

                // Consulta SQL para eliminar un apostador.
                var DeleteString = "DELETE FROM Apostadores WHERE IDApostador = @IDApostador";

                // Abre la conexión con la base de datos.
                sqlConnection.Open();

                // Configura y ejecuta el comando SQL para eliminar el apostador.
                using (SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {
                    // Asigna el valor del ID del apostador a eliminar.
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = bajaApostadoresBody.IDApostador });

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
