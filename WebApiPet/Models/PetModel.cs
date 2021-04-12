using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPet.Models
{
    public class PetModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Breed { get; set; }

        public int Age { get; set; }

        public string Picture { get; set; }


        public List<PetModel> GetAll(string connectionString)
        {
            List<PetModel> list = new List<PetModel>();
            try
            {
                using(MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Pet";
                    using (MySqlCommand cmd = new MySqlCommand(tsql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                list.Add(new PetModel
                                {
                                    ID = int.Parse(reader["ID"].ToString()),
                                    Name = reader["Name"].ToString(),
                                    Breed = reader["Breed"].ToString(),
                                    Age = int.Parse(reader["Age"].ToString()),
                                    Picture = reader["Picture"].ToString()
                                }); 
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return list;
        }

        public PetModel Get(string connectionString, int id)
        {
            PetModel obj = new PetModel();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Pet WHERE ID = @ID";
                    using (MySqlCommand cmd = new MySqlCommand(tsql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                obj = new PetModel
                                {
                                    ID = int.Parse(reader["ID"].ToString()),
                                    Name = reader["Name"].ToString(),
                                    Breed = reader["Breed"].ToString(),
                                    Age = int.Parse(reader["Age"].ToString()),
                                    Picture = reader["Picture"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return obj;
        }

        public ApiResponse Insert(string connectionString)
        {
            try
            {
                object newID;
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string tsql = "INSERT INTO Pet (Name, Breed, Age, Picture) VALUES(@Name, @Breed, @Age, @Picture); SELECT LAST_INSERT_ID();";
                    using (MySqlCommand cmd = new MySqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Breed", Breed);
                        cmd.Parameters.AddWithValue("@Age", Age);
                        cmd.Parameters.AddWithValue("@Picture", Picture);
                        newID = cmd.ExecuteScalar();
                        if (newID != null && newID.ToString().Length > 0)
                        {
                            return new ApiResponse 
                            {
                                IsSuccess = true,
                                Message = "La mascota fue agregada con éxito",
                                Result = newID
                            };
                        }
                        else
                        {
                            return new ApiResponse
                            {
                                IsSuccess = false,
                                Message = $"Se generó un error al insertar una mascota",
                                Result = null
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al insertar una mascota ({ex.Message})",
                    Result = null
                };
            }
        }

        public ApiResponse Update(string connectionString)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string tsql = "UPDATE Pet SET Name = @Name, Breed = @Breed, Age = @Age, Picture = @Picture WHERE ID = @ID;";
                    using (MySqlCommand cmd = new MySqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Breed", Breed);
                        cmd.Parameters.AddWithValue("@Age", Age);
                        cmd.Parameters.AddWithValue("@Picture", Picture);
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.ExecuteNonQuery();
                        return new ApiResponse
                        {
                            IsSuccess = true,
                            Message = "La mascota fue actualizada con éxito",
                            Result = ID
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al actualizar una mascota ({ex.Message})",
                    Result = null
                };
            }
        }

        public ApiResponse Delete(string connectionString, int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string tsql = "DELETE FROM Pet WHERE ID = @ID;";
                    using (MySqlCommand cmd = new MySqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                        return new ApiResponse
                        {
                            IsSuccess = true,
                            Message = "La mascota fue eliminada con éxito",
                            Result = ID
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al eliminar una mascota ({ex.Message})",
                    Result = null
                };
            }
        }

    }
}
