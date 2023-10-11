using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication6.Pages
{
    public class thongtinkhachhangModel : PageModel
    {
        [BindProperty]
        public string CustomerName { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string CustomerAddress { get; set; }

        [BindProperty]
        public List<CartItem> ShoppingCartData { get; set; } // Đối tượng giỏ hàng

        public class CartItem
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var customerName = Request.Form["CustomerName"].ToString();
            var phoneNumber = Request.Form["PhoneNumber"].ToString();
            var customerAddress = Request.Form["CustomerAddress"].ToString();

            // Kết nối đến cơ sở dữ liệu
            string connectionString = "Data Source=DESKTOP-5TDVCUG;Initial Catalog=mystore;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tìm mã khách hàng cuối cùng
                string lastCustomerCode = GetLastCustomerCode(connection);

                // Tạo mã hóa đơn ngẫu nhiên
                string randomInvoiceCode = GenerateRandomInvoiceCode();

                // Tạo mã khách hàng mới (mã tăng dần)
                string newCustomerCode = GenerateNextCustomerCode(lastCustomerCode);

                // Thêm thông tin khách hàng mới vào cơ sở dữ liệu
                string customerSql = "INSERT INTO khachhang (maKH, hoTen, sdt, diaChi, maHD) VALUES (@CustomerCode, @CustomerName, @PhoneNumber, @CustomerAddress, @InvoiceCode);";

                using (SqlCommand customerCommand = new SqlCommand(customerSql, connection))
                {
                    customerCommand.Parameters.AddWithValue("@CustomerCode", newCustomerCode); // Sử dụng mã khách hàng mới
                    customerCommand.Parameters.AddWithValue("@CustomerName", customerName);
                    customerCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    customerCommand.Parameters.AddWithValue("@CustomerAddress", customerAddress);
                    customerCommand.Parameters.AddWithValue("@InvoiceCode", randomInvoiceCode); // Sử dụng mã hóa đơn ngẫu nhiên

                    customerCommand.ExecuteNonQuery();
                }

                // Thêm thông tin hóa đơn mới vào cơ sở dữ liệu


            }

            return RedirectToPage("index");
        }

        // Phương thức để tạo mã hóa đơn ngẫu nhiên
        private string GenerateRandomInvoiceCode()
        {
            // Sử dụng System.Random để tạo mã ngẫu nhiên (ví dụ: "INV" + số ngẫu nhiên)
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999); // Tạo số ngẫu nhiên từ 1000 đến 9999
            string randomCode = "INV" + randomNumber;
            return randomCode;
        }

        // Phương thức để tạo mã khách hàng tiếp theo (mã tăng dần)
        private string GenerateNextCustomerCode(string lastCode)
        {
            // Trích xuất số từ mã khách hàng cuối cùng
            string lastNumberPart = lastCode.Substring(2);

            // Chuyển số thành kiểu số nguyên
            if (int.TryParse(lastNumberPart, out int lastNumber))
            {
                // Tăng số lên 1 và định dạng lại chuỗi
                int nextNumber = lastNumber + 1;
                return "KH" + nextNumber.ToString("D3");
            }

            // Nếu không thể trích xuất số từ mã cũ, trả về một giá trị mặc định
            return "KH000";
        }


        // Phương thức để tìm mã khách hàng cuối cùng
        private string GetLastCustomerCode(SqlConnection connection)
        {
            string sql = "SELECT TOP 1 maKH FROM khachhang ORDER BY maKH DESC;";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                var lastCode = command.ExecuteScalar();
                if (lastCode != null)
                {
                    return lastCode.ToString();
                }

                // Trả về một giá trị mặc định nếu không có mã khách hàng nào trong cơ sở dữ liệu
                return "KH000";
            }
        }


    }
}
