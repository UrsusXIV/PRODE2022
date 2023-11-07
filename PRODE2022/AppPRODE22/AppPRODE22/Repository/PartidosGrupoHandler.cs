using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;

//TODO: AJUSTAR TODO CONFORME AL CRUD 

namespace AppPRODE22.Repository
{
    public class PartidosGrupoHandler : DBHandler
    {
        // Metodo para dar de alta a las sedes.-
        public static bool altaPartidosGrupoHandler(PostPartidosGruposDTO altaPartidosGrupoBody)
        {
            bool insert = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var InsertQuery = "INSERT INTO PartidosGrupos (IDPartido, PartIDCompetencia, PartGrupo, PartIDEquipoL, PartIDEquipoV, PartIDSede, PartFecha, PartHora, PartIDEstado, PartGolesL, PartGolesV, PartPuntosL, PartPuntosV) Values (@IDPartido, @PartIDCompetencia, @PartGrupo, @PartIDEquipoL, @PartIDEquipoV, @PartIDSede, @PartFecha, @PartHora, @PartIDEstado, @PartGolesL, @PartGolesV, @PartPuntosL, @PartPuntosV)";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDPartido", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDPartido });

                    sqlCommand.Parameters.Add(new SqlParameter("PartIDCompetencia", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDCompetencia });

                    sqlCommand.Parameters.Add(new SqlParameter("PartGrupo", System.Data.SqlDbType.VarChar) { Value = altaPartidosGrupoBody.PartGrupo });

                    sqlCommand.Parameters.Add(new SqlParameter("PartIDEquipoL", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDEquipoL });

                    sqlCommand.Parameters.Add(new SqlParameter("PartIDEquipoV", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDEquipoV });

                    sqlCommand.Parameters.Add(new SqlParameter("PartIDSede", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDSede });

                    sqlCommand.Parameters.Add(new SqlParameter("PartFecha", System.Data.SqlDbType.Date) { Value = altaPartidosGrupoBody.PartFechaDate });

                    sqlCommand.Parameters.Add(new SqlParameter("PartHora", System.Data.SqlDbType.Time) { Value = altaPartidosGrupoBody.PartHoraTime });

                    sqlCommand.Parameters.Add(new SqlParameter("PartIDEstado", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartIDEstado });

                    sqlCommand.Parameters.Add(new SqlParameter("PartGolesL", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartGolesL });

                    sqlCommand.Parameters.Add(new SqlParameter("PartGolesV", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartGolesV });

                    sqlCommand.Parameters.Add(new SqlParameter("PartPuntosL", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartPuntosL });

                    sqlCommand.Parameters.Add(new SqlParameter("PartPuntosV", System.Data.SqlDbType.Int) { Value = altaPartidosGrupoBody.PartPuntosV });

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
        public static PartidosGruposResponse consultaPartidosGrupoHandler(GetPartidosGruposDTO consultaPartidosGrupoBody)
        {
            PartidosGruposResponse response = new PartidosGruposResponse();

            response.PartidosGrupos = new List<GetPartidosGruposDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;
                    
                    if(consultaPartidosGrupoBody.PartIDCompetencia != 0 && consultaPartidosGrupoBody.PartGrupo == "getall") // Improviso.
                {
                     SelectQuery = "SELECT PG.*, E1.EquipoNombre AS EquipoLocal,E2.EquipoNombre AS EquipoVisitante FROM PartidosGrupos AS PG INNER JOIN Equipos AS E1 ON PG.PartIDEquipoL = E1.IDEquipo INNER JOIN Equipos AS E2 ON PG.PartIDEquipoV = E2.IDEquipo WHERE PG.PartIDCompetencia = @PartIDCompetencia";
                     
                }

                    else if(consultaPartidosGrupoBody.PartIDCompetencia != 0 && consultaPartidosGrupoBody.PartGrupo != string.Empty && consultaPartidosGrupoBody.PartGrupo != "getall")
                {
                     SelectQuery = "SELECT * FROM PartidosGrupos WHERE PartIDCompetencia = @PartIDCompetencia AND PartGrupo = @PartGrupo";
                }

                    else if (consultaPartidosGrupoBody.PartIDPartido != 0)
                {
                     SelectQuery = "SELECT * FROM PartidosGrupos WHERE IDPartido = @IDPartido";
                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDPartido", System.Data.SqlDbType.Int) { Value = consultaPartidosGrupoBody.PartIDPartido });

                    sqlCommand.Parameters.Add(new SqlParameter("PartIDCompetencia", System.Data.SqlDbType.Int) { Value = consultaPartidosGrupoBody.PartIDCompetencia });

                    sqlCommand.Parameters.Add(new SqlParameter("PartGrupo", System.Data.SqlDbType.VarChar) { Value = consultaPartidosGrupoBody.PartGrupo });

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {

                            while (sqlDataReader.Read())
                            {

                                var partidosgruposDTO = new GetPartidosGruposDTO();

                                partidosgruposDTO.PartIDEstado = Convert.ToInt32(sqlDataReader["PartIDEstado"]);

                                partidosgruposDTO.PartIDCompetencia = Convert.ToInt32(sqlDataReader["PartIDCompetencia"]);

                                partidosgruposDTO.PartIDPartido = Convert.ToInt32(sqlDataReader["IDPartido"]);

                                partidosgruposDTO.PartIDSede = Convert.ToInt32(sqlDataReader["PartIDSede"]);

                                partidosgruposDTO.PartIDEquipoL = Convert.ToInt32(sqlDataReader["PartIDEquipoL"]);

                                partidosgruposDTO.PartIDEquipoV = Convert.ToInt32(sqlDataReader["PartIDEquipoV"]);

                                partidosgruposDTO.PartGolesL = Convert.ToInt32(sqlDataReader["PartGolesL"]);

                                partidosgruposDTO.PartGolesV = Convert.ToInt32(sqlDataReader["PartGolesV"]);

                                partidosgruposDTO.PartGrupo = sqlDataReader["PartGrupo"].ToString();

                                partidosgruposDTO.EquipoLNombre = sqlDataReader["EquipoLocal"].ToString();

                                partidosgruposDTO.EquipoVNombre = sqlDataReader["EquipoVisitante"].ToString();

                                response.PartidosGrupos.Add(partidosgruposDTO);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }

            return response;
        }
        /*public static List<GetPartidosGruposDTO> consultaPartidosGrupoHandler(GetPartidosGruposDTO consultaPartidosGrupoBody)
        {
            List<GetPartidosGruposDTO> listaPartidosGrupo = new List<GetPartidosGruposDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if (consultaPartidosGrupoBody.PartIDEstado == 0)
                {
                    SelectQuery = "SELECT * FROM PartidosGrupos";

                }

                else
                {

                    SelectQuery = "SELECT * FROM PartidosGrupos WHERE IDPartido = @IDPartido";

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDPartido", System.Data.SqlDbType.Int) {Value = consultaPartidosGrupoBody.PartIDEstado});
                    
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var partidosgruposDTO = new GetPartidosGruposDTO();

                                partidosgruposDTO.PartIDEstado = Convert.ToInt32(sqlDataReader["PartIDEstado"]);

                                partidosgruposDTO.PartIDCompetencia = Convert.ToInt32(sqlDataReader["PartIDCompetencia"]);

                                partidosgruposDTO.PartIDPartido = Convert.ToInt32(sqlDataReader["IDPartido"]);

                                partidosgruposDTO.PartIDSede = Convert.ToInt32(sqlDataReader["PartIDSede"]);

                                partidosgruposDTO.PartIDEquipoL = Convert.ToInt32(sqlDataReader["PartIDEquipoL"]);

                                partidosgruposDTO.PartIDEquipoV = Convert.ToInt32(sqlDataReader["PartIDEqupoV"]);

                                partidosgruposDTO.PartGolesL = Convert.ToInt32(sqlDataReader["PartGolesL"]);

                                partidosgruposDTO.PartGolesV = Convert.ToInt32(sqlDataReader["PartGolesV"]);

                                partidosgruposDTO.PartGrupo = sqlDataReader["PartGrupo"].ToString();
 


                                listaPartidosGrupo.Add(partidosgruposDTO);

                            }

                        }

                    }

                }

                sqlConnection.Close();
            }

            return listaPartidosGrupo;
        }
        */
        //----------------------------


        public static bool bajaPartidosGrupoHandler(DeletePartidosGruposDTO bajaPartidosGrupoBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false;

                var DeleteString = "DELETE FROM PartidosGrupos WHERE IDPartido = @IDPartido";


                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("IDPartido", System.Data.SqlDbType.Int) { Value = bajaPartidosGrupoBody.PartIDPartido});

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