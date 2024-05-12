<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="forgetpassword.aspx.cs" Inherits="TT_ShopJQK.forgetpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" OnClientClick="return confirm('xoa di!')">
    <!--Login Register section start-->
    <div class="login-register-section section pt-100 pt-lg-80 pt-md-70 pt-sm-60 pt-xs-50  pb-70 pb-lg-50 pb-md-40 pb-sm-30 pb-xs-20">
        <div class="container">
            <%-- <div class="row">--%>
            <!--Login Form Start-->

            <%-- <div class="col-md-6 col-sm-6">--%>
            <center>
                <div class="customer-login-register">

                    <div class="form-login-title">
                        <h2>Quên mật khẩu</h2>
                        <br />
                    </div>
                    <img style="width: 100px; height: 100px;" src="Anh/Logo/CaptureLogo.JPG" />
                    <div class="login-form" style="width: 500px; height: 400px;">

                        <asp:Panel ID="form1" runat="server">
                            <div class="form-fild">
                                <p>
                                    <label>Tên Đăng Nhập <span class="required">*</span></label>
                                </p>
                                <asp:TextBox ID="txtusername" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-fild">
                                <p>
                                    <label>Email của bạn <span class="required">*</span></label>
                                </p>
                                <%--<input name="password" value="" type="password">--%>
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                            </div>
                            <div class="login-submit" style="margin-top: 20px">
                                <center>
                                    <%-- <button type="submit" class="btn">--%>
                                    <asp:Button ID="btnlogin" CssClass="btn" runat="server" Text="Submit" OnClick="btnlogin_Click" />
                                    <asp:Label ID="lbID" runat="server"></asp:Label>
                                    <asp:Label ID="lbMessage" runat="server" CssClass="msg"></asp:Label>
                                </center>

                            </div>
                            <center>
                                <h5><a href="DangKi.aspx">Tạo tài khoản</a></h5>
                            </center>

                        </asp:Panel>
                        <asp:Panel ID="form2" runat="server">
                            <div class="form-fild">
                                <p>
                                    <label>Mật khẩu mới <span class="required">*</span></label>
                                    <asp:Label id="errorpassnew" style="color: red" runat="server" ><small> </small></asp:Label> 
                                </p>
                                <asp:TextBox ID="txtpassnew" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-fild">
                                <p>
                                    <label>Mật khẩu xác nhận <span class="required">*</span></label>
                                    <asp:Label id="errorpasscf" style="color: red" runat="server" ><small> </small></asp:Label> 
                                </p>
                                <%--<input name="password" value="" type="password">--%>
                                <asp:TextBox ID="txtpassconfig" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-fild">
                                <p>
                                    <label>Mã OTP <span class="required">*</span></label>
                                </p>
                                <%--<input name="password" value="" type="password">--%>
                                <asp:TextBox ID="txtotp" runat="server"></asp:TextBox>
                            </div>
                            <div class="login-submit" style="margin-top: 20px">
                                <center>
                                    <%-- <button type="submit" class="btn">--%>
                                    <asp:Button ID="Button1" CssClass="btn" runat="server" Text="Submit" OnClick="btnlogin_ClickReset" />
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" CssClass="msg"></asp:Label>
                                </center>

                            </div>
                            <center>
                                <h5><a href="DangKi.aspx">Tạo tài khoản</a></h5>
                            </center>

                        </asp:Panel>
                    </div>

                </div>
            </center>
        </div>

        <!--Login Form End-->
        <!--Register Form Start-->

    </div>

    <!--Login Register section end-->
</asp:Content>
