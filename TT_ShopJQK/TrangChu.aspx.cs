using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;

namespace TT_ShopJQK
{
    public partial class TrangChu : System.Web.UI.Page
    {
        DataUtil data = new DataUtil();
        public int ProductId;

        protected void Page_Load(object sender, EventArgs e)
        {
         
        }


        protected void lnkQuickView_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;
            string productId = lnkButton.CommandArgument;

            // Lưu productId vào ViewState để sử dụng sau này
            Session["ModalId"] = productId;
            string message = "Thông báo của bạn"; // Thông điệp bạn muốn hiển thị

            // Tạo mã JavaScript để hiển thị thông báo
            string script = "alert('" + message + "');";

            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            // Mở modal

        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            // Xử lý logic thêm sản phẩm vào giỏ hàng với ProductId đã lấy được
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;
            detailProduct w = data.layra1sp(int.Parse(productId));
                Session["ProductId"] = w.ProductId;

                Session["LastPrice"] = w.LastPrice;

                Session["Price"] = w.Price;
                Session["Name"] = w.Name;

                Session["Url"] = w.Url[0];

                Session["quantity"] = 1;
                Session["tax"] = "0.03";

                Session["add"] = "add";
                //Response.Redirect("GioHang.aspx");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "message",
                        "alert('Bạn vừa thêm sản phẩm " + w.Name + " vào giỏ hàng!');location.href = 'GioHang.aspx';", true);
        }

    }
}