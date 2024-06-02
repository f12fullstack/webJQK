using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TT_ShopJQK.Class;
using System.Configuration;
using log4net;
using Newtonsoft.Json;

namespace TT_ShopJQK
{
    public partial class ThanhToan : System.Web.UI.Page
    {
        private static readonly ILog log =
         LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        tbDisplayDataUtil du = new tbDisplayDataUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userlogin"] == null)
                {
                    txtname.Focus();
                    txtname.Enabled = false;
                    txtEmail.Enabled = false;
                }
                else
                {
                    string userid = Session["userlogin"].ToString();
                    string username = Session["fullName"].ToString();
                    txtname.Text = username;
                    txtname.Enabled = false;
                    txtEmail.Enabled = false;
                    txtEmail.Text = Session["useremail"].ToString();
                    txtPhone.Text = Session["phoneNum"].ToString();

                }
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
                cart.Columns.Add("quantity");
                cart.Columns.Add("money");
                cart.Columns.Add("tax");
                cart.Columns.Add("Price");
                cart.Columns.Add("moneySum");
                //Sau khi tạo xong thì lưu lại vào session
                Session["cart"] = cart;
            }
            else
            {
                //Lấy thông tin giỏ hàng từ Session["cart"]
                cart = Session["cart"] as DataTable;
            }
          
            if (Session["add"] == "show")
            {
                string productid, name, price, quantity /*available*/;
                foreach (DataRow dr in cart.Rows)
                {
              
                        dr["moneySum"] = (1- decimal.Parse(dr["tax"].ToString())) * decimal.Parse(dr["money"].ToString());
                       
                }
                grdcart.DataSource = Session["cart"];
                    grdcart.DataBind();
                

            }
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

        protected void btnPay_Click(object sender, EventArgs e)
        {


            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key
            int id = du.getNewOrder();

            HoaDon o = new HoaDon();
            o.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString());
            o.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToString());
            o.OrderID = id;
            o.UserId = Convert.ToInt32(Session["userlogin"].ToString());
            o.Status = 0;
            DataTable cart = Session["cart"] as DataTable;
            decimal total = 0;
            foreach (DataRow datarow in cart.Rows)
            {

                decimal money = decimal.Parse(datarow["moneySum"].ToString());
                total += money;
            }
            o.ShippingCost = total;
            o.Discount = 10000;
            o.Tax = 3;
            o.ShippingCost = (int)total; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            o.Status = 1; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
            o.CreatedAt = DateTime.Now;

            //Save order to db

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            string json = JsonConvert.SerializeObject(o, Formatting.Indented);
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (o.ShippingCost * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (bankcode_Vnpayqr.Checked == true)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (bankcode_Vnbank.Checked == true)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (bankcode_Intcard.Checked == true)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", o.CreatedAt.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + o.OrderID);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", o.OrderID.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

        /*    vnpay.AddRequestData("vnp_order", json);*/

            //Add Params of 2.1.0 Version
            //Billing

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            Response.Redirect(paymentUrl);


        }
        protected void btnOrder_Click(object sender, EventArgs e)
        {
            if (Session["userlogin"] == null)
            {
                Response.Cookies.Add(new HttpCookie("returnUrl", Request.Url.PathAndQuery));
            }
            else
            {
                try
                {
                    HoaDon o = new HoaDon();
                    o.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString());
                    o.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToString());

                    o.UserId = Convert.ToInt32(Session["userlogin"].ToString());
                    o.Status = 0;
                    DataTable cart = Session["cart"] as DataTable;
                    decimal total = 0;
                    //lbTotal.Text = total.ToString("n0") + " VND";
                    o.ShippingCost = total;
                    o.Discount = 10000;
                    o.Tax = 3;

                    int id = du.addOrder(o);

                    foreach (DataRow datarow in cart.Rows)
                    {
                        du.addItemmOrder(id, int.Parse(datarow["ProductId"].ToString()), int.Parse(datarow["quantity"].ToString()), decimal.Parse(datarow["LastPrice"].ToString()));
                    }
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "popup", "alert('Đặt hàng thành công!')", true);
                    //Response.Redirect(url: "ListCategories.aspx");
                    Session.Remove("cart");
                    Session.Remove("add");
                }
                catch (Exception e1)
                {

                    msg.Text = "Something wrong: " + e1.Message.ToString();
                }
            }
        }
    }
}