using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication6.Pages
{
    public class ProductDetailModel : PageModel
    {
        public List<ProductInfo> listProducts = new List<ProductInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=NOTTODAY\\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
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
                                ProductInfo product = new ProductInfo();
                                product.id = "" + reader.GetInt32(0);
                                product.name = reader.GetString(1);
                                product.des = reader.GetString(2);
                                product.price = reader.GetString(3);
                                product.quantity = reader.GetInt32(4);
                                product.img = reader.GetString(5);
                                product.mfg = reader.GetString(6);
                                product.exp = reader.GetString(7);

                                listProducts.Add(product);
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

    public class ProductInfo
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
