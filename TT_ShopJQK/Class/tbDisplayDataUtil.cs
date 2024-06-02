using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using TT_ShopJQK.Class;
using System.Configuration;

namespace TT_ShopJQK
{

    public class tbDisplayDataUtil
    {
        SqlConnection con;
        public tbDisplayDataUtil()
        {
            string sqlCon = ConfigurationManager.ConnectionStrings["db_ECommerceShopConnectionString"].ConnectionString;
            con = new SqlConnection(sqlCon);
        }
        public List<HoaDon> lsOrder(int IDDN)
        {
            List<HoaDon> ls = new List<HoaDon>();
            string sql = "select * from HoaDon where IDDN=@IDDN";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HoaDon o = new HoaDon();
            /*    o.ID = (int)dr["ID"];
                o.ngayDat = (DateTime)dr["ngayDat"];
                o.tongTien = (int)dr["tongTien"];
                o.IDDN = (int)dr["IDDN"];
                //o. = (bool)dr["Status"];
                o.trangThai = (byte)dr["trangThai"];
                o.sdt = (string)dr["sdt"];
                o.tenDN = (string)dr["tenDN"];
                o.diaChiNhan = (string)dr["diaChiNhan"];*/
                ls.Add(o);
            }
            con.Close();
            return ls;
        }

        //internal object lsOrder()
        //{
        //    throw new NotImplementedException();
        //}


        public void addItemmOrder(int id,int ProductID, int Quantity, decimal Price)
        {
            con.Open();
                 string sql = "insert into OrderItems (OrderID,ProductID, Quantity, Price) values (@OrderID, @ProductID, @Quantity, @Price)";
                 SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("OrderID", id);
            cmd.Parameters.AddWithValue("ProductID", ProductID);
                 cmd.Parameters.AddWithValue("Quantity", Quantity);
                 cmd.Parameters.AddWithValue("Price", Price);
              
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public int getNewOrder ()
        {
            con.Open();
            SqlCommand maxIdCmd = new SqlCommand("SELECT ISNULL(MAX(OrderId), 0) FROM Orders", con);
            int maxOrderId = (int)maxIdCmd.ExecuteScalar();
            con.Close();
            return maxOrderId;
        }

        public int addOrder(HoaDon o)
        {
            con.Open();
            SqlCommand maxIdCmd = new SqlCommand("SELECT ISNULL(MAX(OrderId), 0) FROM Orders", con);
            int maxOrderId = (int)maxIdCmd.ExecuteScalar();

            // Tăng giá trị orderId lên một đơn vị
            int newOrderId = maxOrderId + 1;
            string sql = "insert into Orders ( OrderID,UserId, CreatedAt, UpdatedAt , Status , ShippingCost, Tax, Discount)values ( @OrderID, @UserId, @CreatedAt, @UpdatedAt , @Status , @ShippingCost, @Tax, @Discount)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("OrderID", newOrderId);
            cmd.Parameters.AddWithValue("UserId", o.UserId);
            cmd.Parameters.AddWithValue("CreatedAt", o.CreatedAt);
            cmd.Parameters.AddWithValue("UpdatedAt", o.UpdatedAt);
            cmd.Parameters.AddWithValue("status", o.Status);
            cmd.Parameters.AddWithValue("ShippingCost", o.ShippingCost);
            cmd.Parameters.AddWithValue("Tax", o.Tax);
            cmd.Parameters.AddWithValue("Discount", o.Discount);
            cmd.ExecuteNonQuery();
            con.Close();
            return newOrderId;
        }
        public void addOrderDetail(ChiTietHoaDon o)
        {
            con.Open();
            string sql = "insert into ChiTietHoaDon values (@IDHD,@maSP,@donGia,@soLuong)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("IDHD", o.IDHD);
            cmd.Parameters.AddWithValue("maSP", o.maSP);           
            cmd.Parameters.AddWithValue("donGia", o.donGia);
            cmd.Parameters.AddWithValue("soLuong", o.soLuong);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}