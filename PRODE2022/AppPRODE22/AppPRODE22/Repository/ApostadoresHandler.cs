using System.Data.SqlClient;

using AppPRODE22.Controllers.DTOs;

using AppPRODE22.Models;

namespace AppPRODE22.Repository
{
    public class ApostadoresHandler : DBHandler
    {
        // Metodo para dar de alta a las sedes.-
        public static bool altaApostadoresHandler(PostApostadoresDTO altaApostadoresBody)
        {
            bool insert = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var InsertQuery = "INSERT INTO Apostadores (IDApostador, ApostMail, ApostNombre, ApostPuntos) Values (@IDApostador, @ApostMail, @ApostNombre, @ApostPuntos)";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = altaApostadoresBody.IDApostador });

                    sqlCommand.Parameters.Add(new SqlParameter("ApostPuntos", System.Data.SqlDbType.Int) { Value = 0 }); // Se inicializa obligatoriamente en 0 en el alta.

                    sqlCommand.Parameters.Add(new SqlParameter("ApostMail", System.Data.SqlDbType.VarChar) { Value = altaApostadoresBody.ApostMail });

                    sqlCommand.Parameters.Add(new SqlParameter("ApostNombre", System.Data.SqlDbType.VarChar) { Value = altaApostadoresBody.AposNombre });

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

       /* public static List<GetApostadoresDTO> consultaApostadoresHandler(GetApostadoresDTO consultaApostadoresBody)
        {
            List<GetApostadoresDTO> listaApostadores = new List<GetApostadoresDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if (consultaApostadoresBody.IDApostador == 0)
                {
                    SelectQuery = "SELECT IDApostador, ApostNombre, ApostPuntos FROM Apostadores"; // El E-Mail no se trae por seguridad.

                }

                else
                {

                    SelectQuery = "SELECT IDApostador, ApostNombre, ApostPuntos FROM Apostadores WHERE IDApostador = @IDApostador";

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) {Value = consultaApostadoresBody.IDApostador});
                    
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {

                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                var apostadoresDTO = new GetApostadoresDTO();

                                apostadoresDTO.IDApostador = Convert.ToInt32(sqlDataReader["IDApostador"]);

                                apostadoresDTO.AposPuntos = Convert.ToInt32(sqlDataReader["ApostPuntos"]);

                                apostadoresDTO.AposNombre = sqlDataReader["ApostNombre"].ToString();

                                listaApostadores.Add(apostadoresDTO);

                            }

                        }

                    }

                }

                sqlConnection.Close();
            }

            return listaApostadores;
        }
         */

        public static ApostadoresResponse consultaApostadoresHandler(GetApostadoresDTO consultaApostadoresBody)
        {
            ApostadoresResponse response = new ApostadoresResponse();

            response.Apostadores = new List<GetApostadoresDTO>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var SelectQuery = string.Empty;

                if(consultaApostadoresBody.IDApostador == 0)
                {
                    SelectQuery = "SELECT IDApostador, ApostNombre, ApostPuntos, ApostMail FROM Apostadores";
                }

                else
                {
                    SelectQuery = "SELECT IDApostador, ApostNombre, ApostPuntos, ApostMail FROM Apostadores WHERE IDApostador = @IDApostador";
                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) {Value = consultaApostadoresBody.IDApostador });
                    
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {

                                var apostadoresDTO = new GetApostadoresDTO();

                                apostadoresDTO.IDApostador = Convert.ToInt32(sqlDataReader["IDApostador"]);

                                apostadoresDTO.AposNombre = sqlDataReader["ApostNombre"].ToString();

                                apostadoresDTO.AposPuntos = Convert.ToInt32(sqlDataReader["ApostPuntos"]);

                                apostadoresDTO.ApostMail = sqlDataReader["ApostMail"].ToString();

                                response.Apostadores.Add(apostadoresDTO);


                            }
                        }
                    }
                }

                sqlConnection.Close();
            }

            return response;
        }
        //----------------------------

        public static bool modificacionApostadoresHandler(PutApostadoresDTO modificacionApostadoresBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool update = false;

                var UpdateQuery = "UPDATE Apostadores SET ApostMail = @ApostMail, ApostNombre = @ApostNombre WHERE IDApostador = @IDApostador";

                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("ApostMail", System.Data.SqlDbType.VarChar) { Value = modificacionApostadoresBody.ApostMail});

                    sqlCommand.Parameters.Add(new SqlParameter("ApostNombre", System.Data.SqlDbType.VarChar) { Value = modificacionApostadoresBody.AposNombre });

                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = modificacionApostadoresBody.IDApostador });

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

        public static bool bajaApostadoresHandler(DeleteApostadoresDTO bajaApostadoresBody)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                bool delete = false;

                var DeleteString = "DELETE FROM Apostadores WHERE IDApostador = @IDApostador";


                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("IDApostador", System.Data.SqlDbType.Int) { Value = bajaApostadoresBody.IDApostador });

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