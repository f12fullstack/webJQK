﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TrangChu.aspx.cs"  Inherits="TT_ShopJQK.TrangChu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 1379px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="hero-section section position-relative">

        <div class="tf-element-carousel slider-nav" data-slick-options='{
        "slidesToShow": 1,
        "slidesToScroll": 1,
        "infinite": true,
        "arrows": true,
        "dots": true
        }'
            data-slick-responsive='[
        {"breakpoint":768, "settings": {
        "slidesToShow": 1
        }},
        {"breakpoint":575, "settings": {
        "slidesToShow": 1,
        "arrows": false,
        "autoplay": true
        }}
        ]'>
            <!--Hero Item start-->
            <div class="hero-item image-height bg-image" data-bg="./assets/images/hero/hero-7.jpg">
                <div class="container">
                    <div class="row">
                        <div class="col-12">
                            <!--Hero Content start-->
                            <div class="hero-content-4 center">
                                <img alt="" src="https://dojeannam.com/wp-content/uploads/2017/10/banner-thoi-trang-nam-cong-so-2018.jpg" />
                            </div>
                            <!--Hero Content end-->
                        </div>
                    </div>
                </div>
            </div>
            <!--Hero Item end-->

            <!--Hero Item start-->
            <div class="hero-item image-height bg-image" data-bg="https://hellomida.vn/wp-content/uploads/2023/09/banner-hlmd-1.jpg">
                <div class="container">
                    <div class="row">
                        <div class="col-12">
                            <!--Hero Content start-->
                            <div class="hero-content-2 color-1 center">
                                <img alt="" src="https://intphcm.com/data/upload/banner-thoi-trang-tuoi.jpg" width='100%' />
                            </div>
                            <!--Hero Content end-->
                        </div>
                    </div>
                </div>
            </div>
            <!--Hero Item end-->
        </div>

    </div>
    <!--slider section end-->

    <!--Banner section start-->
    <div class="banner-section section pt-100 pt-lg-80 pt-md-70 pt-sm-60 pt-xs-50">
        <div class="container">
            <div class="row">
                <div class="col-lg-6 col-md-6">
                    <!-- Single Banner Start -->
                    <div class="single-banner mb-30">
                        <a href="#">
                            <img src="https://intphcm.com/data/upload/1608888571-banner-thoi-trang-2.jpg" alt="">
                        </a>
                    </div>
                    <!-- Single Banner End -->
                </div>
                <div class="col-lg-3 col-md-3">
                    <!-- Single Banner Start -->
                    <div class="single-banner mb-30">
                        <a href="#">
                            <img src="https://channel.mediacdn.vn/428462621602512896/2022/5/11/vulcano-1-1652263546844522213228.jpg" alt="">
                        </a>
                    </div>
                    <div class="single-banner mb-30">
                        <a href="#">
                            <img src="https://product.hstatic.net/200000410665/product/8_c55db3399ff64d829d0588219348f561.jpg" alt="">
                        </a>
                    </div>
                    <!-- Single Banner End -->
                </div>
                <div class="col-lg-3 col-md-3">
                    <!-- Single Banner Start -->
                    <div class="single-banner mb-30">
                        <a href="#">
                            <img src="https://kaiwings.vn/upload/product/kw-037/giay-nam-dep-cong-so-cao-cap-chinh-hang-tre-trung.jpg" alt="">
                        </a>
                    </div>


                    <!-- Single Banner End -->
                    <!-- Single Banner Start -->
                    <div class="single-banner mb-30">
                        <a href="#">
                            <img src="https://drive.gianhangvn.com/image/sunrise-1200sa-2-2240220j15489.jpg" alt="">
                        </a>
                    </div>
                    <!-- Single Banner End -->
                </div>
            </div>
        </div>
    </div>
    <!--Banner section end-->
    <!----content new---->

    <!----end content new---->
    <!--Product section start-->
    <div
        class="product-section section pt-70 pt-lg-45 pt-md-40 pt-sm-30 pt-xs-15 ">
        <div class="container">

            <div class="row">
                <div class="col">
                    <div class="product-tab-menu mb-40 mb-xs-20">
                        <ul class="nav">
                            <li><a class="active" data-toggle="tab" href="#products">Hot Sale</a></li>
                            <li><a data-toggle="tab" href="#onsale">Sản Phẩm Mới</a></li>
                            <%--<li><a data-toggle="tab" href="#featureproducts"> Feature Products</a></li>--%>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="products">
                        <div class="row tf-element-carousel" data-slick-options='{
                                "slidesToShow": 4,
                                "slidesToScroll": 1,
                                "infinite": true,
                                "rows": 2,
                                "arrows": true,
                                "prevArrow": {"buttonClass": "slick-btn slick-prev", "iconClass": "fa fa-angle-left" },
                                "nextArrow": {"buttonClass": "slick-btn slick-next", "iconClass": "fa fa-angle-right" }
                                }'
                            data-slick-responsive='[
                                {"breakpoint":1199, "settings": {
                                "slidesToShow": 3
                                }},
                                {"breakpoint":992, "settings": {
                                "slidesToShow": 2
                                }},
                                {"breakpoint":768, "settings": {
                                "slidesToShow": 2,
                                "arrows": false,
                                "autoplay": true
                                }},
                                {"breakpoint":576, "settings": {
                                "slidesToShow": 1,
                                "arrows": false,
                                "autoplay": true
                                }}
                                ]'>

                            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                                <ItemTemplate>
                                    <div class="col-lg-12">
                                        <!-- Single Product Start -->
                                        <div class="single-product mb-30">
                                            <div class="product-img">
                                                <a title='<%#:Eval("Name") %> <%#:Eval("ProductId") %>' href='/ChiTietSanPham.aspx?ProductId=<%#:Eval("ProductId") %>'>
                                                    <img style="height: 350px" alt="" src='<%#: Eval("Url") %>' />
                                                </a>
                                                <!-- <span class="descount-sticker">-10%</span> -->
                                                <span class="sticker" style="color: red">New</span>

                                                <div class="product-action d-flex justify-content-between">
                                                    <asp:Button runat="server" CssClass="product-btn" OnClick="btnAddToCart_Click" style="border:0 ;background-color: #343a40;" Text="Add to Cart" CommandName="AddToCart" CommandArgument='<%# Eval("ProductId") %>' />

                                                    <ul class="d-flex">
                                                      
                                                        <li><a href="#" onclick="openQuickViewModal('<%# Eval("ProductId") %>')" title="Quick View"><i class="fa fa-eye"></i></a></li>

                                                        <li><a href="#"><i class="fa fa-heart-o"></i></a></li>
                                                        <li><a href="#"><i class="fa fa-exchange"></i></a></li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="product-content">
                                                <h3><a href='/ChiTietSanPham.aspx?ProductId=<%#:Eval("ProductId") %>'><%#Eval("Name")%></a></h3>
                                                <div class="ratting">
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                </div>
                                                <h4 class="price"><span class="new"><%# Eval("LastPrice")%></span>
                                                    <span class="old"><%#Eval("Price")%>  đ</span></h4>
                                            </div>
                                        </div>

                                        <!-- Single Product End -->
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_ECommerceShopConnectionString %>" 
                                SelectCommand="SELECT p.*, pd.*
                                        FROM Products p
                                        JOIN (
                                            SELECT *, ROW_NUMBER() OVER(PARTITION BY ProductId ORDER BY Id) AS RowNum
                                            FROM ProductImages
                                        ) pd ON p.ProductId = pd.ProductId
                                        WHERE pd.RowNum = 1;
                                        "></asp:SqlDataSource>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="onsale">
                        <div class="row tf-element-carousel" data-slick-options='{
                                    "slidesToShow": 4,
                                    "slidesToScroll": 1,
                                    "infinite": true,
                                    "rows": 2,
                                    "arrows": true,
                                    "prevArrow": {"buttonClass": "slick-btn slick-prev", "iconClass": "fa fa-angle-left" },
                                    "nextArrow": {"buttonClass": "slick-btn slick-next", "iconClass": "fa fa-angle-right" }
                                    }'
                            data-slick-responsive='[
                                    {"breakpoint":1199, "settings": {
                                    "slidesToShow": 3
                                    }},
                                    {"breakpoint":992, "settings": {
                                    "slidesToShow": 2
                                    }},
                                    {"breakpoint":768, "settings": {
                                    "slidesToShow": 2,
                                    "arrows": false,
                                    "autoplay": true
                                    }},
                                    {"breakpoint":576, "settings": {
                                    "slidesToShow": 1,
                                    "arrows": false,
                                    "autoplay": true
                                    }}
                                    ]'>
                            <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2">
                                <ItemTemplate>
                                    <div class="col-12">
                                        <!-- Single Product Start -->
                                        <div class="single-product mb-30">
                                            <div class="product-img">
                                                <a title='<%#:Eval("Name") %> <%#:Eval("ProductId") %>' href='/ChiTietSanPham.aspx?ProductId=<%#:Eval("ProductId") %>'>
                                                    <img style="height: 350px" alt="" src='<%#: Eval("Url") %>' />
                                                </a>
                                                <span class="descount-sticker">-10%</span>
                                                <span class="sticker">New</span>
                                                <div class="product-action d-flex justify-content-between">
                                                    <asp:Button runat="server" class="product-btn" OnClick="btnAddToCart_Click" style="border:0 ;background-color: #343a40;" Text="Add to Cart" CommandName="AddToCart" CommandArgument='<%# Eval("ProductId") %>' />

                                                    <ul class="d-flex">
                                                        <li>
                                                            <asp:LinkButton ID="lnkQuickView" runat="server" CssClass="quick-view-btn" OnClick="lnkQuickView_Click" CommandArgument='<%#: Eval("ProductId") %>'>
                                                            <i class="fa fa-eye"></i>
                                                        </asp:LinkButton>
                                                        </li>
                                                        <li><a href="#"><i class="fa fa-heart-o"></i></a></li>
                                                        <li><a href="#"><i class="fa fa-exchange"></i></a></li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="product-content">
                                                <h3><a href='/ChiTietSanPham.aspx?ProductId=<%#:Eval("ProductId") %>'><%#Eval("Name")%></a></h3>
                                                <div class="ratting">
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                    <i class="fa fa-star"></i>
                                                </div>
                                                <h4 class="price"><span class="new"><%# "10%" %></span>
                                                    <span class="old"><%#Eval("LastPrice")%>  đ</span></h4>
                                            </div>
                                        </div>

                                        <!-- Single Product End -->
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:db_ECommerceShopConnectionString %>"
                                SelectCommand="SELECT p.*, pd.*
                                        FROM Products p
                                        JOIN (
                                            SELECT *, ROW_NUMBER() OVER(PARTITION BY ProductId ORDER BY Id) AS RowNum
                                            FROM ProductImages
                                        ) pd ON p.ProductId = pd.ProductId
                                        WHERE pd.RowNum = 1;
                                        "></asp:SqlDataSource>
                        </div>
                        <!-- Single Product End -->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Product section end-->

    <!-- Feature Section Start -->
    <div class="feature-section section pt-70 pt-lg-50 pt-md-35 pt-sm-30 pt-xs-20">
        <div class="container">
            <div class="row">
                <!-- Section Title Start -->
                <div class="col">
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 col-md-6 col-sm-6">
                    <!-- Single Faeture Start -->
                    <div class="single-feature feature-style-2 mb-30">
                        <div class="icon">
                            <i class="fa-truck fa"></i>
                        </div>
                        <div class="content">
                            <h2>Free shipping </h2>
                            <p>Nội Thành Hà Nội với Tát cả các Hoá đơn</p>
                        </div>
                    </div>
                    <!-- Single Faeture End -->
                </div>
                <div class="col-lg-4 col-md-6 col-sm-6">
                    <!-- Single Faeture Start -->
                    <div class="single-feature feature-style-2 mb-30">
                        <div class="icon">
                            <i class="fa fa-undo"></i>
                        </div>
                        <div class="content">
                            <h2>Đổi trong vòng 3 ngày</h2>
                            <p>Quý khách có thể muốn đổi có lỗi liên hệ lại với Shop</p>
                        </div>
                    </div>
                    <!-- Single Faeture End -->
                </div>
                <div class="col-lg-4 col-md-6 col-sm-6">
                    <!-- Single Faeture Start -->
                    <div class="single-feature feature-style-2 mb-30 br-0">
                        <div class="icon">
                            <i class="fa fa-thumbs-o-up"></i>
                        </div>
                        <div class="content">
                            <h2>Member Vip</h2>
                            <p>Được hưởng các chính sách Ưu đãi khuyến mãi Vip</p>
                        </div>
                    </div>
                    <!-- Single Faeture End -->
                </div>
            </div>
        </div>
    </div>
    <!-- Feature Section End -->

    <!--Blog section start-->
    <div
        class="blog-section section bg-gray pt-100 pt-lg-80 pt-md-70 pt-sm-60 pt-xs-50 pb-75 pb-lg-55 pb-md-45 pb-sm-35 pb-xs-30">
        <div class="container">

            <div class="row">
                <!-- Section Title Start -->
                <div class="col">
                    <div class="section-title mb-40 mb-xs-20">
                        <h2>Tin Tức</h2>
                    </div>
                </div>
                <!-- Section Title End -->
            </div>

            <div class="row tf-element-carousel" data-slick-options='{
                    "slidesToShow": 3,
                    "slidesToScroll": 1,
                    "infinite": true,
                    "arrows": true,
                    "prevArrow": {"buttonClass": "slick-btn slick-prev", "iconClass": "fa fa-angle-left" },
                    "nextArrow": {"buttonClass": "slick-btn slick-next", "iconClass": "fa fa-angle-right" }
                    }'
                data-slick-responsive='[
                    {"breakpoint":1199, "settings": {
                    "slidesToShow": 3
                    }},
                    {"breakpoint":992, "settings": {
                    "slidesToShow": 2,
                    "arrows": false,
                    "autoplay": true
                    }},
                    {"breakpoint":768, "settings": {
                    "slidesToShow": 2
                    }},
                    {"breakpoint":575, "settings": {
                    "slidesToShow": 1,
                    "arrows": false,
                    "autoplay": true
                    }}
                    ]'>
                <!-- Single Blog Start -->
                <div class="blog col">
                    <div class="blog-inner">
                        <div class="media">
                            <a href="blog-details.html" class="image">
                                <img src="Anh/Logo/Loai San Pham Hari.jpg" alt=""></a>
                        </div>
                        <div class="content">
                            <h3 class="title"><a href="blog-details.html">5 Loại Sản Phẩm Tốt</a></h3>
                            <ul class="meta">
                                <li><i class="fa fa-calendar"></i><span class="date-time"><span class="date">20</span><span class="separator">-</span><span class="month">Jul</span></span></li>
                            </ul>
                            <p>Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat </p>
                            <a class="readmore" href="blog-details.html">Read more</a>
                        </div>
                    </div>
                </div>
                <!-- Single Blog End -->
                <!-- Single Blog Start -->
                <div class="blog col">
                    <div class="blog-inner">
                        <div class="media">
                            <a href="blog-details.html" class="image">
                                <img src="Anh/Logo/Brushed-Back-David-Beckham-Hairstyles.jpg" alt=""></a>
                        </div>
                        <div class="content">
                            <h3 class="title"><a href="blog-details.html">Bí Quyết Tóc Đẹp</a></h3>
                            <ul class="meta">
                                <li><i class="fa fa-calendar"></i><span class="date-time"><span class="date">20</span><span class="separator">-</span><span class="month">Jul</span></span></li>
                            </ul>
                            <p>Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat </p>
                            <a class="readmore" href="Blog.aspx">Read more</a>
                        </div>
                    </div>
                </div>
                <!-- Single Blog End -->
                <!-- Single Blog Start -->
                <div class="blog col">
                    <div class="blog-inner">
                        <div class="media">
                            <a href="blog-details.html" class="image">
                                <img src="Anh/Logo/San pam sap Kevin.jpg" alt=""></a>
                        </div>
                        <div class="content">
                            <h3 class="title"><a href="blog-details.html">Đánh Giá Sản Phẩm này có Tốt</a></h3>
                            <ul class="meta">
                                <li><i class="fa fa-calendar"></i><span class="date-time"><span class="date">20</span><span class="separator">-</span><span class="month">Jul</span></span></li>
                            </ul>
                            <p>Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat </p>
                            <a class="readmore" href="blog-details.html">Read more</a>
                        </div>
                    </div>
                </div>
                <!-- Single Blog End -->

            </div>
        </div>
    </div>
    <!--Blog section end-->

    <script type="text/javascript">
        function openQuickViewModal(productId) {
            // Mở modal
            $('#quick-view-modal-container').modal('show');

            // Thực hiện các hành động khác với productId
            console.log('ProductId: ' + productId);
        }
    </script>

</asp:Content>

