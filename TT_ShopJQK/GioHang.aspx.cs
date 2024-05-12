using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;
namespace TT_ShopJQK
{
    public partial class GioHang : System.Web.UI.Page
    {
        DataUtil xuly = new DataUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["productdelete"] = null;
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
                cart.Columns.Add("Price");
                cart.Columns.Add("Url");
                cart.Columns.Add("quantity");
                cart.Columns.Add("money");
                cart.Columns.Add("tax");
                cart.Columns.Add("moneySum");
                //Sau khi tạo xong thì lưu lại vào session
                Session["cart"] = cart;
            }
            else
            {
                //Lấy thông tin giỏ hàng từ Session["cart"]
                cart = Session["cart"] as DataTable;
            }
            //if (!String.IsNullOrEmpty(Request.QueryString["action"]))
            //{
            //    if (Request.QueryString["action"] == "add")
            //    {
            if (Session["add"] == "add")
            {
                
                string maSP = Session["ProductId"].ToString();
                string tenSP = Session["Name"].ToString();
                string donGia = Session["LastPrice"].ToString();
                string giaGoc = Session["Price"].ToString();
                string hinhAnh = Session["Url"].ToString();
                string quantity = Session["quantity"].ToString();
                string tax = Session["tax"].ToString();
                //Kiểm tra xem đã có sản phẩm trong giỏ hàng chưa ?
                //Nếu chưa thì thêm bản ghi mới vào giỏ hàng với số lượng Quantity là 1
                //Nếu có thì tăng quantity lên 1
                bool isExisted = false;
                foreach (DataRow dr in cart.Rows)
                {
                    if (dr["ProductId"].ToString() == maSP)
                    {
                        dr["quantity"] = int.Parse(dr["quantity"].ToString()) + int.Parse(quantity);
                        isExisted = true;
                        break;
                    }
                }
                if (!isExisted)//Chưa có sản phẩm trong giỏ hàng
                {
                    DataRow dr = cart.NewRow();
                    dr["ProductId"] = maSP;
                    dr["Name"] = tenSP;
                    dr["LastPrice"] = donGia;
                    dr["Url"] = hinhAnh;
                    dr["quantity"] = quantity;
                    dr["money"] = (Convert.ToDecimal(donGia) * Convert.ToInt32(quantity));
                    dr["tax"] = tax;
                    dr["Price"] = giaGoc;
                    dr["moneySum"] = 0;
                    cart.Rows.Add(dr);
                 
                }
                //Lưu lại thông tin giỏ hàng mới nhất vào session["Cart"]
                Session["cart"] = cart;
                Session["add"] = "show";
                grdcart.DataSource = cart;
                grdcart.DataBind();
              
            }
            else
            {
                string maSP, tenSP, donGia, hinhAnh, quantity;
                if (Session["ProductId"] == null)
                {
                    //Lưu lại thông tin giỏ hàng mới nhất vào session["Cart"]
                    Session["cart"] = cart;
                    grdcart.DataSource = cart;
                    grdcart.DataBind();
                }
                else
                {
                    maSP = Session["ProductId"].ToString();
                    foreach (DataRow dr in cart.Rows)
                    {
                        if (dr["ProductId"].ToString() == maSP)
                        {
                            break;
                        }
                    }
                   
                    //Lưu lại thông tin giỏ hàng mới nhất vào session["Cart"]
                    Session["cart"] = cart;
                    grdcart.DataSource = cart;
                    grdcart.DataBind();
                }

            }




            //    }
            //}
            //Hiện thị thông tin giỏ hàng
        }
        protected void grdcart_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string id = grdcart.DataKeys[e.NewSelectedIndex].Value.ToString();
            TextBox quantity = grdcart.Rows[e.NewSelectedIndex].Cells[4].FindControl("txtQuantity") as TextBox;
            string a = quantity.Text;
            //Duyệt qua Giỏ hàng và tăng số lượng
            DataTable cart = Session["cart"] as DataTable;
            foreach (DataRow dr in cart.Rows)
            {
                //Kiểm tra mã sản phẩm phù hợp để gán số lượng khách hàng mua
                if (dr["ProductId"].ToString() == id)
                {
                    dr["quantity"] = int.Parse(quantity.Text);
                    dr["money"] = Convert.ToInt32(dr["quantity"]) * Convert.ToDecimal(dr["LastPrice"]);
                    break;
                }
            }
            //Lưu lại vào Session
            Session["cart"] = cart;
            //Hiển thị giỏ hàng với thông tin mới
            grdcart.DataSource = cart;
            grdcart.DataBind();
            
        }
        protected void grdcart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = grdcart.DataKeys[e.RowIndex].Value.ToString();
            //Duyệt qua Giỏ hàng và xóa sản phẩm phù hợp
            DataTable cart = Session["cart"] as DataTable;
            foreach (DataRow dr in cart.Rows)
            {
                if (dr["ProductId"].ToString() == id)
                {
                    Session["productdelete"] = dr["ProductId"].ToString();
                    string a = Session["productdelete"].ToString();
                    dr.Delete();
                    break;
                }
            }
            //Lưu lại vào Session
            Session["cart"] = cart;
            //Hiển thị giỏ hàng với thông tin mới
            grdcart.DataSource = cart;
            grdcart.DataBind();
        }

        protected void btnUpgrade_Click(object sender, EventArgs e)
        {
            DataTable cart = Session["cart"] as DataTable;
            decimal total = 0;
            foreach (DataRow dr in cart.Rows)
            {
                decimal money = Decimal.Parse(dr["money"].ToString());
                total += money;
            }
            lbTotal.Text = total.ToString("n0") + " VND";
            Session["productdelete"] = null;
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
        protected void lbCheckout_Click(object sender, EventArgs e)
        {
            DataTable cart = Session["cart"] as DataTable;
            int check = 0;
            foreach (DataRow dr in cart.Rows)
            {
                string id = dr["ProductId"].ToString();
                if (id == "" || id == null)
                {
                    check = 0;
                }
                else { check += 1; }
            }
            int i = grdcart.Rows.Count;
            if (check >= 1)
            {
                Response.Redirect("ThanhToan.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page),
                    "popup", "alert('Bạn cần thêm vào giỏ ít nhất 1 sản phẩm để thanh toán!')", true);
            }
        }
        protected void lbDelete_Click(object sender, EventArgs e)
        {
        }
    }
}
