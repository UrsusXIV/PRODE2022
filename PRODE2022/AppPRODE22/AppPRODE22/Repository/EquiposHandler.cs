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
                var InsertQuery = $"INSERT INTO Equipos (IDEquipo, EquipoNombre) Values ({altaEquipoBody.IdEquipo}, '{altaEquipoBody.EquipoNombre}')";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(InsertQuery, sqlConnection))
                {

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

                    SelectQuery = $"SELECT * FROM Equipos WHERE IdEquipo = {consultaEquipoBody.IdEquipo}";

                }

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(SelectQuery, sqlConnection))
                {

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

                var UpdateQuery = $"UPDATE Equipos SET EquipoNombre = '{modificacionEquipoBody.EquipoNombre}' WHERE IDEquipo = {modificacionEquipoBody.IdEquipo}";

                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(UpdateQuery, sqlConnection))
                {

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

                var DeleteString = $"DELETE FROM Equipos WHERE IdEquipo = {bajaEquipoBody.IdEquipo}";


                sqlConnection.Open();

                using(SqlCommand sqlCommand = new SqlCommand(DeleteString, sqlConnection))
                {

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
