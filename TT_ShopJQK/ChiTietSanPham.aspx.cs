using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;

namespace TT_ShopJQK
{
    public partial class ChiTietSanPham : System.Web.UI.Page
    {
        DataUtil data = new DataUtil();
        public int ProductId;
        public string anh;
        public int rating = 5;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hiddenRating.Value = "5";
                // Liên kết dữ liệu với Repeater khi trang được load lần đầu
                ProductId = int.Parse(Request.QueryString[0].ToString());
                BindComments(ProductId);
                detailProduct w = data.layra1sp(ProductId);
                LbtenSP.Text = w.Name.ToString();
                lbdonGia.Text = w.LastPrice.ToString();
                LbdanhMuc.Text = w.NameCategoryId;
                LbthongTinSp.Text = w.Description;
                LbkhuyenMai.Text = w.Price.ToString();
                Label1.Text = w.NameCategoryId;
                lbQuantityStatus.Text = w.quantity == 0 ? "Hết hàng" : "Còn " + w.quantity.ToString() + " sản phẩm";
                anhsp.ImageUrl = w.Url[0];
                Image1.ImageUrl = w.Url[1];
                Image2.ImageUrl = w.Url[2];
                myLink.HRef = w.Url[0];
                A1.HRef = w.Url[1];
                A2.HRef = w.Url[2];
            }

        }


        private void BindComments(int id)
        {
            string connectionString = @"Data Source=LAPTOP-P8OOSEKE\SQLEXPRESS;Initial Catalog=db_ECommerceShop;User Id=sa;Password=12345;"; // Thay thế bằng chuỗi kết nối của bạn
            string query = "SELECT Comments.*, Users.FullName FROM Comments INNER JOIN Users ON Comments.Author = Users.UserId WHERE Comments.ProductId = @ProductId;"; // Thay thế bằng câu truy vấn SQL của bạn

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", id);
                  
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    commentRepeater.DataSource = reader;
                    commentRepeater.DataBind();
                }
                con.Close();
            }
        }
        protected string GetStarRating(object rating)
        {
            int starCount = Convert.ToInt32(rating);
            StringBuilder starsHtml = new StringBuilder();
            for (int i = 0; i < starCount; i++)
            {
                starsHtml.Append("<i class='fa fa-star'></i>");
            }
            for (int i = starCount; i < 5; i++)
            {
                starsHtml.Append("<i class='fa fa-star-o'></i>");
            }
            return starsHtml.ToString();
        }
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (txtOrderQuantity.Text != "0")
            {
                Session["ProductId"] = int.Parse(Request.QueryString[0].ToString());

                Session["LastPrice"] = lbdonGia.Text;

                Session["Price"] = LbkhuyenMai.Text;
                Session["Name"] = LbtenSP.Text;

                Session["Url"] = anhsp.ImageUrl;

                Session["quantity"] = txtOrderQuantity.Text;
                Session["tax"] = "0.03";
                string quantity = txtOrderQuantity.Text;

                Session["add"] = "add";
                //Response.Redirect("GioHang.aspx");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "message",
                        "alert('Bạn vừa thêm sản phẩm " + LbtenSP.Text + " vào giỏ hàng!');location.href = 'GioHang.aspx';", true);
            }else
            {
               /* lbdonGia.Text = hiddenRating.Value.ToString();*/
            }
        }
        protected void Comment_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-P8OOSEKE\SQLEXPRESS;Initial Catalog=db_ECommerceShop;User Id=sa;Password=12345;";
            string sql = "INSERT INTO Comments (Content, Star, Author, time, ProductId) " +
                          "VALUES (@Content, @Star, @Author, @time, @ProductId)";
            DateTimeOffset currentTime = DateTimeOffset.UtcNow;
           int idU = Session["userlogin"] == null ? 23 : int.Parse(Session["userlogin"].ToString());
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmds = new SqlCommand(sql, con))
                {
                    cmds.Parameters.AddWithValue("@Content", txtMessage.Text);
                    cmds.Parameters.AddWithValue("@Star", hiddenRating.Value);
                    cmds.Parameters.AddWithValue("@Author",idU );
                    cmds.Parameters.AddWithValue("@time", currentTime);
                    cmds.Parameters.AddWithValue("@ProductId", int.Parse(Request.QueryString[0].ToString()));
                    con.Open();
                    cmds.ExecuteNonQuery();
                }
                con.Close();
            }
            BindComments(int.Parse(Request.QueryString[0].ToString()));


        }
    }
}
