using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication6.Pages
{
    public class ClientsModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-5TDVCUG;Initial Catalog=mystore;Integrated Security=True";
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
                                ClientInfo client = new ClientInfo();
                                client.id = "" + reader.GetInt32(0);
                                client.name = reader.GetString(1);
                                client.des = reader.GetString(2);
                                client.price = reader.GetString(3);
                                client.quantity = reader.GetInt32(4);
                                client.img = reader.GetString(5);
                                client.mfg = reader.GetString(6);
                                client.exp = reader.GetString(7);

                                listClients.Add(client);
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

    public class ClientInfo
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
