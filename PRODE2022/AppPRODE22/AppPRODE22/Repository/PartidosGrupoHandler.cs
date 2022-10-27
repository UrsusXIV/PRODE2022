using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;

//TODO: AJUSTAR TODO CONFORME AL CRUD 

namespace AppPRODE22.Repository
{
    public class SedeHandler : DBHandler
    {
        // Metodo para dar de alta a las sedes.-
        public static bool altaSedeHandler(PostSedeDTO altaSedeBody)
        {
            bool insert = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var InsertQuery = "INSERT INTO Sedes (SedeID, SedeNombre) Values (@SedeID, @SedeNombre)";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("SedeID", System.Data.SqlDbType.Int) { Value = altaSedeBody.SedeID });

                    sqlCommand.Parameters.Add(new SqlParameter("SedeNombre", System.Data.SqlDbType.VarChar) { Value = altaSedeBody.SedeNombre });

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

        public static List<GetSedeDTO> consultaSedeHandler(GetSedeDTO consultaSedeBody)
        {
            List<GetSedeDTO> listaSedes = new List<GetSedeDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if (consultaSedeBody.SedeID == 0)
                {
                    SelectQuery = "SELECT * FROM Sedes";

                }

                else
                {

                    SelectQuery = "SELECT * FROM Sedes WHERE SedeID = @SedeID";

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("SedeID", System.Data.SqlDbType.Int) {Value = consultaSedeBody.SedeID});
                    
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var sedeDTO = new GetSedeDTO();

                                sedeDTO.SedeID = Convert.ToInt32(sqlDataReader["SedeID"]);

                                sedeDTO.SedeNombre = sqlDataReader["SedeNombre"].ToString();

                                listaSedes.Add(sedeDTO);

                            }

                        }

                    }

                }

                sqlConnection.Close();
            }

            return listaSedes;
        }

        //----------------------------

        public static bool modificacionSedeHandler(PutSedeDTO modificacionSedeBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false;

                var UpdateQuery = "UPDATE Sedes SET SedeNombre = @SedeNombre WHERE SedeID = @SedeID";

                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("SedeNombre", System.Data.SqlDbType.VarChar) { Value = modificacionSedeBody.SedeNombre});

                    sqlCommand.Parameters.Add(new SqlParameter("SedeID", System.Data.SqlDbType.Int) { Value = modificacionSedeBody.SedeID });

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

        public static bool bajaSedeHandler(DeleteSedeDTO bajaSedeBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false;

                var DeleteString = "DELETE FROM Sedes WHERE SedeID = @SedeID";


                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("SedeID", System.Data.SqlDbType.Int) { Value = bajaSedeBody.SedeID});

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