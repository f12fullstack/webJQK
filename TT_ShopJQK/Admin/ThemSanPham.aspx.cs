using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;
namespace TT_ShopJQK.Admin
{
    public partial class ThemSanPham : System.Web.UI.Page
    {
        DataUtil da = new DataUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            cbbmaDanhMuc.DataSource = da.au();
            cbbmaDanhMuc.DataTextField = "Name";
            cbbmaDanhMuc.DataValueField = "CategoryId";
            DataBind();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();

            try
            {
                detailProduct bo = new detailProduct();
                //bo.maDT = int.Parse(txtmaDT.Text);
                bo.CategoryId = int.Parse(cbbmaDanhMuc.SelectedValue);
                bo.Name = txttenSP.Text;
                bo.LastPrice = decimal.Parse(txtdongia.Text);
                bo.Price =decimal.Parse(txtKhuyenmai.Text);
                bo.Description = txtgioithieu.Text;
                list.Add(Image1.Text);
                list.Add(Image2.Text);
                list.Add(Image3.Text);
                /*  bo.hinhAnh = txtAnhd.FileName;*/

                da.Them(bo,list);
                the.Text = "them thanh cong";
            }
            catch (Exception ex)
            {

                the.Text = "khong them duoc" + ex.Message;

            }
        }
    }
}