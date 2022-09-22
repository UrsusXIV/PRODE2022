using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class EstadosPartidosHandler : DBHandler
    {
        // Metodo para dar de alta el estado del partido.-
        public static bool altaEstadoHandler(PostEstadosPartidosDTO altaEstadoBody)
        {
            bool insert = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var InsertQuery = "INSERT INTO EstadoPartidos (IDEstado, EstadoPartido) Values (@IDEstado, @EstadoPartido)";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDEstado", System.Data.SqlDbType.Int) { Value = altaEstadoBody.IDEstado });

                    sqlCommand.Parameters.Add(new SqlParameter("EstadoPartido", System.Data.SqlDbType.VarChar) { Value = altaEstadoBody.EstadoDescripcion });

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {

                        insert = true;

                    }

                }

                sqlConnection.Close();

                return insert;
            }
        }

        // ---------------------------------------------------------

        public static List<GetEstadosPartidosDTO> consultaEstadoHandler(GetEstadosPartidosDTO consultaEstadoBody)
        {
            List<GetEstadosPartidosDTO> listaEstados = new List<GetEstadosPartidosDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if (consultaEstadoBody.IDEstado == 0)
                {
                    SelectQuery = "SELECT * FROM EstadoPartidos";

                }

                else
                {

                    SelectQuery = "SELECT * FROM EstadoPartidos WHERE IDEstado = @IDEstado";

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDEstado", System.Data.SqlDbType.Int) {Value = consultaEstadoBody.IDEstado});
                    
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var estadosDTO = new GetEstadosPartidosDTO();

                                estadosDTO.IDEstado = Convert.ToInt32(sqlDataReader["IDEstado"]);

                                estadosDTO.EstadoDescripcion = sqlDataReader["EstadoPartido"].ToString();

                                listaEstados.Add(estadosDTO);

                            }

                        }

                    }

                }

                sqlConnection.Close();
            }

            return listaEstados;
        }

        //----------------------------

    }
}
