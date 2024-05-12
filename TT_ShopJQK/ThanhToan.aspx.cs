using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using TT_ShopJQK.Class;

namespace TT_ShopJQK
{
    public partial class ThanhToan : System.Web.UI.Page
    {
        tbDisplayDataUtil du = new tbDisplayDataUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userlogin"] == null)
                {
                    txtname.Focus();
                    txtname.Enabled = false;
                    txtEmail.Enabled = false;
                }
                else
                {
                    string userid = Session["userlogin"].ToString();
                    string username = Session["fullName"].ToString();
                    txtname.Text = username;
                    txtname.Enabled = false;
                    txtEmail.Enabled = false;
                    txtEmail.Text = Session["useremail"].ToString();
                    txtPhone.Text = Session["phoneNum"].ToString();

                }
                showCart();
            }
        }

        private void showCart()
        {
            DataTable cart = new DataTable();
            if (Session["cart"] == null)
            {
                //Nếu chưa có giỏ hàng, tạo giỏ hàng thông qua DataTable với 4 cột chính
                //ID (Mã sản phẩm), Name (Tên sản phẩm)
                //Price (Giá tiền), Quantity (Số lượng)

                cart.Columns.Add("ProductId");
                cart.Columns.Add("Name");
                cart.Columns.Add("LastPrice");
                cart.Columns.Add("quantity");
                cart.Columns.Add("money");
                cart.Columns.Add("tax");
                cart.Columns.Add("Price");
                cart.Columns.Add("moneySum");
                //Sau khi tạo xong thì lưu lại vào session
                Session["cart"] = cart;
            }
            else
            {
                //Lấy thông tin giỏ hàng từ Session["cart"]
                cart = Session["cart"] as DataTable;
            }
          
            if (Session["add"] == "show")
            {
                string productid, name, price, quantity /*available*/;
                foreach (DataRow dr in cart.Rows)
                {
              
                        dr["moneySum"] = decimal.Parse(dr["tax"].ToString()) * decimal.Parse(dr["money"].ToString());
                       
                }
                grdcart.DataSource = Session["cart"];
                    grdcart.DataBind();
                

            }
        }

        protected void grdcart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = e.Row.FindControl("lbID") as Label;
                string a = lbl.Text;
                if (lbl.Text == "")
                {
                    e.Row.Visible = false;
                }
                else
                {
                    e.Row.Visible = true;
                }
            }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            if (Session["userlogin"] == null)
            {
                Response.Cookies.Add(new HttpCookie("returnUrl", Request.Url.PathAndQuery));
            }
            else
            {
                try
                {
                    HoaDon o = new HoaDon();
                    o.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString());
                    o.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToString());
                  
                    o.UserId = Convert.ToInt32(Session["userlogin"].ToString());
                    o.Status = 0;
                    DataTable cart = Session["cart"] as DataTable;
                    decimal total = 0;
                    foreach (DataRow datarow in cart.Rows)
                    {

                        decimal money = decimal.Parse(datarow["moneySum"].ToString());
                        total += money;
                    }
                    //lbTotal.Text = total.ToString("n0") + " VND";
                    o.ShippingCost = total;
                    o.Discount = 10000;
                    o.Tax = 3;
                 
                    int id = du.addOrder(o);

                    foreach (DataRow datarow in cart.Rows)
                    {

                        decimal money = decimal.Parse(datarow["moneySum"].ToString());
                        du.addItemmOrder(id, int.Parse(datarow["ProductId"].ToString()), int.Parse(datarow["quantity"].ToString()), decimal.Parse(datarow["LastPrice"].ToString()));
                        total += money;
                    }
                    /*    string sqlCon = @"Data Source=LAPTOP-P8OOSEKE\SQLEXPRESS;Initial Catalog=db_ECommerceShop;User Id=sa;Password=12345;";
                        SqlConnection con = new SqlConnection(sqlCon);
                        string sql = "select * from Orders where CreatedAt=@CreatedAt and UserId=@UserId";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("CreatedAt", o.CreatedAt);
                        cmd.Parameters.AddWithValue("UserId", o.UserId);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            Session["IDHD"] = int.Parse(dr["ID"].ToString());
                        }   

                        foreach (DataRow datarow in cart.Rows)
                        {
                            ChiTietHoaDon od = new ChiTietHoaDon();
                            od.IDHD = int.Parse(Session["IDHD"].ToString());
                            od.maSP = int.Parse(datarow["maSP"].ToString());
                            od.soLuong = int.Parse(datarow["quantity"].ToString());
                            od.donGia = int.Parse(datarow["donGia"].ToString());
                            du.addOrderDetail(od);
                        }
                        con.Close();*/

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "popup", "alert('Thanh toán thành công!')", true);
                    //Response.Redirect(url: "ListCategories.aspx");
                    Session.Remove("cart");
                    Session.Remove("add");
                }
                catch (Exception e1)
                {

                    msg.Text = "Something wrong: " + e1.Message.ToString();
                }
            }


        }
        
    }
}