using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Org.BouncyCastle.Asn1.X509;
using System.Collections.Concurrent;
using System.Drawing;

namespace TT_ShopJQK.Class
{
    public class DataUtil
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_ECommerceShopConnectionString"].ConnectionString);
        public List<detailProduct> dssp()
        {
            List<detailProduct> dt = new List<detailProduct>();
            string sql = " SELECT *  FROM( SELECT p.*, pd.Url, ROW_NUMBER() OVER(PARTITION BY p.ProductId ORDER BY pd.Id) AS RowNumber FROM Products p JOIN ProductImages pd ON p.ProductId = pd.ProductId ) AS RankedImages WHERE RowNumber = 1;";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                detailProduct bok = new detailProduct();
                bok.ProductId = (int)dr["ProductId"];
                bok.CategoryId = (int)dr["CategoryId"];
                bok.CategoryId = 1;
                bok.Name = (string)dr["Name"];
                bok.LastPrice = (decimal)dr["LastPrice"];
                bok.Price = (decimal)dr["Price"];
                bok.Description = (string)dr["Description"];
                string b = ((string)dr["Url"]);
                if (bok.Url == null)
                {
                    bok.Url = new List<string> { b };
                }
                else bok.Url.Add(b);
                dt.Add(bok);
            }
            con.Close();
            return dt;
        }
        public void Xoa(int ProductId)
        {
            con.Open();
            string sql1 = "delete from Products where ProductId=@ProductId";
            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("@ProductId", ProductId);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Them(detailProduct b, List<string> imageUrls)
        {
           
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    SqlCommand maxIdCmd = new SqlCommand("SELECT ISNULL(MAX(Id), 0) FROM ProductImages", con, transaction);
                    object result = maxIdCmd.ExecuteScalar();
                    int maxImageId = Convert.ToInt32(result == DBNull.Value ? 0 : result) + 1;

                    string sql1 = "INSERT INTO Products (CategoryId, Name, LastPrice, Price, Description) VALUES (@CategoryId, @Name, @LastPrice, @Price, @Description); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(sql1, con, transaction);
                    cmd.Parameters.AddWithValue("@CategoryId", b.CategoryId);
                    cmd.Parameters.AddWithValue("@Name", b.Name);
                    cmd.Parameters.AddWithValue("@LastPrice", b.LastPrice);
                    cmd.Parameters.AddWithValue("@Price", b.Price);
                    cmd.Parameters.AddWithValue("@Description", b.Description);
                    int productId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (string url in imageUrls)
                    {
                        SqlCommand cmdImage = new SqlCommand("INSERT INTO ProductImages (Id, ProductId, Url) VALUES (@Id, @ProductId, @Url);", con, transaction);
                        cmdImage.Parameters.AddWithValue("@Id", maxImageId);
                        cmdImage.Parameters.AddWithValue("@ProductId", productId);
                        cmdImage.Parameters.AddWithValue("@Url", url);
                        cmdImage.ExecuteNonQuery();
                        maxImageId++;
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error: " + ex.Message);
                }
            
        }


        public List<Category> au()
        {
            List<Category> a = new List<Category>();
            string sql = "select * from Categories where CategoryId is not NULL";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Category bok = new Category();
                bok.CategoryId = (int)dr["CategoryId"];
                bok.Name = (string)dr["Name"];
                //bok.maThuongHieu = (int)dr["maThuongHieu"];
                a.Add(bok);
            }
            con.Close();
            return a;
        }
        public detailProduct layra1sp(int ProductId)
        {
            List<detailProduct> a = new List<detailProduct>();
            string b = "";
            string sql = "SELECT p.ProductId,p.quantity, p.CategoryId, p.LastPrice, p.Price, p.Description, p.Name, pd.Id, pd.Url ,pc.Name AS NameCategory  FROM Products p JOIN ProductImages pd ON p.ProductId = pd.ProductId JOIN Categories pc ON p.CategoryId = pc.CategoryId WHERE p.ProductId = @ProductId;";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ProductId", ProductId);
            detailProduct bok = new detailProduct();
           /* SqlDataReader dr = cmd.ExecuteReader();*/
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    
                    bok.ProductId = (int)dr["ProductId"];
                    bok.NameCategoryId = (string)dr["NameCategory"];
                    bok.Name = (string)dr["Name"];
                    bok.LastPrice = (decimal)dr["LastPrice"];
                    bok.Price = (decimal)dr["Price"];
                    bok.Description = (string)dr["Description"];
                    bok.CategoryId = (int)dr["CategoryId"];
                    bok.quantity = (int)dr["quantity"];
                    b = (string)dr["Url"];
                    int c = (int)dr["Id"];
                    if (bok.Url == null)
                    {
                        bok.Url = new List<string> { b };
                        bok.Images = new List<(string Url, int Id)> { (b, c) };
                    }
                    else
                    {
                        bok.Url.Add(b);
                        bok.Images.Add((b, c));
                    };


                    a.Add(bok);
                }
            }
           /* if (dr.Read())
            {
                bok = new detailProduct();
                bok.ProductId = (int)dr["ProductId"];
                bok.CategoryId = 1;
                bok.Name = (string)dr["Name"];
                bok.LastPrice = (decimal)dr["LastPrice"];
                bok.Price = (decimal)dr["Price"];
                bok.Description = (string)dr["Description"];
               
                bok.hinhAnh = (string)dr["Url"];

                a.Add(bok);
            }*/
            con.Close();
            return bok;
        }
        public void updateImageProduct(int id, string url)
        {
            con.Open();
            string sql1 = "update ProductImages set Url = @Url WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Url", url); 
           

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Sua(detailProduct b)
        {
            con.Open();
            string sql1 = "update Products set CategoryId=@CategoryId,Name=@Name,LastPrice=@LastPrice,Price=@Price,Description=@Description" +
                " where ProductId=@ProductId";
            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("@ProductId", b.ProductId);
            cmd.Parameters.AddWithValue("@CategoryId", b.CategoryId);
            cmd.Parameters.AddWithValue("@Name", b.Name);
            cmd.Parameters.AddWithValue("@LastPrice", b.LastPrice);
            cmd.Parameters.AddWithValue("@Price", b.Price);
            cmd.Parameters.AddWithValue("@Description", b.Description);
   

            cmd.ExecuteNonQuery();
            con.Close();
        }
        //-----------------------------------------------Hoá Đơn Hàng----------------------------

        public List<HoaDon> dsHD()
        {
            List<HoaDon> dt = new List<HoaDon>();
            string sql = "select * from Orders";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HoaDon bok = new HoaDon();
                bok.Status = (int)dr["Status"];
                bok.ShippingCost = (decimal)dr["ShippingCost"];
                bok.CreatedAt = (DateTime)dr["CreatedAt"];
                bok.OrderID = (int)dr["OrderID"];
                bok.UserId = (int)dr["UserId"]; 
                dt.Add(bok);
            }
            con.Close();
            return dt;
        }
        public void XoaHD(int ID)
        {
            con.Open();
            string sql1 = "UPDATE Orders " +
                "SET Status = 1" +
                "WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("@OrderID", ID);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public HoaDon layra1HD(int ID)
        {
            List<HoaDon> a = new List<HoaDon>();
            string sql = "select * from Orders where OrderID=@OrderID";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("OrderID", ID);
            HoaDon bok = null;

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                bok = new HoaDon();
          /*      bok.ID = (int)dr["ID"];
                bok.trangThai = Convert.ToByte(dr["trangThai"]);*/
                a.Add(bok);
            }
            con.Close();
            return bok;
        }
        public void SuaHD(int id, int status)
        {
            con.Open();
            string sql1 = "UPDATE Orders " +
                "SET Status = @Status " +
                "WHERE OrderID = @OrderID";
            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("@OrderID", id);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //-----------------------------------------------Users----------------------------
        public List<Users> dsUser()
        {
            List<Users> dt = new List<Users>();
            string sql = "select * from Users";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                    Users bok = new Users();
                    bok.IDDN = (int)dr["UserId"];
                    bok.tenDN = (string)dr["UserName"];
                    bok.email = (string)dr["UserEmail"];
                    bok.diaChi = (string)dr["AddressInfo"];
                    bok.sdt = (string)dr["PhoneNum"];
                    /* bok.matkhauDN = (string)dr["matkhauDN"];*/
                    bok.genDer = (bool)dr["Gender"];
                    bok.quyen = (string)dr["UserRole"];
                    dt.Add(bok);
                }
            con.Close();
            return dt;
        }
        public int checkEntry(string u)
        {
            int count = 0;

            try
            {
                con.Open();
                string sql = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserName", u);

                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                Console.WriteLine("Lỗi khi kiểm tra tên đăng nhập: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return count;
        }

        public void themUser(Users u)
        {
            con.Open();
            string sql = "INSERT INTO Users (UserName, FullName, UserEmail, AddressInfo, PhoneNum, UserPassword, UserRole, Gender) " +
                         "VALUES (@UserName, @FullName, @UserEmail, @AddressInfo, @PhoneNum, @UserPassword, @UserRole, @Gender)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@UserName", u.tenDN);
            cmd.Parameters.AddWithValue("@FullName", u.fullName);
            cmd.Parameters.AddWithValue("@UserEmail", u.email);
            cmd.Parameters.AddWithValue("@AddressInfo", u.diaChi);
            cmd.Parameters.AddWithValue("@PhoneNum", u.sdt);
            cmd.Parameters.AddWithValue("@UserPassword", u.matkhauDN);
            cmd.Parameters.AddWithValue("@UserRole", u.quyen);
            cmd.Parameters.AddWithValue("@Gender", u.genDer);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void XoaUser(int UserId)
        {
            con.Open();
            string sql1 = "delete from Users where UserId=@UserId";
            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("UserId", UserId);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public Users layra1user(int UserId)
        {
            List<Users> a = new List<Users>();
            string sql = "select * from Users where UserId=@UserId";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("UserId", UserId);
            Users bok = null;

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                bok = new Users();
                bok.IDDN = (int)dr["UserId"];
                bok.tenDN = (string)dr["UserName"];
                bok.email = (string)dr["UserEmail"];
                bok.diaChi = (string)dr["AddressInfo"];
                bok.sdt = (string)dr["PhoneNum"];
              /*  bok.matkhauDN = (string)dr["matkhauDN"];*/
                bok.quyen = (string)(dr["UserRole"]);
                bok.genDer = (bool)dr["Gender"];
                a.Add(bok);
            }
            con.Close();
            return bok;
        }
        public void SuaUser(Users b)
        {
            con.Open();
            string sql1 = "update Users set UserName=@UserName,UserEmail=@UserEmail,AddressInfo=@AddressInfo,PhoneNum=@PhoneNum,UserRole=@UserRole,Gender=@Gender where UserId=@UserId";
            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("UserId", b.IDDN);
            cmd.Parameters.AddWithValue("UserName", b.tenDN);
            cmd.Parameters.AddWithValue("UserEmail", b.email);
            cmd.Parameters.AddWithValue("AddressInfo", b.diaChi);
            cmd.Parameters.AddWithValue("PhoneNum", b.sdt);
            cmd.Parameters.AddWithValue("Gender", b.genDer);
            cmd.Parameters.AddWithValue("UserRole", b.quyen);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //-----------------------------------------------DanhMucSanPham----------------------------
        public void DsDanhMUC(detailProduct b)
        {
            con.Open();
            string sql1 = "select * from ChiTietSanPham where CategoryId=@CategoryId";
            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("ProductId", b.ProductId);
            cmd.Parameters.AddWithValue("CategoryId", b.CategoryId);
            cmd.Parameters.AddWithValue("Name", b.Name);
            cmd.Parameters.AddWithValue("LastPrice", b.LastPrice);
            cmd.Parameters.AddWithValue("Price", b.Price);
            cmd.Parameters.AddWithValue("Description", b.Description);
          /*  cmd.Parameters.AddWithValue("hinhAnh", b.hinhAnh);*/
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public List<detailProduct> dsSanPhamDanhMuc(int CategoryId)
        {
            List<detailProduct> dt = new List<detailProduct>();
            string sql = "select * from ChiTietSanPham where CategoryId=@CategoryId";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("CategoryId", CategoryId);
            detailProduct bok = null;
            while (dr.Read())
            {
                bok = new detailProduct();
                bok.ProductId = (int)dr["ProductId"];
                bok.CategoryId = (int)dr["CategoryId"];
                bok.Name = (string)dr["Name"];
                bok.LastPrice = (decimal)dr["LastPrice"];
                bok.Price = (decimal)dr["Price"];
                bok.Description = (string)dr["Description"];
                /*bok.hinhAnh = (string)dr["hinhAnh"];*/
                dt.Add(bok);
            }
            con.Close();
            return dt;
        }

    }


}