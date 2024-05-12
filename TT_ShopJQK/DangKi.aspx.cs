using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;
namespace TT_ShopJQK
{
    public partial class DangKi : System.Web.UI.Page
    {
        DataUtil da = new DataUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            //cbbmaDanhMuc.DataSource = da.au();
            //cbbmaDanhMuc.DataTextField = "tenDanhMuc";
            //cbbmaDanhMuc.DataValueField = "maDanhMuc";
            //txtIDDN.Enabled = false;

            DataBind();
        }
        public Boolean checkValue()
        {
            var rs = true;
            
            if (txtTendangnhap.Text == "")
            {
                errorNameUser.Text = "Không được để trống!";
                rs = false;
            }
            else
            {
                string pattern = @"^[a-zA-Z0-9_]*$";
                if (!Regex.IsMatch(txtTendangnhap.Text, pattern))
                {
                    // Nếu địa chỉ email không hợp lệ, trả về false
                    errorNameUser.Text = "Tên đăng nhập không chứa ký tự đặc biệt và khoảng trắng!";
                    rs = false;
                }else
                {
                    errorNameUser.Text = "";
                }
            }
            if (txtEmail.Text == "")
            {
                errorEmail.Text = "Không được để trống!";
                rs = false;
            }
            else
            {
                string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                if (!Regex.IsMatch(txtEmail.Text, pattern))
                {
                    // Nếu địa chỉ email không hợp lệ, trả về false
                    errorEmail.Text = "Email không đúng định dạng!";
                    rs = false;
                }else
                {
                    errorEmail.Text = "";
                }
            }
            if (txtDiachi.Text == "")
            {
                errorAddress.Text = "Không được để trống!";
                rs = false;

            }else
            {
                errorAddress.Text = "";
            }
           

            if (txtsdt.Text == "")
            {
                errorPhone.Text = "Không được để trống!";
                rs = false;
            }
            else
            {
                string pattern = @"^\d{10,11}$";
                if (!Regex.IsMatch(txtsdt.Text, pattern))
                {
                    errorPhone.Text = "Số điện thoại không đúng định dạng!";
                    rs = false;
                }
                else errorPhone.Text = "";


            }
            if (txtFullName.Text == "")
            {
                errorFullName.Text = "Không được để trống!";
                rs = false;
            }
            else errorFullName.Text = "";
            if (txtMatkhau.Text == "")
            {
                errorPass.Text = "Không được để trống!";
                rs = false;
            }
            else errorPass.Text = "";

                return rs;
        }
        protected void btnThem_Click(object sender, EventArgs e)
        {

            if (checkValue() == true)
            {
                try
                {
                    Users bo = new Users();
                    //bo.maDT = int.Parse(txtmaDT.Text);


                    bo.tenDN = txtTendangnhap.Text;
                    bo.email = txtEmail.Text;
                    bo.diaChi = txtDiachi.Text;
                    bo.sdt = txtsdt.Text;
                    bo.matkhauDN = txtMatkhau.Text;
                    bo.genDer = true;
                    bo.fullName = txtFullName.Text;
                    bo.quyen = "user";
                    if(da.checkEntry(txtTendangnhap.Text) == 0)
                    {
                        da.themUser(bo);
                        string script = "alert('Bạn đã đăng ký thành công tài khoản!'); window.location.href = 'DangNhap.aspx';";
                        ScriptManager.RegisterStartupScript(this, GetType(), "SuccessAlert", script, true);
                        //the.Text = "Bạn đã đăng kí thành công tài khoản!";
                    }else
                    {
                        errorNameUser.Text = "Tên đăng nhập đã tồn tại!";
                    }

                  
                }
                catch (Exception ex)
                {
                    the.Text = "không thêm được" + ex.Message;
                }
            }
        }
    }
}