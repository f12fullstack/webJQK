using System;
using System.Net.Mail;
using System.Net;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

namespace TT_ShopJQK
{
    public partial class forgetpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            form2.Visible = false;
        }
        protected bool CheckUser(string username, string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db_ECommerceShopConnectionString"].ConnectionString; ;
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND UserEmail = @UserEmail";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Tạo và mở đối tượng SqlCommand để thực thi câu lệnh SQL
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@UserEmail", email);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void SendEmail(string toEmail , int otp)
        {
            string fromEmail = "ngocrongj10@gmail.com"; // Địa chỉ email của người gửi
            string password = "ewvhgpdwwyvkhqzj"; // Mật khẩu của tài khoản Gmail

            MailMessage PassRecMail = new MailMessage(fromEmail, toEmail);

            PassRecMail.Body = "Xin chào " + toEmail +
            ",<br /><br />Chúng tôi QJKShop đã nhận được yêu cầu cấp lại mật khẩu cho tài khoản shop của chúng tôi.<br />" +
            "Mã OTP dùng một lần của bạn: " + otp + "<br />" +
            "Hết hạn trong 5 phút tới. "+ "<br />" +
            "Nếu không yêu cầu mã này thì bạn có thể bỏ qua email này một cách an toàn.<br />" +
            "Xin cảm ơn<br />JQKShop<br /><a href='https://localhost:44309/TrangChu.aspx'>https://localhost:44309/TrangChu.aspx</a>";


            PassRecMail.IsBodyHtml = true;
            PassRecMail.Subject = "Quên mật khẩu JQKShop";
            PassRecMail.Priority = MailPriority.High;

            SmtpClient SMTP = new SmtpClient("smtp.gmail.com", 587);
            SMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
            SMTP.UseDefaultCredentials = false;
            SMTP.Credentials = new NetworkCredential(fromEmail, password); // Cung cấp thông tin xác thực
            SMTP.EnableSsl = true;
            SMTP.Send(PassRecMail);


        }

        protected int upOTP(string username, string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db_ECommerceShopConnectionString"].ConnectionString;
            ;
            string query = "UPDATE Users SET Otp = @Otp, TimeOtp = @TimeOtp WHERE Username = @Username AND UserEmail = @UserEmail";
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            DateTimeOffset currentTime = DateTimeOffset.UtcNow;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@UserEmail", email);
                        cmd.Parameters.AddWithValue("@Otp", randomNumber);
                        cmd.Parameters.AddWithValue("@TimeOtp", currentTime);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    } 

                    con.Close();
                    return randomNumber;
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
                return 0;
            }
           
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if(CheckUser(txtusername.Text, txtEmail.Text))
            {

                int otp = upOTP(txtusername.Text, txtEmail.Text);
                SendEmail(txtEmail.Text, otp);
                form1.Visible = false;
                form2.Visible = true;
            }
            else
            {
                string script = "alert('Thông tin tài khoản chưa chính xác!\\nVui lòng thử lại.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert", script, true);
            }
        }

        protected bool CheckOtp(string otp)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db_ECommerceShopConnectionString"].ConnectionString;
            string query = "SELECT COUNT(*) FROM Users WHERE Otp = @Otp AND TimeOtp < GETDATE()";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Tạo và mở đối tượng SqlCommand để thực thi câu lệnh SQL
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Otp", otp);
               
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    con.Close();
                    return count > 0;
                }
              
            }
        
        }

        protected void upPass(string username, string email , string pass)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db_ECommerceShopConnectionString"].ConnectionString;
            string query = "UPDATE Users SET UserPassword = @UserPassword  WHERE Otp = @Otp AND TimeOtp < GETDATE();";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        /*  cmd.Parameters.AddWithValue("@Username", username);
                          cmd.Parameters.AddWithValue("@UserEmail", email);*/
                        cmd.Parameters.AddWithValue("@UserPassword", pass);
                        cmd.Parameters.AddWithValue("@Otp", username);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public bool checkInput()
        {
            bool check = true;
            if(txtpassnew.Text == "")
            {
                errorpassnew.Text = "Không được để trống!";
                check = false;
            }
            if(txtpassconfig.Text == "")
            {
                errorpasscf.Text = "Không được để trống!";
                check = false;
            }
            else
            {
                if (txtpassconfig.Text != txtpassnew.Text)
                {
                    errorpasscf.Text = "Mật khẩu xác nhận không khớp!";
                    check = false;
                }
            }
            return check;
        }
        protected void btnlogin_ClickReset(object sender, EventArgs e)
        {
            if (CheckOtp(txtotp.Text))
            {
                if (checkInput())
                {
                    upPass(txtotp.Text, txtEmail.Text, txtpassnew.Text);
                    Console.WriteLine(txtEmail.Text);
                    string script = "alert('Thông tin tài khoản  chính xác!\\nVui lòng thử lại.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert", script, true);
                    Response.Redirect("DangNhap.aspx");
                }
                
            }
            else
            {
                string script = "alert('Thông tin tài khoản chưa chính xác!\\nVui lòng thử lại.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert", script, true);
            }
        }
    }
}