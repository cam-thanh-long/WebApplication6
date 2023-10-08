using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication6.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo client = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=NOTTODAY\\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM thuocsau WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                client.id = "" + reader.GetInt32(0);
                                client.name = reader.GetString(1);
                                client.des = reader.GetString(2);
                                client.price = reader.GetString(3);
                                client.quantity = reader.GetInt32(4);
                                client.img = reader.GetString(5);
                                client.mfg = reader.GetString(6);
                                client.exp = reader.GetString(7);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost() 
        {
            client.id = Request.Form["id"];
            client.name = Request.Form["name"];
            client.des = Request.Form["des"];
            client.price = Request.Form["price"];
            if (int.TryParse(Request.Form["quantity"], out int quantity))
            {
                client.quantity = quantity;
            }
            else
            {
                errorMessage = "Số lượng không hợp lệ";
                return;
            }
            client.img = Request.Form["img"];
            client.mfg = Request.Form["mfg"];
            client.exp = Request.Form["exp"];

            if (client.name.Length == 0 || client.des.Length == 0 ||
                client.price.Length == 0 || client.img.Length == 0 ||
                client.mfg.Length == 0 || client.exp.Length == 0)
            {
                errorMessage = "Không được để trống";
                return;
            }

            try
            {
                string connectionString = "Data Source=NOTTODAY\\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE thuocsau SET name = @name, des = @des, price = @price, quantity = @quantity, img = @img, mfg = @mfg, exp = @exp WHERE id=@id;";
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", client.name);
                        command.Parameters.AddWithValue("@des", client.des);
                        command.Parameters.AddWithValue("@price", client.price);
                        command.Parameters.AddWithValue("@quantity", client.quantity);
                        command.Parameters.AddWithValue("@img", client.img);
                        command.Parameters.AddWithValue("@mfg", client.mfg);
                        command.Parameters.AddWithValue("@exp", client.exp);
                        command.Parameters.AddWithValue("@id", client.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clients");
        }
    }
}
