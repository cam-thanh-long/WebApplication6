using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication6.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo client = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
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

            //add
            try
            {
                string connectionString = "Data Source=X-PC\\MSSQLSERVER02;Initial Catalog=webstore;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO thuocsau (name, des, price, quantity, img, mfg, exp) VALUES (@name, @des, @price, @quantity, @img, @mfg, @exp);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", client.name);
                        command.Parameters.AddWithValue("@des", client.des);
                        command.Parameters.AddWithValue("@price", client.price);
                        command.Parameters.AddWithValue("@quantity", client.quantity);
                        command.Parameters.AddWithValue("@img", client.img);
                        command.Parameters.AddWithValue("@mfg", client.mfg);
                        command.Parameters.AddWithValue("@exp", client.exp);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            client.name = "";
            client.des = "";
            client.price = "";
            client.quantity = 0;
            client.img = "";
            client.mfg = "";
            client.exp = "";
            successMessage = "Thêm mới thành công";

            Response.Redirect("/Clients");
        }
    }
}
