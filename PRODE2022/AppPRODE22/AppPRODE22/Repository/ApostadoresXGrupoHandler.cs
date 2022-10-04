using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class ApostadoresXGrupoHandler : DBHandler
    {
        // Metodo para dar de alta a las sedes.-
        public static bool altaApostadoresXGrupoHandler(PostApostadoresXGrupo altaApostadoresXGrupoBody)
        {
            bool insert = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var InsertQuery = "INSERT INTO ApostadoresXGrupo (IDApostador, IDGruposAp) Values (@IDApostador, @IDGruposAp)";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = altaApostadoresXGrupoBody.IDApostador });

                    sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) { Value = altaApostadoresXGrupoBody.IDGruposAp });

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

        public static List<GetApostadoresXGrupo> consultaApostadoresXGrupoHandler(GetApostadoresXGrupo consultaApostadoresXGrupoBody)
        {
            List<GetApostadoresXGrupo> listaApostadoresXGrupo = new List<GetApostadoresXGrupo>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if (consultaApostadoresXGrupoBody.IDApostador == 0)
                {
                    SelectQuery = "SELECT * FROM ApostadoresXGrupo";

                }

                else
                {

                    SelectQuery = "SELECT * FROM ApostadoresXGrupo WHERE IDApostador = @IDApostador";

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) {Value = consultaApostadoresXGrupoBody.IDApostador});
                    
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var apostadoresxgrupoDTO = new GetApostadoresXGrupo();

                                apostadoresxgrupoDTO.IDApostador = Convert.ToInt32(sqlDataReader["IDApostador"]);

                                apostadoresxgrupoDTO.IDGruposAp = Convert.ToInt32(sqlDataReader["IDGruposAp"]);

                                listaApostadoresXGrupo.Add(apostadoresxgrupoDTO);

                            }

                        }

                    }

                }

                sqlConnection.Close();
            }

            return listaApostadoresXGrupo;
        }

        //----------------------------

        public static bool bajaApostadoresXGrupoHandler(DeleteApostadoresXGrupo bajaApostadoresXGrupoBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false;

                var DeleteString = "DELETE FROM ApostadoresXGrupo WHERE IDApostador = @IDApostador";


                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = bajaApostadoresXGrupoBody.IDApostador});

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if(numberOfRows > 0)
                    {

                        delete = true;

                    }

                    
                }

                sqlConnection.Close();

                return delete;
            }


        }


    }
}


// TEST.-1