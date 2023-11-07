using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class EquiposXCompetenciaHandler : DBHandler
    {
        // Metodo para dar de alta a las sedes.-
        public static bool altaEqpsXCompHandler(PostEquiposXCompetenciaDTO altaEqpsXCompBody)
        {
            bool insert = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var InsertQuery = "INSERT INTO EquiposXCompetencia (IDEquipo, IDCompetencia) Values (@IDEquipo, @IDCompetencia)";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = altaEqpsXCompBody.IDEquipo });

                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = altaEqpsXCompBody.IDCompetencia });

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
        /*
        public static List<GetEquiposXCompetenciaDTO> consultaEqpsXCompHandler(GetEquiposXCompetenciaDTO consultaEqpsXCompBody)
        {
            List<GetEquiposXCompetenciaDTO> listaEqpsXComp = new List<GetEquiposXCompetenciaDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;


                if (consultaEqpsXCompBody.IDEquipo == 0 & consultaEqpsXCompBody.IDCompetencia == 0)
                {
                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre FROM EquiposXCompetencia INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia  INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo "; // Trae todo.
                    
                }

                else if (consultaEqpsXCompBody.IDEquipo != 0 & consultaEqpsXCompBody.IDCompetencia != 0)
                {

                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre FROM EquiposXCompetencia INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia  INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo WHERE EquiposXCompetencia.IDEquipo = @IDEquipo AND EquiposXCompetencia.IDCompetencia = @IDCompetencia"; // Traer el equipo y competencia informada.

                }

                else if (consultaEqpsXCompBody.IDEquipo != 0 & consultaEqpsXCompBody.IDCompetencia == 0)
                {
                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre FROM EquiposXCompetencia INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo WHERE EquiposXCompetencia.IDEquipo = @IDEquipo"; // Trae todas las competencias del equipo informado.

                }

                else if (consultaEqpsXCompBody.IDEquipo == 0 & consultaEqpsXCompBody.IDCompetencia != 0)
                {
                   SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre FROM EquiposXCompetencia INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo WHERE EquiposXCompetencia.IDCompetencia = @IDCompetencia"; // Traer todos los equipos de la competencia informada.

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) {Value = consultaEqpsXCompBody.IDEquipo});
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = consultaEqpsXCompBody.IDCompetencia });

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var eqpsxcompDTO = new GetEquiposXCompetenciaDTO();

                                eqpsxcompDTO.IDEquipo = Convert.ToInt32(sqlDataReader["IDEquipo"]);

                                eqpsxcompDTO.IDCompetencia = Convert.ToInt32(sqlDataReader["IDCompetencia"]);

                                eqpsxcompDTO.EquipoNombre = sqlDataReader["EquipoNombre"].ToString();

                                eqpsxcompDTO.NombreCompetencia = sqlDataReader["CompetenciaNombre"].ToString();

                                listaEqpsXComp.Add(eqpsxcompDTO);

                            }

                        }

                    }

                }

                sqlConnection.Close();
            }

            return listaEqpsXComp;
        }
        */

        public static EquiposXCompetenciaResponse consultaEquiposXCompetenciaHandler(GetEquiposXCompetenciaDTO consultaEquiposXCompetenciaBody)
        {
            EquiposXCompetenciaResponse response = new EquiposXCompetenciaResponse();

            response.EquiposXCompetencia = new List<GetEquiposXCompetenciaDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if (consultaEquiposXCompetenciaBody.IDEquipo == 0 & consultaEquiposXCompetenciaBody.IDCompetencia == 0)
                {
                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre FROM EquiposXCompetencia INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia  INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo "; // Trae todo.

                }

                else if (consultaEquiposXCompetenciaBody.IDEquipo != 0 & consultaEquiposXCompetenciaBody.IDCompetencia != 0)
                {

                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre FROM EquiposXCompetencia INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia  INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo WHERE EquiposXCompetencia.IDEquipo = @IDEquipo AND EquiposXCompetencia.IDCompetencia = @IDCompetencia"; // Traer el equipo y competencia informada.

                }

                else if (consultaEquiposXCompetenciaBody.IDEquipo != 0 & consultaEquiposXCompetenciaBody.IDCompetencia == 0)
                {
                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre FROM EquiposXCompetencia INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo WHERE EquiposXCompetencia.IDEquipo = @IDEquipo"; // Trae todas las competencias del equipo informado.

                }

                else if (consultaEquiposXCompetenciaBody.IDEquipo == 0 & consultaEquiposXCompetenciaBody.IDCompetencia != 0)
                {
                    SelectQuery = "SELECT EquiposXCompetencia.IDEquipo, EquiposXCompetencia.IDCompetencia, Equipos.EquipoNombre, Competencia.CompetenciaNombre FROM EquiposXCompetencia INNER JOIN Competencia ON EquiposXCompetencia.IDCompetencia = Competencia.IDCompetencia INNER JOIN Equipos ON EquiposXCompetencia.IDEquipo = Equipos.IDEquipo WHERE EquiposXCompetencia.IDCompetencia = @IDCompetencia"; // Traer todos los equipos de la competencia informada.

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand (SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = consultaEquiposXCompetenciaBody.IDEquipo });
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = consultaEquiposXCompetenciaBody.IDCompetencia });


                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var eqpsxcompDTO = new GetEquiposXCompetenciaDTO();

                                eqpsxcompDTO.IDEquipo = Convert.ToInt32(sqlDataReader["IDEquipo"]);

                                eqpsxcompDTO.IDCompetencia = Convert.ToInt32(sqlDataReader["IDCompetencia"]);

                                eqpsxcompDTO.EquipoNombre = sqlDataReader["EquipoNombre"].ToString();

                                eqpsxcompDTO.NombreCompetencia = sqlDataReader["CompetenciaNombre"].ToString();

                                response.EquiposXCompetencia.Add(eqpsxcompDTO);

                            }
                        }
                    }
                }
                sqlConnection.Close();


            }

            return response;
        }

        //----------------------------

        public static bool modificacionEqpsXCompHandler(PutEquiposXCompetenciaDTO modificacionEqpsXCompBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false;

                var UpdateQuery = "UPDATE EquiposXCompetencia SET IDCompetencia = @IDCompetencia WHERE IDEquipo = @IDEquipo";

                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = modificacionEqpsXCompBody.IDCompetencia});

                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = modificacionEqpsXCompBody.IDEquipo });

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

        public static bool bajaEqpsXCompHandler(DeleteEquiposXCompetenciaDTO bajaEqpsXCompBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false;

                var DeleteString = "DELETE FROM EquiposXCompetencia WHERE IDEquipo = @IDEquipo AND IDCompetencia = @IDCompetencia";


                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = bajaEqpsXCompBody.IDEquipo});
                    sqlCommand.Parameters.Add(new SqlParameter("IDCompetencia", System.Data.SqlDbType.Int) { Value = bajaEqpsXCompBody.IDCompetencia });

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


