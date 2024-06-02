using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1.X509;
using System.Collections.Concurrent;
using System.Security.Policy;
using System.Configuration;

namespace TT_ShopJQK
{
    public partial class My_Account : System.Web.UI.Page
    {
        DataUtil data = new DataUtil();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_ECommerceShopConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lbDetailTitle.Text = "";
               

                if (Session["username"] != null)
                {
                    var id = Session["userlogin"].ToString();
                    lbName.Text = Session["fullName"].ToString();
                    Users u =  getUser(int.Parse(id));
                    
                    if (u != null)
                    {
                        TxtDiachi.Text = u.diaChi;
                        TxtEmail.Text = u.email;
                        TxtSdt.Text = u.sdt;
                        txtFullName.Text = u.fullName;
                    }

                }
                else
                {
                    lbName.Text = "";
                }
                showList();
            }
        }
        private void showList()
        {
            if (Session["userlogin"] != null)
            {
                int IDDN = Convert.ToInt32(Session["userlogin"].ToString());
                con.Open();
                string query = @"
                        SELECT o.OrderID, 
                               o.ShippingCost, 
                               o.CreatedAt, 
                               CASE 
                                   WHEN o.Status = 0 THEN 'Chưa duyet'
                                   WHEN o.Status = 1 THEN 'Đã duyet'
                                   WHEN o.Status = 2 THEN 'Đã nhan'
                                    WHEN o.Status = 3 THEN 'Đã thanh toán'
                                   ELSE 'Unknown'
                               END AS Status, 
                               u.UserName, 
                               u.FullName, 
                               u.PhoneNum, 
                               u.AddressInfo 
                        FROM Orders o 
                        JOIN Users u ON o.UserId = u.UserId 
                        WHERE o.UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("UserId", IDDN);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
               
              /*  if (dt.Rows.Count >= 0)
                {
                    decimal s = 0;
                    lbuser.Text = dt.Rows[0]["FullName"].ToString();
                    lbphone.Text = dt.Rows[0]["PhoneNum"].ToString();
                    lbaddress.Text = dt.Rows[0]["AddressInfo"].ToString();
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        s += Convert.ToDecimal(row["money"]);
                    }
                    lbmoney.Text = s.ToString() + "VND";
                }*/
             
                da.Fill(dt);
                grOrders.DataSource = dt;
                grOrders.DataBind();
               
                con.Close();
            }

        }
        public Boolean checkValue()
        {
            var rs = true;

            if (TxtEmail.Text == "")
            {
                errorEmail.Text = "Không được để trống!";
                rs = false;
            }
            else
            {
                string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                if (!Regex.IsMatch(TxtEmail.Text, pattern))
                {
                    // Nếu địa chỉ email không hợp lệ, trả về false
                    errorEmail.Text = "Email không đúng định dạng!";
                    rs = false;
                }
                else
                {
                    errorEmail.Text = "";
                }
            }
            if (TxtDiachi.Text == "")
            {
                errorAddress.Text = "Không được để trống!";
                rs = false;

            }
            else
            {
                errorAddress.Text = "";
            }


            if (TxtSdt.Text == "")
            {
                errorPhone.Text = "Không được để trống!";
                rs = false;
            }
            else
            {
                string pattern = @"^\d{10,11}$";
                if (!Regex.IsMatch(TxtSdt.Text, pattern))
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
           

            return rs;
        }
        protected void grOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string orderid = e.Row.Cells[0].Text;
            //    Session["orderid"] = orderid;
            //    string days = e.Row.Cells[2].Text;
            //    if (days == "False")
            //    {
            //        e.Row.Cells[2].Text = "Chưa thanh toán";
            //    }
            //    else if (days == "True")
            //    {
            //        e.Row.Cells[2].Text = "Đã thanh toán";
            //    }
            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string days = e.Row.Cells[0].Text;
                if (days == "False")
                {
                    e.Row.Cells[6].Text = "Chưa thanh toán";
                }
                else if (days == "True")
                {
                    e.Row.Cells[6].Text = "Đã thanh toán";
                }
            }
            
        }
        private void showdetail(int IDHD)
        {
            con.Open();
            string query = "SELECT o.OrderID, oi.ProductID, oi.Quantity, oi.Price, p.ProductId, p.Name, m.Url "+
                            "FROM Orders o "+
                            "JOIN OrderItems oi ON o.OrderID = oi.OrderID "+
                            "JOIN Products p ON oi.ProductID = p.ProductId "+
                            "JOIN("+
                            "   SELECT ProductId, Url, ROW_NUMBER() OVER(PARTITION BY ProductId ORDER BY(SELECT NULL)) AS RowNum "+
                            "   FROM ProductImages "+
                            "     ) m ON p.ProductId = m.ProductId AND m.RowNum = 1 "+
                            "WHERE o.OrderID = @OrderID; ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("OrderID", IDHD);
     
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grvDetails.DataSource = dt;
            grvDetails.DataBind();
            con.Close();
        }


        private Users getUser(int id)
        {
            Users user = null;
            try
            {
                con.Open();
                string query = "SELECT * FROM Users WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new Users
                    {
                        email = reader["UserEmail"].ToString(),
                        diaChi = reader["AddressInfo"].ToString(),
                        sdt = reader["PhoneNum"].ToString(),
                        fullName = reader["FullName"].ToString(),
                        genDer = (bool)reader["Gender"],
                        
                    // Gán các thuộc tính khác tùy thuộc vào cấu trúc của bảng Users
                };
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                Console.WriteLine("Lỗi khi lấy thông tin người dùng: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return user;
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            msg.Text = "";
            if (checkValue())
            {
                con.Open();
                string checkuser = "select * from Users";
                SqlCommand cmd = new SqlCommand(checkuser, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    /*if (Txtmatkhau.Text == "")
                    {
                        msg.Text = "Mật khẩu không được để trống";
                        Txtmatkhau.Focus();
                        break;
                    }
                    else if (Txtmatkhau.Text != "")*/

                    int userid = int.Parse(Session["userlogin"].ToString());

                    SuaUser(userid);
                    msg.Text = "Cập nhật thông tin tài khoản thành công";
                    Session["fullName"] = txtFullName.Text;
                    lbName.Text = Session["fullName"].ToString();
                    break;

                    //else
                    //{
                    //    msg.Text = "Email không hợp lệ";
                    //}
                }
            } 
           
        }
        public void SuaUser(int IDDN)/*(Users b)*/
        {
            con.Open();
            string sql1 = "update Users set FullName = @FullName, UserEmail=@UserEmail,AddressInfo=@AddressInfo,PhoneNum=@PhoneNum where UserId=@UserId";
            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("UserEmail", TxtEmail.Text);
            cmd.Parameters.AddWithValue("AddressInfo", TxtDiachi.Text);
            cmd.Parameters.AddWithValue("PhoneNum", TxtSdt.Text);
            cmd.Parameters.AddWithValue("FullName", txtFullName.Text);
            /*cmd.Parameters.AddWithValue("matkhauDN", Txtmatkhau.Text);*/
            cmd.Parameters.AddWithValue("UserId", IDDN);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //protected void lbLogin_Click(object sender, EventArgs e)
        //{
        //    Response.Cookies.Add(new HttpCookie("returnUrl", Request.Url.PathAndQuery));
        //    Response.Redirect("Login.aspx");
        //}

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
        protected void urlLogin_Click(object sender, EventArgs e)
        {

            if (Session["userlogin"] == null)
            {
                urlLogin.Text = "Đăng Xuất";
                Response.Redirect("DangNhap.aspx");
            }
            else if (Session["userlogin"] != null)
            {
                Session.Remove("userlogin");
                Session.Remove("username");
                Session.Remove("useremail");
                Response.Cookies.Add(new HttpCookie("returnUrl", Request.Url.PathAndQuery));
                HttpCookie returnCookie = Request.Cookies["returnUrl"];
                if ((returnCookie == null) || string.IsNullOrEmpty(returnCookie.Value))
                {
                    Response.Redirect(url: "~/TrangChu.aspx");
                }
                else
                {
                    HttpCookie deleteCookie = new HttpCookie("returnUrl");
                    deleteCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(deleteCookie);
                    Response.Redirect(returnCookie.Value);
                }
            }
        }
        protected void lbExport_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int m = Convert.ToInt32(e.CommandArgument);
                Session["orderid"] = m;

                if (Session["orderid"] != null)
                {
                    grOrders.Visible = false;
                    string orderid = Session["orderid"].ToString();
                    showdetail(Convert.ToInt32(orderid));
                    grvDetails.Visible = false;
                    lbDetailTitle.Text = "Chi tiết order #" + m;
                    findaddress(m);
                    showgrid(m);
                    Panel2.Visible = true;
                    GridView1.Visible = true;

                }
            }
        }
        private void findaddress(int orderid)
        {
            String mycon = @"Data Source=DESKTOP-1G3UVUT\SQLEXPRESS;Initial Catalog=JQKShop;Integrated Security=True";

            String myquery = "Select a.ID ,ngayDat,tongTien,a.IDDN,a.sdt,a.diaChiNhan,b.tenDN " +
                "from HoaDon a inner join Users b on a.IDDN=b.IDDN " +
                "where a.ID='" + orderid + "'";
            SqlConnection con = new SqlConnection(mycon);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = myquery;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
             /*   lborderid.Text = ds.Tables[0].Rows[0]["ID"].ToString();*/
                lbuser.Text = ds.Tables[0].Rows[0]["tenDN"].ToString();
              /*  lbdate.Text = ds.Tables[0].Rows[0]["ngayDat"].ToString();*/
                lbphone.Text = ds.Tables[0].Rows[0]["sdt"].ToString();
                lbaddress.Text = ds.Tables[0].Rows[0]["diaChiNhan"].ToString();
                lbmoney.Text = ds.Tables[0].Rows[0]["tongTien"].ToString();
            }

            con.Close();
        }
        private void showgrid(int orderid)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("Mã sản phẩm");
            dt.Columns.Add("Tên sản phẩm");
            dt.Columns.Add("Số lượng mua");
            dt.Columns.Add("Đơn giá");
            String mycon = @"Data Source=DESKTOP-1G3UVUT\SQLEXPRESS;Initial Catalog=JQKShop;Integrated Security=True";
            SqlConnection scon = new SqlConnection(mycon);
            String myquery = "Select IDHD,c.maSP, tenSP,soLuong,a.donGia " +
                "from ChiTietHoaDon " +
                "a inner join HoaDon b on a.IDHD = b.ID " +
                "inner join ChiTietSanPham c on a.maSP = c.maSP " +
                "where IDHD='" + orderid + "'";
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = myquery;
            cmd.Connection = scon;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            int totalrows = ds.Tables[0].Rows.Count;
            int i = 0;
            while (i < totalrows)
            {
                dr = dt.NewRow();
                dr["Mã sản phẩm"] = ds.Tables[0].Rows[i]["maSP"].ToString();
                dr["Tên sản phẩm"] = ds.Tables[0].Rows[i]["tenSP"].ToString();          
                dr["Số lượng mua"] = ds.Tables[0].Rows[i]["soLuong"].ToString();
                dr["Đơn giá"] = ds.Tables[0].Rows[i]["donGia"].ToString();
                dt.Rows.Add(dr);
                i = i + 1;
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();

            //Label8.Text = grandtotal.ToString();
        }
        private void exportpdf()
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=OrderInvoice.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            Panel2.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void btnpdf_Click(object sender, EventArgs e)
        {
            exportpdf();
        }

    }
}
