using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication6.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<ShowProduct> listShowProducts = new List<ShowProduct>();
        public void OnGet()
        {
            try
            {
                //tự tạo xong phải sửa lại đường dẫn source phía dưới này
                string connectionString = "Data Source=X-PC\\MSSQLSERVER02;Initial Catalog=webstore;Integrated Security=True;Pooling=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM thuocsau";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ShowProduct showpro = new ShowProduct();
                                showpro.id = "" + reader.GetInt32(0);
                                showpro.name = reader.GetString(1);
                                showpro.des = reader.GetString(2);
                                showpro.price = reader.GetString(3);
                                showpro.quantity = reader.GetInt32(4);
                                showpro.img = reader.GetString(5);
                                showpro.mfg = reader.GetString(6);
                                showpro.exp = reader.GetString(7);

                                listShowProducts.Add(showpro);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


    }

    public class ShowProduct
    {
        public string id;
        public string name;
        public string des;
        public string price;
        public int quantity;
        public string img;
        public string mfg;
        public string exp;

    }

}