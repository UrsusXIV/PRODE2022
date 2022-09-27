using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class GruposApuestasHandler : DBHandler
    {
        // Metodo para dar de alta a los grupos de apostadores.-
        public static bool altaGruposApuestasHandler(PostGruposApuestasDTO altaGruposApuestasBody)
        {
            bool insert = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var InsertQuery = "INSERT INTO GrupoApuestas (IDGruposAp, GruposApDescripcion) Values (@IDGruposAp, @GruposApDescripcion)";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) { Value = altaGruposApuestasBody.IDGruposAp });

                    sqlCommand.Parameters.Add(new SqlParameter("GruposApDescripcion", System.Data.SqlDbType.VarChar) { Value = altaGruposApuestasBody.GrupoApDescripcion });

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

        public static List<GetGruposApuestasDTO> consultaGruposApuestasHandler(GetGruposApuestasDTO consultaGruposApuestasBody)
        {
            List<GetGruposApuestasDTO> listaGruposAp = new List<GetGruposApuestasDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if (consultaGruposApuestasBody.IDGruposAp == 0)
                {
                    SelectQuery = "SELECT * FROM GrupoApuestas";

                }

                else
                {

                    SelectQuery = "SELECT * FROM GrupoApuestas WHERE IDGruposAp = @IDGruposAp";

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) {Value = consultaGruposApuestasBody.IDGruposAp});
                    
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var gruposApDTO = new GetGruposApuestasDTO();

                                gruposApDTO.IDGruposAp = Convert.ToInt32(sqlDataReader["IDGruposAp"]);

                                gruposApDTO.GrupoApDescripcion = sqlDataReader["GruposApDescripcion"].ToString();

                                listaGruposAp.Add(gruposApDTO);

                            }

                        }

                    }

                }

                sqlConnection.Close();
            }

            return listaGruposAp;
        }

        //----------------------------

        public static bool modificacionGruposApuestasHandler(PutGruposApuestasDTO modificacionGruposApuestasBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false;

                var UpdateQuery = "UPDATE GrupoApuestas SET GruposApDescripcion = @GruposApDescripcion WHERE IDGruposAp = @IDGruposAp";

                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("GruposApDescripcion", System.Data.SqlDbType.VarChar) { Value = modificacionGruposApuestasBody.GrupoApDescripcion });

                    sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) { Value = modificacionGruposApuestasBody.IDGruposAp });

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

        public static bool bajaGruposApuestasHandler(DeleteGruposApuestasDTO bajaGruposApuestasBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false;

                var DeleteString = "DELETE FROM GrupoApuestas WHERE IDGruposAp = @IDGruposAp";


                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("IDGruposAp", System.Data.SqlDbType.Int) { Value = bajaGruposApuestasBody.IDGruposAp });

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


