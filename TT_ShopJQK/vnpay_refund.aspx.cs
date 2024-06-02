using iTextSharp.text.pdf.qrcode;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TT_ShopJQK.Class;

namespace TT_ShopJQK
{
    public partial class vnpay_refund : System.Web.UI.Page
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        tbDisplayDataUtil du = new tbDisplayDataUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            log.InfoFormat("Begin VNPAY Return, URL={0}", Request.RawUrl);
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];
               /* HoaDon order = JsonConvert.DeserializeObject<HoaDon>(Request.QueryString["vnp_order"]);*/

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        //Thanh toan thanh cong
                        displayMsg.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                        try
                        {
                            HoaDon o = new HoaDon();
                            o.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString());
                            o.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToString());

                            o.UserId = Convert.ToInt32(Session["userlogin"].ToString());
                            o.Status = 3;
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
/*
                            msg.Text = "Something wrong: " + e1.Message.ToString();*/
                        }
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        displayMsg.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                    }
                    displayTmnCode.InnerText = "Mã Website (Terminal ID):";
                    value_displayTmnCode.InnerText = TerminalID;

                    displayTxnRef.InnerText = "Mã giao dịch thanh toán:";
                    value_displayTxnRef.InnerText = "JQKSHOP0000" + orderId.ToString();

                    displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:";
                    value_displayVnpayTranNo.InnerText = vnpayTranId.ToString();

                    displayAmount.InnerText = "Số tiền thanh toán (VND):";
                    value_displayAmount.InnerText = vnp_Amount.ToString();

                    displayBankCode.InnerText = "Ngân hàng thanh toán:";
                    value_displayBankCode.InnerText =  bankCode;
                   
                }
                else
                {
                    log.InfoFormat("Invalid signature, InputData={0}", Request.RawUrl);
                    displayMsg.InnerText = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

        }

    }
}