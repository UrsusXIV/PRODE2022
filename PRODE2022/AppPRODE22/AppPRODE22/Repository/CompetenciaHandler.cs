using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class CompetenciaHandler : DBHandler
    {
        // Metodo para dar de alta a las competencias.-
        public static bool altaCompetenciaHandler(PostCompetenciasDTO altaCompetenciaBody)
        {
            bool insert = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var InsertQuery = "INSERT INTO Competencia (IDCompetencia, CompetenciaNombre) Values (@IDCompetencia, @CompetenciaNombre)";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = altaCompetenciaBody.IDCompetencia});

                    sqlCommand.Parameters.Add(new SqlParameter("CompetenciaNombre", System.Data.SqlDbType.VarChar) { Value = altaCompetenciaBody.CompetenciaNombre });

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

        /*public static List<GetCompetenciasDTO> consultaCompetenciaHandler(GetCompetenciasDTO consultaCompetenciaBody)
        {
            List<GetCompetenciasDTO> listaCompetencias = new List<GetCompetenciasDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if (consultaCompetenciaBody.IDCompetencia == 0)
                {
                    SelectQuery = "SELECT * FROM Competencia";

                }

                else
                {

                    SelectQuery = "SELECT * FROM Competencia WHERE IDCompetencia = @IDCompetencia";

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) {Value = consultaCompetenciaBody.IDCompetencia});
                    
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var competenciaDTO = new GetCompetenciasDTO();

                                competenciaDTO.IDCompetencia = Convert.ToInt32(sqlDataReader["IDCompetencia"]);

                                competenciaDTO.CompetenciaNombre = sqlDataReader["CompetenciaNombre"].ToString();

                                listaCompetencias.Add(competenciaDTO);

                            }

                        }

                    }

                }

                sqlConnection.Close();
            }

            return listaCompetencias;
        }
        */

        public static CompetenciasResponse consultaCompetenciasHandler(GetCompetenciasDTO consultaCompetenciasBody)
        {
            CompetenciasResponse response = new CompetenciasResponse();

            response.Competencias = new List<GetCompetenciasDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if(consultaCompetenciasBody.IDCompetencia == 0)
                {
                    SelectQuery = "SELECT * FROM Competencia";
                }
                else
                {
                    SelectQuery = "SELECT * FROM Competencia WHERE IDCompetencia = @IDCompetencia";
                }
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = consultaCompetenciasBody.IDCompetencia });

                    using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if(sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var competenciasDTO = new GetCompetenciasDTO();

                                competenciasDTO.IDCompetencia = Convert.ToInt32(sqlDataReader["IDCompetencia"]);

                                competenciasDTO.CompetenciaNombre = sqlDataReader["CompetenciaNombre"].ToString();

                                response.Competencias.Add(competenciasDTO);
                            }
                        }
                    }
                }

                sqlConnection.Close();
            }

            return response;
        }

        //----------------------------

        public static bool modificacionCompetenciaHandler(PutCompetenciaDTO modificacionCompetenciaBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false;

                var UpdateQuery = "UPDATE Competencia SET CompetenciaNombre = @CompetenciaNombre WHERE IDCompetencia = @IDCompetencia";

                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("CompetenciaNombre", System.Data.SqlDbType.VarChar) { Value = modificacionCompetenciaBody.CompetenciaNombre});

                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = modificacionCompetenciaBody.IDCompetencia });

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if(numberOfRows > 0)
                    {

                        update = true;

                    }

                }
                sqlConnection.Close();

                return update;
            }
            
        }

        public static bool bajaCompetenciaHandler(DeleteCompetenciasDTO bajaCompetenciaBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false;

                var DeleteString = "DELETE FROM Competencia WHERE IDCompetencia = @IDCompetencia";


                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = bajaCompetenciaBody.IDCompetencia});

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