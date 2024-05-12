<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ChamSocDa.aspx.cs" Inherits="TT_ShopJQK.ChamSocDa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <!-- Shop Section Start -->
        <div class="shop-section section pt-100 pt-lg-80 pt-md-70 pt-sm-60 pt-xs-50 pb-70 pb-lg-50 pb-md-40 pb-sm-60 pb-xs-50">
            <div class="container">
                <div class="row">
                    <div class="col-lg-3 order-lg-1 order-2">
                        <!-- Single Sidebar Start  -->
                        <div class="common-sidebar-widget">
                            <h3 class="sidebar-title">Color</h3>
                            <ul class="sidebar-list">
                                <li><a href="#"><i class="fa fa-angle-right"></i>Red <span class="count">(5)</span></a></li>
                                <li><a href="#"><i class="fa fa-angle-right"></i>Orange <span class="count">(4)</span></a></li>
                                <li><a href="#"><i class="fa fa-angle-right"></i>Blue <span class="count">(4)</span></a></li>
                              
                            </ul>
                        </div>
                        <!-- Single Sidebar End  -->
                        <!-- Single Sidebar Start  -->
                        <div class="common-sidebar-widget">
                            <h3 class="sidebar-title">Có Thể Bạn Cần</h3>
                            <ul class="sidebar-list">
                                <li><a href="#"><i class="fa fa-angle-right"></i>Chăm Sóc Tóc <span class="count">(1)</span></a></li>
                                <li><a href="#"><i class="fa fa-angle-right"></i>Nước Hoa <span class="count"></span></a></li>
                                <li><a href="#"><i class="fa fa-angle-right"></i>Chăm Sóc Da <span class="count"></span></a></li>
                                <li><a href="#"><i class="fa fa-angle-right"></i>Thương Hiệu <span class="count"></span></a></li>
                                <li><a href="#"><i class="fa fa-angle-right"></i>Blogs  <span class="count">(2)</span></a></li>
                            </ul>
                        </div>
                        <!-- Single Sidebar End  -->
                        <!-- Single Sidebar Start  -->
                        <div class="common-sidebar-widget">
                            <h3 class="sidebar-title">Price</h3>
                            <ul class="sidebar-list">
                                <li><a href="#"><i class="fa fa-angle-right"></i><span class="price">€0.00</span> - <span class="price">€99.99</span> <span class="count">(7)</span></a></li>
                                <li><a href="#"><i class="fa fa-angle-right"></i><span class="price">€100.00</span> and above  <span class="count">(14)</span></a></li>
                            </ul>
                        </div>
                        <!-- Single Sidebar End  -->
                        <!-- Single Sidebar Start  -->
                        <div class="common-sidebar-widget">
                            <div class="single-banner">
                                <a href="#">
                                    <img src="https://img.lazcdn.com/g/p/803f4b2949ecce51325d92e745f66b67.jpg_720x720q80.jpg" alt="">
                                </a>
                            </div>
                        </div>
                        <!-- Single Sidebar End  -->
                    </div>
                    <div class="col-lg-9 order-lg-2 order-1">
                        <div class="row">
                            <div class="col-12">
                                <div class="shop-banner mb-35 mb-xs-20">
                                    <img style="height:350px;" src="https://file.hstatic.net/1000317075/file/thoi-trang-cho-nguoi-gay-nu-cardina_e4968f42764142388ac8ecd58f4c7ea2.jpg" alt="">
                                </div>
                                <div class="shop-banner-title">
                                    <h2> Quẩn áo</h2>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <!-- Grid & List View Start -->
                                <div class="shop-topbar-wrapper d-md-flex justify-content-md-between align-items-center">
                                    <div class="grid-list-option">
                                         <ul class="nav">
                                          <li>
                                            <a class="active show" data-toggle="tab" href="#grid"><i class="fa fa-th"></i></a>
                                          </li>
                                          <li>
                                            <a data-toggle="tab" href="#list" class=""><i class="fa fa-th-list"></i></a>
                                          </li>
                                        </ul>
                                     </div>
                                     <!--Toolbar Short Area Start-->
                                     <div class="toolbar-short-area d-md-flex align-items-center">
                                         <div class="toolbar-shorter ">
                                            <label>Sort By:</label>
                                             <select class="wide">
                                                 <option data-display="Select">Nothing</option>
                                                 <option value="Relevance">Relevance</option>
                                                 <option value="Name, A to Z">Name, A to Z</option>
                                                 <option value="Name, Z to A">Name, Z to A</option>
                                                 <option value="Price, low to high">Price, low to high</option>
                                                 <option value="Price, high to low">Price, high to low</option>
                                             </select>
                                         </div>
                                         <div class="toolbar-shorter ">
                                            <label>Show</label>
                                             <select class="small">
                                                 <option data-display="Select">9</option>
                                                 <option value="15">15</option>
                                                 <option value="30">30</option>
                                             </select>
                                             <span>per page</span>
                                         </div>
                                         
                                     </div>
                                     <!--Toolbar Short Area End-->
                                </div>
                                <!-- Grid & List View End -->
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="shop-product">
                                    <div id="myTabContent-2" class="tab-content">
                                        <div id="grid" class="tab-pane fade active show">
                                            <div class="product-grid-view">
                                                <div class="row">
                                                                                                                                                                 
                                <asp:Repeater ID="Rspt" runat="server" DataSourceID="SqlDataSource1" >
                                    <ItemTemplate>
                                        <div class="col-lg-4 col-md-6 col-sm-6">
                                                       <!-- Single Product Start -->
                                                        <div class="single-product mb-30">
                                                    <div class="product-img">
                                                         <a title='<%#:Eval("Name") %> <%#:Eval("ProductId") %>' href='/ChiTietSanPham.aspx?ProductId=<%#:Eval("ProductId") %>'>
                                                                <img style="height:350px"  alt="" src='<%#:Eval("Url") %>' />
                                                         </a>
                                                                <span class="descount-sticker">-10%</span>
                                                                <span class="sticker">New</span>
                                                                <div class="product-action d-flex justify-content-between">
                                                                    <a class="product-btn" href="#">Add to Cart</a>
                                                                    <ul class="d-flex">
                                                                        <li><a href="#quick-view-modal-container" data-toggle="modal" title="Quick View"><i class="fa fa-eye"></i></a></li>
                                                                        <li><a href="#"><i class="fa fa-heart-o"></i></a></li>
                                                                        <li><a href="#"><i class="fa fa-exchange"></i></a></li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                   <div class="product-content">
                                                                <h3><a href='/ChiTietSanPham.aspx?maSP=<%#:Eval("ProductId") %>'><%#Eval("Name")%></a></h3>
                                                                <div class="ratting">
                                                                    <i class="fa fa-star"></i>
                                                                    <i class="fa fa-star"></i>
                                                                    <i class="fa fa-star"></i>
                                                                    <i class="fa fa-star"></i>
                                                                    <i class="fa fa-star"></i>
                                                                </div>
                                                                <h4 class="price"><span class="new"><%#Eval("Price")%></span>
                                                                    <span class="old"><%#Eval("LastPrice")%> đ</span></h4>
                                                            </div>
                                                 </div>                                             
                                                </div> 
                                        </ItemTemplate>
                                </asp:Repeater>

                                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_ECommerceShopConnectionString %>" SelectCommand="
                                      SELECT *
                                            FROM (
                                                SELECT p.ProductId, p.CategoryId, p.LastPrice, p.Price, p.Description, p.Name, pd.Url, pc.Name AS NameCategory,
                                                        ROW_NUMBER() OVER (PARTITION BY p.ProductId ORDER BY pd.id) AS RowNumber
                                                FROM Products p 
                                                JOIN ProductImages pd ON p.ProductId = pd.ProductId 
                                                JOIN Categories pc ON p.CategoryId = pc.CategoryId 
                                                WHERE p.CategoryId = 3
                                            ) AS RankedProducts
                                            WHERE RowNumber = 1;">

                                 </asp:SqlDataSource>

                                        </div>
                                        <div id="list" class="tab-pane fade">
                                            <div class="product-list-view">
                                               
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-30 mb-sm-40 mb-xs-30">
                            <div class="col">
                                <ul class="page-pagination">
                                    <li><a href="#"><i class="fa fa-angle-left"></i></a></li>
                                    <li class="active"><a href="#">01</a></li>
                                    <li><a href="#">02</a></li>
                                    <li><a href="#">03</a></li>
                                    <li><a href="#"><i class="fa fa-angle-right"></i></a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Shop Section End -->
   
</asp:Content>
