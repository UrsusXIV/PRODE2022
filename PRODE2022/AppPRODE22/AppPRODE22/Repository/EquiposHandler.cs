using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class EquiposHandler : DBHandler
    {
        // Metodo para dar de alta a los equipos.-
        public static bool altaEquipoHandler(PostEquipoDTO altaEquipoBody)
        {
            bool insert = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var InsertQuery = "INSERT INTO Equipos (IDEquipo, EquipoNombre) Values (@IDEquipo, @EquipoNombre)";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = altaEquipoBody.IdEquipo });

                    sqlCommand.Parameters.Add(new SqlParameter("EquipoNombre", System.Data.SqlDbType.VarChar) { Value = altaEquipoBody.EquipoNombre });

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

        // ----------------------POST FIXED.-----------------------------------

        public static List<GetEquipoDTO> consultaEquiposHandler(GetEquipoDTO consultaEquipoBody)
        {
            List<GetEquipoDTO> listaEquipos = new List<GetEquipoDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if (consultaEquipoBody.IdEquipo == 0)
                {
                    SelectQuery = "SELECT * FROM Equipos";

                }

                else
                {

                    SelectQuery = "SELECT * FROM Equipos WHERE IDEquipo = @IDEquipo";

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) {Value = consultaEquipoBody.IdEquipo});
                    
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var equipoDTO = new GetEquipoDTO();

                                equipoDTO.IdEquipo = Convert.ToInt32(sqlDataReader["IdEquipo"]);

                                equipoDTO.EquipoNombre = sqlDataReader["EquipoNombre"].ToString();

                                listaEquipos.Add(equipoDTO);

                            }

                        }

                    }

                }

                sqlConnection.Close();
            }

            return listaEquipos;
        }

        //----------------------------

        public static bool modificacionEquiposHandler(PutEquipoDTO modificacionEquipoBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false;

                var UpdateQuery = "UPDATE Equipos SET EquipoNombre = @EquipoNombre WHERE IDEquipo = @IDEquipo";

                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("EquipoNombre", System.Data.SqlDbType.VarChar) { Value = modificacionEquipoBody.EquipoNombre});

                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = modificacionEquipoBody.IdEquipo });

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

        public static bool bajaEquipoHandler(DeleteEquipoDTO bajaEquipoBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false;

                var DeleteString = "DELETE FROM Equipos WHERE IDEquipo = @IDEquipo";


                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("IDEquipo", System.Data.SqlDbType.Int) { Value = bajaEquipoBody.IdEquipo });

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
