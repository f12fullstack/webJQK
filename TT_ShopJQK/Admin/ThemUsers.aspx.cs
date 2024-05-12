using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;
namespace TT_ShopJQK.Admin
{
    public partial class ThemUsers : System.Web.UI.Page
    {
        DataUtil da = new DataUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            //cbbmaDanhMuc.DataSource = da.au();
            //cbbmaDanhMuc.DataTextField = "tenDanhMuc";
            //cbbmaDanhMuc.DataValueField = "maDanhMuc";
            txtIDDN.Enabled = false;
            
            DataBind();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                Users bo = new Users();
                //bo.maDT = int.Parse(txtmaDT.Text);

                bo.IDDN = 1;
                bo.tenDN = txtTendangnhap.Text;
                bo.email = txtEmail.Text;
                bo.diaChi = txtDiachi.Text;
                bo.sdt = txtsdt.Text;
                bo.matkhauDN = txtMatkhau.Text;
                bo.genDer = (ddlGender.SelectedValue == "1");
                bo.fullName = txtFullName.Text;
                bo.quyen = ddlQuyen.SelectedItem.Text;
                da.themUser(bo);
                the.Text = "them thanh cong";
            }
            catch (Exception ex)
            {

                the.Text = "không thêm được" + ex.Message;
            }

        }
    }
}