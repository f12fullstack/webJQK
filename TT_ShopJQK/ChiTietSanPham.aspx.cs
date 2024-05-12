using System;
using System.Collections.Generic;
using System.Linq;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductId = int.Parse(Request.QueryString[0].ToString());
            detailProduct w = data.layra1sp(ProductId);
           /* anhsp.ImageUrl = "~/Anh/anhSp/" + w.hinhAnh;
            anh = w.hinhAnh;
            anhsp1.ImageUrl = "~/Anh/anhSp/" + w.hinhAnh;
            anh = w.hinhAnh;*/
            //ltImage.Text = "<img src='/images/" + w.hinhAnh.ToString() + "' />";
            LbtenSP.Text = w.Name;
            lbdonGia.Text = w.LastPrice.ToString();
            LbdanhMuc.Text = w.NameCategoryId;
            LbthongTinSp.Text = w.Description;
            LbkhuyenMai.Text = w.Price.ToString();
            Label1.Text = w.NameCategoryId;
            anhsp.ImageUrl = w.Url[0];
            Image1.ImageUrl = w.Url[1];
            Image2.ImageUrl = w.Url[2];
            myLink.HRef = w.Url[0];
            A1.HRef = w.Url[1];
            A2.HRef = w.Url[2];
        }
        protected void btnAddToCart_Click(object sender, EventArgs e)
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

        }
    }
}
