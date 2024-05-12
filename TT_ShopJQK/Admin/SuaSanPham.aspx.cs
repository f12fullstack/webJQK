using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;

namespace TT_ShopJQK.Admin
{
    public partial class SuaSanPham : System.Web.UI.Page
    {
        DataUtil data = new DataUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                detailProduct d = (detailProduct)Session["sp"];
                txtmaSP.Text = d.ProductId.ToString();

                cbbmaDanhMuc.DataSource = data.au();
                cbbmaDanhMuc.DataTextField = "Name";
                cbbmaDanhMuc.DataValueField = "CategoryId";
                DataBind();
              
                txttenSP.Text = d.Name;
                txtdongia.Text = d.LastPrice.ToString();
                txtKhuyenmai.Text = d.Price.ToString();
                txtgioithieu.Text = d.Description.ToString();
                cbbmaDanhMuc.SelectedValue = d.CategoryId.ToString();
                Image1.Text = d.Images[0].Url;
                Image2.Text = d.Images[1].Url;
                Image3.Text = d.Images[2].Url;
                Image1.Font.Name = d.Images[0].Id.ToString();
                Image2.Font.Name = d.Images[1].Id.ToString();
                Image3.Font.Name = d.Images[2].Id.ToString();
            /*    foreach (var item in d.Images)
                {
                    TextBox txtBox = new TextBox();
                    txtBox.ID = item.Id.ToString(); 
                    txtBox.CssClass = "form-control";
                    txtBox.Text = item.Url;
                    TextBoxContainer.Controls.Add(txtBox);
                    TextBoxContainer.Controls.Add(new LiteralControl("<br />")); 
                }*/

                /*Txthinhanh.Text = d.hinhAnh;*/
            }
        }

        protected void btnSua_Click1(object sender, EventArgs e)
        {
            try
            {
                detailProduct bo = new detailProduct();
                bo.ProductId = int.Parse(txtmaSP.Text);
                bo.CategoryId = int.Parse(cbbmaDanhMuc.SelectedValue);
                bo.Name = txttenSP.Text;
                bo.LastPrice = decimal.Parse(txtdongia.Text);
                bo.Price = decimal.Parse(txtKhuyenmai.Text);
                bo.Description = txtgioithieu.Text;

                bo.Images = new List<(string Url, int Id)> {
                ((Image1.Text, int.Parse(Image1.Font.Name))),
                ((Image2.Text, int.Parse(Image2.Font.Name))),
                ((Image3.Text, int.Parse(Image3.Font.Name)))
                };

                /*  bo.hinhAnh = Txthinhanh.Text;*/
                foreach (var item in bo.Images)
                {
                    data.updateImageProduct(item.Id, item.Url);
                }
                data.Sua(bo);
                the.Text = "Cập nhật thành công!";



            }
            catch (Exception ex)
            {

                the.Text = "Không sửa được" + ex.Message;

            }
        }
    }
}