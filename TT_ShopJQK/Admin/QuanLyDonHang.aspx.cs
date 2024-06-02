using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;

namespace TT_ShopJQK.Admin
{
    public partial class QuanLyDonHang : System.Web.UI.Page
    {
        DataUtil data = new DataUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hienthi();
            }
        }
        public void hienthi()
        {
            grOrders.DataSource = data.dsHD();
            DataBind();
        }
        protected void Xoa_click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "xoa")
            {
                int b = Convert.ToInt16(e.CommandArgument);
                data.XoaHD(b);
                hienthi();
            }
        }
        protected void Sua_click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "sua")
            {
                int b = Convert.ToInt16(e.CommandArgument);
               
                Session["us"] = b;
                Response.Redirect("DuyetDon.aspx");
            }
        }
        protected void lbView_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int m = Convert.ToInt32(e.CommandArgument);
                Session["IDHD"] = m;

                if (Session["IDHD"] != null)
                {
                    grOrders.Visible = false;
                    string IDHD = Session["IDHD"].ToString();
                    showdetail(Convert.ToInt32(IDHD));
                    grvDetails.Visible = true;
                    lbDetailTitle.Text = "Chi tiết order #" + m;

                }
            }

        }
        private void showdetail(int IDHD)
        {
            //grdShoes.DataSource = data.lsProduct();
            //grdShoes.DataBind();
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-P8OOSEKE\SQLEXPRESS;Initial Catalog=db_ECommerceShop;User Id=sa;Password=12345;");
            string query = "SELECT o.OrderID, oi.ProductID, oi.Quantity, oi.Price, p.ProductId, p.Name, m.Url " +
                         "FROM Orders o " +
                         "JOIN OrderItems oi ON o.OrderID = oi.OrderID " +
                         "JOIN Products p ON oi.ProductID = p.ProductId " +
                         "JOIN(" +
                         "   SELECT ProductId, Url, ROW_NUMBER() OVER(PARTITION BY ProductId ORDER BY(SELECT NULL)) AS RowNum " +
                         "   FROM ProductImages " +
                         "     ) m ON p.ProductId = m.ProductId AND m.RowNum = 1 " +
                         "WHERE o.OrderID = @OrderID; ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("OrderID", IDHD);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grvDetails.DataSource = dt;
            grvDetails.DataBind();
        }
        protected void grOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string days = e.Row.Cells[7].Text;
                if (days == "0")
                {
                    e.Row.Cells[7].Text = "Chưa thanh toán";
                }
                else if (days == "1")
                {
                    e.Row.Cells[7].Text = "Đã thanh toán";
                }
            }

        }
    }
}