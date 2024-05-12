using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;
namespace TT_ShopJQK.Admin
{
    public partial class DuyetDon : System.Web.UI.Page
    {
        DataUtil data = new DataUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int d = (int)Session["us"];
                txtID.Text = d.ToString();
                int role = 0;
                if (role == 2)
                {

                    ddlDuyet.SelectedValue = "2";
                }
                else if (role == 0)
                {
                    ddlDuyet.SelectedValue = "0";

                }
                txtID.Enabled = false;
                DataBind();
            }
        }

        protected void btnSua_Click1(object sender, EventArgs e)
        {
            try
            {
            
                HoaDon x = new HoaDon();
                x.OrderID = int.Parse(txtID.Text);
                string role = ddlDuyet.SelectedValue;
                x.Status = int.Parse(role);
                data.SuaHD(x.OrderID,x.Status);
                the.Text = "Duyệt thành công !";
                Response.Redirect(url: "QuanLyDonHang.aspx");
            }
            catch (Exception e1)
            {

                the.Text = "Something wrong: " + e1.Message.ToString();
            }
        }
        
    }
}