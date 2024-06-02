using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace TT_ShopJQK
{
    public partial class DangNhap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbID.Visible = false;
            txtusername.Attributes.Add("placeholder", "Tên tài khoản");
            txtpassword.Attributes.Add("placeholder", "Mật khẩu");
            lbMessage.Visible = false;
        }

      
        public void checkUser()
        {
            string sqlCon = ConfigurationManager.ConnectionStrings["db_ECommerceShopConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(sqlCon);
            con.Open();
            string checkuser = "select * from Users where UserName like '" + txtusername.Text.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(checkuser, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["UserName"].ToString() == txtusername.Text && dr["UserPassword"].ToString() == txtpassword.Text &&
                    (dr["UserRole"].ToString()) == "admin")
                {
                    Users u = new Users();
                    u.IDDN = (int)(dr["UserId"]);
                    u.tenDN = (string)dr["UserName"];
                    lbID.Text = u.IDDN.ToString();
                    string name = u.tenDN;
                    Session["login"] = lbID.Text;
                    Session["name"] = name;
                    Response.Redirect(url: "~/Admin/HomeAdmin.aspx");
                }
                else if (dr["UserName"].ToString() == txtusername.Text && dr["UserPassword"].ToString() == txtpassword.Text &&
                    (dr["UserRole"].ToString()) == "user")
                {
                    Users u = new Users();
                    u.tenDN = (string)dr["UserName"];
                    u.IDDN = (int)(dr["UserId"]);
                    lbID.Text = u.IDDN.ToString();
                    string name = u.tenDN;
                    Session["userlogin"] = lbID.Text;
                    Session["useremail"] = (string)dr["UserEmail"];
                    Session["username"] = name;
                    Session["fullName"] = (string)dr["FullName"];
                    Session["phoneNum"] = (string)dr["PhoneNum"];
                    //Response.Redirect(url: "~/TrangChu.aspx");
                    HttpCookie returnCookie = Request.Cookies["returnUrl"];
                    if ((returnCookie == null) || string.IsNullOrEmpty(returnCookie.Value))
                    {
                        Response.Redirect("TrangChu.aspx");
                    }
                    else
                    {
                        HttpCookie deleteCookie = new HttpCookie("returnUrl");
                        deleteCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(deleteCookie);
                        Response.Redirect(returnCookie.Value);
                    }
                }

                
            }

            con.Close();
        }


        public void SendEmail(string emailSubject, string emailBody, string toEmail)
        {
            string fromEmail = "ngocrongj10@gmail.com"; // Địa chỉ email của người gửi
            string password = "ewvhgpdwwyvkhqzj"; // Mật khẩu của tài khoản Gmail

            Random random = new Random();

            // Tạo một số ngẫu nhiên có 6 chữ số
            int randomNumber = random.Next(100000, 999999);
            MailMessage PassRecMail = new MailMessage(fromEmail, toEmail);

            PassRecMail.Body = "Xin chào " + toEmail +
            ",<br /><br />Chúng tôi QJKShop đã nhận được yêu cầu cấp lại mật khẩu cho tài khoản shop của chúng tôi.<br />" +
            "Mã dùng một lần của bạn: " + randomNumber + "<br />" +
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


        protected void lnkForgotPassword_Click(object sender, EventArgs e)
        {
            // Hiển thị modal hoặc chuyển hướng đến trang mới
            // Ví dụ: Response.Redirect("ForgotPassword.aspx");
            /*SendEmail("truong", "hello", "chinh2k2zzz@gmail.com");*/
            Response.Redirect("forgetpassword.aspx");
        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            checkUser();
        }
    }
}